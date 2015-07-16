package com.hopthanh.galagala.app;

import org.doubango.ngn.NgnEngine;
import org.doubango.ngn.events.NgnInviteEventArgs;
import org.doubango.ngn.events.NgnMediaPluginEventArgs;
import org.doubango.ngn.model.NgnContact;
import org.doubango.ngn.sip.NgnAVSession;
import org.doubango.ngn.sip.NgnInviteSession.InviteState;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;

import com.gala.sip.SipCall;
import com.gala.sip.SipEngine;
import com.gala.sip.SipEngine.SipCallState;
import com.hopthanh.gala.utils.LayoutCallUtils;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageView;

public class InCallActivity extends ActionBarActivity{
	
	private static final String TAG = "InCallActivity";
	public static final String EXTRAT_SIP_SESSION_ID = "GalaSipCallSess";
	private NgnAVSession mSession;
	private BroadcastReceiver mSipBroadCastRecv;
	protected SipCallState sipCallState = SipCallState.CALL_NONE;
	private final NgnEngine mEngine = SipSingleton.getInstance().getEngine();
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.call_layout_incall_audio);
		
        Bundle extras = getIntent().getExtras();
        if(extras != null){
        	mSession = NgnAVSession.getSession(extras.getLong(EXTRAT_SIP_SESSION_ID));
        }
        
        if(mSession == null){
        	Log.e(TAG, "Null session");
        	finish();
        	return;
        }
        mSession.incRef();
        mSession.setContext(this);
        
//      if(InviteState.INCOMING == mSession.getState()){
//  		sipCallState = SipCallState.CALL_INCOMING;
//  		mEngine.getSoundService().startRingTone();
//      }

        // listen for audio/video session state
        mSipBroadCastRecv = new BroadcastReceiver() {
			@Override
			public void onReceive(Context context, Intent intent) {
				handleSipEvent(intent);
			}
		};
		IntentFilter intentFilter = new IntentFilter();
		intentFilter.addAction(NgnInviteEventArgs.ACTION_INVITE_EVENT);
	    registerReceiver(mSipBroadCastRecv, intentFilter);

		loadCallAction();
		
		ImageView imgCancel = (ImageView) findViewById(R.id.imgCancel);
		
		imgCancel.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				hangUpCall();
				finish();
			}
		});
	}
	
	@Override
	protected void onResume() {
		// TODO Auto-generated method stub
		super.onResume();
		if(mSession != null){
			final InviteState callState = mSession.getState();
			if(callState == InviteState.TERMINATING || callState == InviteState.TERMINATED){
				finish();
			}
		}

	}
	
	@Override
	protected void onDestroy() {
		// TODO Auto-generated method stub
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
	
	private String getStateDesc(InviteState state){
		switch(state){
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
	
	private void handleSipEvent(Intent intent){
		if(mSession == null){
			Log.e(TAG, "Invalid session object");
			return;
		}
		final String action = intent.getAction();
		if(NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action)){
			NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
			if(args == null){
				Log.e(TAG, "Invalid event args");
				return;
			}
			if(args.getSessionId() != mSession.getId()){
				return;
			}
			
			final InviteState callState = mSession.getState();
			switch(callState){
			case REMOTE_RINGING:
				mEngine.getSoundService().startRingBackTone();
				sipCallState = SipCallState.CALL_REMOTE_RINGING;
				break;
			case INCOMING:
				mEngine.getSoundService().startRingTone();
				sipCallState = SipCallState.CALL_INCOMING;
				break;
			case EARLY_MEDIA:
			case INCALL:
				mEngine.getSoundService().stopRingTone();
				mEngine.getSoundService().stopRingBackTone();
				mSession.setSpeakerphoneOn(false);
				sipCallState = callState==InviteState.INCALL?SipCallState.CALL_INCALL:SipCallState.CALL_EARLY_MEDIA;
				break;
			case TERMINATING:
			case TERMINATED:
				mEngine.getSoundService().stopRingTone();
				mEngine.getSoundService().stopRingBackTone();
				sipCallState = SipCallState.CALL_NONE;
				finish();
				break;
			default:
					break;
			}
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
	
	private void loadCallAction(){
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_mute), R.drawable.icon_call_action_mute_80, LayoutCallUtils.TAG_AUDIO_CALL, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_chat), R.drawable.icon_call_action_chat_80, LayoutCallUtils.TAG_CHAT, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_speak), R.drawable.icon_call_action_speak_80, LayoutCallUtils.TAG_SPEAK, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_video_call), R.drawable.icon_call_action_video_call_80, LayoutCallUtils.TAG_VIDEO_CALL, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_call_transfer), R.drawable.icon_call_action_call_transfer_80, LayoutCallUtils.TAG_CALL_TRANSFER, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_contact), R.drawable.icon_call_action_contact_80, LayoutCallUtils.TAG_CONTACT, mOnKeyboardClickListener);
	}
	
	private View.OnClickListener mOnKeyboardClickListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			switch(Integer.parseInt(v.getTag().toString())) {
			case LayoutCallUtils.TAG_AUDIO_CALL:
				break;
			case LayoutCallUtils.TAG_CHAT:
				break;
			case LayoutCallUtils.TAG_SPEAK:
				break;
			case LayoutCallUtils.TAG_VIDEO_CALL:
				break;
			case LayoutCallUtils.TAG_CALL_TRANSFER:
				break;
			case LayoutCallUtils.TAG_CONTACT:
				break;
			}
		}
	};

}
