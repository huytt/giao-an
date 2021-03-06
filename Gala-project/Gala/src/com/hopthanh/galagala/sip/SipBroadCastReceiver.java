package com.hopthanh.galagala.sip;

import org.doubango.ngn.NgnEngine;
import org.doubango.ngn.events.NgnEventArgs;
import org.doubango.ngn.events.NgnInviteEventArgs;
import org.doubango.ngn.events.NgnInviteEventTypes;
import org.doubango.ngn.events.NgnMessagingEventArgs;
import org.doubango.ngn.events.NgnRegistrationEventArgs;
import org.doubango.ngn.model.NgnHistorySMSEvent;
import org.doubango.ngn.model.NgnHistoryEvent.StatusType;
import org.doubango.ngn.sip.NgnAVSession;
import org.doubango.ngn.sip.NgnInviteSession.InviteState;
import org.doubango.ngn.utils.NgnDateTimeUtils;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;

import com.gala.sip.SipEngine.SipCallState;
import com.hopthanh.galagala.app.MessageActivity;
import com.hopthanh.galagala.app.R;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

public class SipBroadCastReceiver extends BroadcastReceiver{

	private static final String TAG = "SipBroadCastReceiver";

	@Override
	public void onReceive(Context context, Intent intent) {
		// TODO Auto-generated method stub
//		final String action = intent.getAction();
		
		// Registration Event
//		if(NgnRegistrationEventArgs.ACTION_REGISTRATION_EVENT.equals(action)){
//			NgnRegistrationEventArgs args = intent.getParcelableExtra(NgnEventArgs.EXTRA_EMBEDDED);
//			if(args == null){
//				Log.e(TAG, "Invalid event args");
//				return;
//			}
//			switch(args.getEventType()){
//				case REGISTRATION_NOK:
//					Log.e(TAG, "Failed to register :(");
//					break;
//				case UNREGISTRATION_OK:
//					Log.e(TAG, "You are now unregistered :)");
//					break;
//				case REGISTRATION_OK:
//					Log.e(TAG, "You are now registered :)");
//					break;
//				case REGISTRATION_INPROGRESS:
//					Log.e(TAG, "Trying to register...");
//					break;
//				case UNREGISTRATION_INPROGRESS:
//					Log.e(TAG, "Trying to unregister...");
//					break;
//				case UNREGISTRATION_NOK:
//					Log.e(TAG, "Failed to unregister :(");
//					break;
//			}
//		} else 
//		if (NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action)) {
//			NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
//			if(args == null){
//				Log.e(TAG, "Invalid event args");
//				return;
//			}
//			NgnAVSession mSession = NgnAVSession.getSession(args.getSessionId());
//			if(mSession == null){
//				Log.e(TAG, "Invalid session object");
//				return;
//			}
//		
//			final InviteState callState = mSession.getState();
//			if(callState == InviteState.INCOMING)
//			{
//				incomingCall(context, mSession.getId());
//			}			
//		
//		}
		
		handleSipEvent(context, intent);
	}
	
	private void handleSipEvent(Context context, Intent intent){
		Engine mEngine = SipSingleton.getInstance().getEngine(); 

		final String action = intent.getAction();
		if(NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action)){
			NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
			if(args == null){
				Log.e(TAG, "Invalid event args");
				return;
			}
			
			NgnAVSession session = InCallActivity.getInstance() != null? InCallActivity.getInstance().getSession() : null;
			
			if(session == null) {
				session = NgnAVSession.getSession(args.getSessionId());
				if(session == null){
					Log.e(TAG, "Invalid session object");
					return;
				}
			}

			if(args.getSessionId() != session.getId()){
				Log.e(TAG, "args.getSessionId() != mSession.getId()");
				return;
			}

			final InviteState callState = session.getState();
			NgnInviteEventTypes eventType =  args.getEventType();
			Log.e(TAG, "====callState===="+callState+"=====eventType======="+eventType);
//			switch(callState){
//			case REMOTE_RINGING:
//				Log.e(TAG, "remote ringing");
//				mEngine.getSoundService().startRingBackTone();
//				break;
//			case INCOMING:
//				Log.e(TAG, "incomingCall");
//				incomingCall(context, session.getId());
////				SipSingleton.getInstance().in_out_Coming_Call(context, session.getId(), InCallActivity.IN_COMING_AUDIO_CALL);
//				break;
//			case EARLY_MEDIA:
//			case INCALL:
//				Log.e(TAG, "inCall");
//				mEngine.getSoundService().stopRingTone();
//				mEngine.getSoundService().stopRingBackTone();
//				session.setSpeakerphoneOn(false);
//				break;
//			case TERMINATING:
//			case TERMINATED:
//				Log.e(TAG, "terminate");
//				mEngine.getSoundService().stopRingTone();
//				mEngine.getSoundService().stopRingBackTone();
//				InCallActivity.getInstance().finish();
//				break;
//			default:
//				break;
//			}
			
			switch(eventType){
			case RINGING:
				Log.e(TAG, "ringing");
				mEngine.getSoundService().startRingBackTone();
				break;
			case INCOMING:
				Log.e(TAG, "incomingCall");
				incomingCall(context, session.getId());
//				SipSingleton.getInstance().in_out_Coming_Call(context, session.getId(), InCallActivity.IN_COMING_AUDIO_CALL);
				break;
			case INPROGRESS:
				break;
			case EARLY_MEDIA:
			case CONNECTED:
				Log.e(TAG, "inCall");
				mEngine.getSoundService().stopRingTone();
				mEngine.getSoundService().stopRingBackTone();
				session.setSpeakerphoneOn(false);
				break;
			case TERMWAIT:
			case TERMINATED:
				Log.e(TAG, "terminate");
				mEngine.getSoundService().stopRingTone();
				mEngine.getSoundService().stopRingBackTone();
				InCallActivity.getInstance().finish();
				break;
			default:
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
					Log.e(TAG, "Message comming");
					String dateString = intent.getStringExtra(NgnMessagingEventArgs.EXTRA_DATE);
					String remoteParty = intent.getStringExtra(NgnMessagingEventArgs.EXTRA_REMOTE_PARTY);
					if(NgnStringUtils.isNullOrEmpty(remoteParty)){
						remoteParty = NgnStringUtils.nullValue();
					}
					remoteParty = NgnUriUtils.getUserName(remoteParty);
					NgnHistorySMSEvent event = new NgnHistorySMSEvent(remoteParty, StatusType.Incoming);
					event.setContent(new String(args.getPayload()));
					event.setStartTime(NgnDateTimeUtils.parseDate(dateString).getTime());
					mEngine.getHistoryService().addEvent(event);
//					if(!MessageActivity.isAvailable(context)) {
						mEngine.showSMSNotif(R.drawable.sms_25, "New message");
//					}
					break;
			}
		}
	}
	
	private void incomingCall(Context context, long sessId){
		Intent i = new Intent();
		i.setClass(context, InCallActivity.class);
		i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		i.putExtra(InCallActivity.EXTRAT_SIP_SESSION_ID, sessId);
		i.putExtra(InCallActivity.FRAGMENT_ID, InCallActivity.IN_COMING_AUDIO_CALL);
		context.startActivity(i);
		
		SipSingleton.getInstance().getEngine().getSoundService().startRingTone();
	}

}
