package com.hopthanh.galagala.sip;

import org.doubango.ngn.NgnApplication;
import org.doubango.ngn.NgnEngine;
import org.doubango.ngn.NgnNativeService;
import org.doubango.ngn.events.NgnEventArgs;
import org.doubango.ngn.events.NgnInviteEventArgs;
import org.doubango.ngn.events.NgnMessagingEventArgs;
import org.doubango.ngn.events.NgnRegistrationEventArgs;
import org.doubango.ngn.model.NgnHistoryEvent.StatusType;
import org.doubango.ngn.model.NgnHistorySMSEvent;
import org.doubango.ngn.sip.NgnAVSession;
import org.doubango.ngn.utils.NgnDateTimeUtils;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.PowerManager;
import android.util.Log;
import android.widget.Toast;

public class NativeSipService extends NgnNativeService{
	private final static String TAG = NativeSipService.class.getCanonicalName();
	
	private PowerManager.WakeLock mWakeLock;
	private BroadcastReceiver mBroadcastReceiver;
	
	public NativeSipService(){
		super();
	}
	
	@Override
	public void onCreate() {
		super.onCreate();
		Log.d(TAG, "onCreate()");
		Toast.makeText(this, "NativeSipService Started", Toast.LENGTH_LONG).show();
		
		final PowerManager powerManager = (PowerManager) getSystemService(Context.POWER_SERVICE);
		if(powerManager != null && mWakeLock == null){
			mWakeLock = powerManager.newWakeLock(PowerManager.ON_AFTER_RELEASE 
					| PowerManager.SCREEN_BRIGHT_WAKE_LOCK 
					| PowerManager.ACQUIRE_CAUSES_WAKEUP, TAG);
		}
	}

	@Override
	public void onStart(Intent intent, int startId) {
		// TODO Auto-generated method stub
		super.onStart(intent, startId);
		
		mBroadcastReceiver = new BroadcastReceiver() {

			@Override
			public void onReceive(Context context, Intent intent) {
				// TODO Auto-generated method stub
				NgnEngine engine = SipSingleton.getInstance().getEngine();
				final String action = intent.getAction();
				
				// Registration Events
				if(NgnRegistrationEventArgs.ACTION_REGISTRATION_EVENT.equals(action)){
					NgnRegistrationEventArgs args = intent.getParcelableExtra(NgnEventArgs.EXTRA_EMBEDDED);
					if(args == null){
						Log.e(TAG, "Invalid event args");
						return;
					}
					switch(args.getEventType()){
						case REGISTRATION_OK:
						case REGISTRATION_NOK:
						case REGISTRATION_INPROGRESS:
						case UNREGISTRATION_INPROGRESS:
						case UNREGISTRATION_OK:
						case UNREGISTRATION_NOK:
						default:
							if(SipSingleton.getInstance().isRegistered()){
								NgnApplication.acquirePowerLock();
							}
							else{
								NgnApplication.releasePowerLock();
							}
							break;
					}
				}
				
				// PagerMode Messaging Events
				else if(NgnMessagingEventArgs.ACTION_MESSAGING_EVENT.equals(action)){
					NgnMessagingEventArgs args = intent.getParcelableExtra(NgnMessagingEventArgs.EXTRA_EMBEDDED);
					if(args == null){
						Log.e(TAG, "Invalid event args");
						return;
					}
					switch(args.getEventType()){
						case INCOMING:
							String dateString = intent.getStringExtra(NgnMessagingEventArgs.EXTRA_DATE);
							String remoteParty = intent.getStringExtra(NgnMessagingEventArgs.EXTRA_REMOTE_PARTY);
							if(NgnStringUtils.isNullOrEmpty(remoteParty)){
								remoteParty = NgnStringUtils.nullValue();
							}
							remoteParty = NgnUriUtils.getUserName(remoteParty);
							NgnHistorySMSEvent event = new NgnHistorySMSEvent(remoteParty, StatusType.Incoming);
							event.setContent(new String(args.getPayload()));
							event.setStartTime(NgnDateTimeUtils.parseDate(dateString).getTime());
							engine.getHistoryService().addEvent(event);
//							mEngine.showSMSNotif(R.drawable.sms_25, "New message");
							break;
					}
				}

				// Invite Events
				else if(NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action)){
					NgnInviteEventArgs args = intent.getParcelableExtra(NgnEventArgs.EXTRA_EMBEDDED);
					if(args == null){
						Log.e(TAG, "Invalid event args");
						return;
					}
					
					switch(args.getEventType()){
						case TERMWAIT:
						case TERMINATED:
							Log.e(TAG, "terminate");
							engine.getSoundService().stopRingTone();
							engine.getSoundService().stopRingBackTone();
							InCallActivity.getInstance().finish();
							break;
							
						case INCOMING:
							Log.e(TAG, "inComingCall");
							final NgnAVSession avSession = NgnAVSession.getSession(args.getSessionId());
							if(avSession != null){
								incomingCallLoadScreen(context, avSession.getId());
								if(mWakeLock != null && !mWakeLock.isHeld()){
									mWakeLock.acquire(10);
								}
								engine.getSoundService().startRingTone();
							}
							else{
								Log.e(TAG, String.format("Failed to find session with id=%ld", args.getSessionId()));
							}
							break;
							
						case INPROGRESS:
							break;
							
						case RINGING:
							Log.e(TAG, "ringing");
							engine.getSoundService().startRingBackTone();
							break;
						
						case CONNECTED:
						case EARLY_MEDIA:
							Log.e(TAG, "inCall");
							engine.getSoundService().stopRingTone();
							engine.getSoundService().stopRingBackTone();
							InCallActivity.getInstance().getSession().setSpeakerphoneOn(false);
							break;
						default: 
							break;
					}
				}
			}
		};
		
