package com.gala.sip;

import org.doubango.ngn.NgnEngine;
import org.doubango.ngn.events.NgnEventArgs;
import org.doubango.ngn.events.NgnMediaPluginEventArgs;
import org.doubango.ngn.events.NgnRegistrationEventArgs;
import org.doubango.ngn.events.NgnInviteEventArgs;
import org.doubango.ngn.sip.NgnAVSession;
import org.doubango.ngn.sip.NgnInviteSession.InviteState;
import org.doubango.ngn.services.INgnConfigurationService;
import org.doubango.ngn.services.INgnSipService;
import org.doubango.ngn.model.NgnContact;
import org.doubango.ngn.utils.NgnConfigurationEntry;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;
import org.doubango.ngn.media.NgnMediaType;
import org.doubango.tinyWRAP.SipStack;
import org.doubango.tinyWRAP.tdav_codec_id_t;

import com.gala.sip.SipEngine.SipCallState;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class SipCall extends Activity {
	private static final String TAG = SipCall.class.getCanonicalName();
	
	protected final NgnEngine mEngine;
	protected NgnAVSession mSession;
	protected SipCallState sipCallState;
	protected BroadcastReceiver mSipBroadCastRecv;
	
	public final static String EXTRAT_SIPCALL_SESSION_ID = "GalaSipCallSess";
	
	public SipCall(){
		super();
		mEngine = NgnEngine.getInstance();
		sipCallState = SipCallState.CALL_NONE;
		mSession=null;
	}
	
//    public void onCreate(Bundle savedInstanceState) {
//        super.onCreate(savedInstanceState);
//        
//        Bundle extras = getIntent().getExtras();
//        if(extras != null){
//        	mSession = NgnAVSession.getSession(extras.getLong(EXTRAT_SIPCALL_SESSION_ID));
//        }
//        
//        if(mSession == null){
//        	Log.e(TAG, "Null session");
//        	finish();
//        	return;
//        }
//        mSession.incRef();
//        mSession.setContext(this);
//        if(InviteState.INCOMING == mSession.getState()){
//    		sipCallState = SipCallState.CALL_INCOMING;
//    		mEngine.getSoundService().startRingTone();
//        }
//        
//        // listen for audio/video session state
//        mSipBroadCastRecv = new BroadcastReceiver() {
//			@Override
//			public void onReceive(Context context, Intent intent) {
//				handleSipEvent(intent);
//			}
//		};
//		IntentFilter intentFilter = new IntentFilter();
//		intentFilter.addAction(NgnInviteEventArgs.ACTION_INVITE_EVENT);
//		//intentFilter.addAction(NgnMediaPluginEventArgs.ACTION_MEDIA_PLUGIN_EVENT);
//	    registerReceiver(mSipBroadCastRecv, intentFilter);
//	}
	
	@Override
	protected void onResume() {
		super.onResume();
		Log.d(TAG,"onResume()");
		if(mSession != null){
			final InviteState callState = mSession.getState();
			if(callState == InviteState.TERMINATING || callState == InviteState.TERMINATED){
				finish();
			}
		}
	}
	
	@Override
	protected void onDestroy() {
		Log.d(TAG,"onDestroy()");
       if(mSipBroadCastRecv != null){
    	   unregisterReceiver(mSipBroadCastRecv);
    	   mSipBroadCastRecv = null;
       }
       
       if(mSession != null){
    	   mSession.setContext(null);
    	   mSession.decRef();
       }
       super.onDestroy();
	}
	
	public String getStateDesc(){
		if(mSession == null)
			return "NA";

		final InviteState callState = mSession.getState();
		switch(callState){
			case NONE:
			default:
				return "Unknown";
			case INCOMING:
				return "Incoming";
			case INPROGRESS:
				return "Inprogress";
			case REMOTE_RINGING:
				return "Ringing";
			case EARLY_MEDIA:
				return "Early media";
			case INCALL:
				return "In Call";
			case TERMINATING:
				return "Terminating";
			case TERMINATED:
				return "termibated";
		}
	}
	protected void handleSipEvent(Intent intent){		
		NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
		if(args == null){
			Log.e(TAG, "Invalid event args");
			return;
		}
		mSession = NgnAVSession.getSession(args.getSessionId());
		if(mSession == null){
			Log.e(TAG, "Invalid session object");
			return;
		}
		
		final InviteState callState = mSession.getState();
		switch(callState){
			case REMOTE_RINGING:
				mEngine.getSoundService().startRingBackTone();
				sipCallEventCallback(SipCallState.CALL_REMOTE_RINGING,"");
				sipCallState = SipCallState.CALL_REMOTE_RINGING;
				break;
			case INCOMING:
				mEngine.getSoundService().startRingTone();
				sipCallEventCallback(SipCallState.CALL_INCOMING,mSession.getRemotePartyUri());
				sipCallState = SipCallState.CALL_INCOMING;
				break;
			case EARLY_MEDIA:
			case INCALL:
				mEngine.getSoundService().stopRingTone();
				mEngine.getSoundService().stopRingBackTone();
				mSession.setSpeakerphoneOn(false);
				sipCallEventCallback(callState==InviteState.INCALL?SipCallState.CALL_INCALL:SipCallState.CALL_EARLY_MEDIA,
						mSession.getRemotePartyDisplayName());//Long.toString(mSession.getId()));
				sipCallState = callState==InviteState.INCALL?SipCallState.CALL_INCALL:SipCallState.CALL_EARLY_MEDIA;
				break;
			case TERMINATING:
			case TERMINATED:
				mEngine.getSoundService().stopRingTone();
				mEngine.getSoundService().stopRingBackTone();
				sipCallEventCallback(SipCallState.CALL_TERMINATED,"");
				sipCallState = SipCallState.CALL_NONE;
				finish();
				break;
			default:
					break;
		}
	}
	private void handleMediaEvent(Intent intent){
		final String action = intent.getAction();
	
		NgnMediaPluginEventArgs args = intent.getParcelableExtra(NgnMediaPluginEventArgs.EXTRA_EMBEDDED);
		if(args == null){
			Log.e(TAG, "Invalid event args");
			return;
		}
		
		switch(args.getEventType()){
			case STARTED_OK: //started or restarted (e.g. reINVITE)
			{
				break;
			}
			case PREPARED_OK:
			case PREPARED_NOK:
			case STARTED_NOK:
			case STOPPED_OK:
			case STOPPED_NOK:
			case PAUSED_OK:
			case PAUSED_NOK:
			{
				break;
			}
		}
	}
	public void sipCallEventCallback(SipCallState callState, String callPara)
	{
	}
	public boolean isCallIncoming(){
		return sipCallState == SipCallState.CALL_INCOMING;
	}
	public boolean isInCall(){
		return sipCallState != SipCallState.CALL_NONE;
	}
	public boolean acceptIncomingCall(){
		if(mSession != null){
			return mSession.acceptCall();
		}
		return false;
	}
	public boolean hangUpCall(){
		if(mSession != null){
			return mSession.hangUpCall();
		}
		return false;
	}
	public String getPeerName(){
		String mRemotePartyDisplayName;
		if(mSession != null){
			final NgnContact remoteParty = mEngine.getContactService().getContactByUri(mSession.getRemotePartyUri());
			if(remoteParty != null){
				mRemotePartyDisplayName = remoteParty.getDisplayName();
			}
			else{
				mRemotePartyDisplayName = NgnUriUtils.getDisplayName(mSession.getRemotePartyUri());
			}
			if(NgnStringUtils.isNullOrEmpty(mRemotePartyDisplayName)){
				mRemotePartyDisplayName = "Unknown";
			}
			return mRemotePartyDisplayName;
		}
		return "";
	}
}