		final IntentFilter intentFilter = new IntentFilter();
		intentFilter.addAction(NgnRegistrationEventArgs.ACTION_REGISTRATION_EVENT);
		intentFilter.addAction(NgnInviteEventArgs.ACTION_INVITE_EVENT);
		registerReceiver(mBroadcastReceiver, intentFilter);
		
		if(intent != null){
			Bundle bundle = intent.getExtras();
			if (bundle != null && bundle.getBoolean("autostarted")) {
				SipSingleton.getInstance().onResume(getApplicationContext());
			}
		}

	}

	@Override
	public void onDestroy() {
		Log.d(TAG, "onDestroy()");
		if(mBroadcastReceiver != null){
			unregisterReceiver(mBroadcastReceiver);
			mBroadcastReceiver = null;
		}
		if(mWakeLock != null){
			if(mWakeLock.isHeld()){
				mWakeLock.release();
				mWakeLock = null;
			}
		}
		super.onDestroy();
	}

//	private void handleSipEvent(Context context, Intent intent) {
//		NgnEngine mEngine = SipSingleton.getInstance().getEngine();
//		NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
//		if(args == null){
//			Log.e(TAG, "Invalid event args");
//			return;
//		}
//		
//		NgnAVSession session = InCallActivity.getInstance() != null? InCallActivity.getInstance().getSession() : null;
//		
//		if(session == null) {
//			session = NgnAVSession.getSession(args.getSessionId());
//			if(session == null){
//				Log.e(TAG, "Invalid session object");
//				return;
//			}
//		}
//
//		if(args.getSessionId() != session.getId()){
//			Log.e(TAG, "args.getSessionId() != mSession.getId()");
//			return;
//		}
//
//		final String action = intent.getAction();
//		if(NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action)){
//			final InviteState callState = session.getState();
//			NgnInviteEventTypes eventType =  args.getEventType();
//			Log.e(TAG, "====callState===="+callState+"=====eventType======="+eventType);
////			switch(callState){
////			case REMOTE_RINGING:
////				Log.e(TAG, "remote ringing");
////				mEngine.getSoundService().startRingBackTone();
////				break;
////			case INCOMING:
////				Log.e(TAG, "incomingCall");
////				incomingCall(context, session.getId());
//////				SipSingleton.getInstance().in_out_Coming_Call(context, session.getId(), InCallActivity.IN_COMING_AUDIO_CALL);
////				break;
////			case EARLY_MEDIA:
////			case INCALL:
////				Log.e(TAG, "inCall");
////				mEngine.getSoundService().stopRingTone();
////				mEngine.getSoundService().stopRingBackTone();
////				session.setSpeakerphoneOn(false);
////				break;
////			case TERMINATING:
////			case TERMINATED:
////				Log.e(TAG, "terminate");
////				mEngine.getSoundService().stopRingTone();
////				mEngine.getSoundService().stopRingBackTone();
////				InCallActivity.getInstance().finish();
////				break;
////			default:
////				break;
////			}
//			
//			switch(eventType){
//			case RINGING:
//				Log.e(TAG, "ringing");
//				mEngine.getSoundService().startRingBackTone();
//				break;
//			case INCOMING:
//				Log.e(TAG, "incomingCall");
//				incomingCall(context, session.getId());
////				SipSingleton.getInstance().in_out_Coming_Call(context, session.getId(), InCallActivity.IN_COMING_AUDIO_CALL);
//				break;
//			case INPROGRESS:
//				break;
//			case EARLY_MEDIA:
//			case CONNECTED:
//				Log.e(TAG, "inCall");
//				mEngine.getSoundService().stopRingTone();
//				mEngine.getSoundService().stopRingBackTone();
//				session.setSpeakerphoneOn(false);
//				break;
//			case TERMWAIT:
//			case TERMINATED:
//				Log.e(TAG, "terminate");
//				mEngine.getSoundService().stopRingTone();
//				mEngine.getSoundService().stopRingBackTone();
//				InCallActivity.getInstance().finish();
//				break;
//			default:
//				break;
//			}
//		}
//	}
	
	private void incomingCallLoadScreen(Context context, long sessId){
		Intent i = new Intent();
		i.setClass(context, InCallActivity.class);
		i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		i.putExtra(InCallActivity.EXTRAT_SIP_SESSION_ID, sessId);
		i.putExtra(InCallActivity.FRAGMENT_ID, InCallActivity.IN_COMING_AUDIO_CALL);
		context.startActivity(i);
	}
}
