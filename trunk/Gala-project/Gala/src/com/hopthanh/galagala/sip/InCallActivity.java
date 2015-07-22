package com.hopthanh.galagala.sip;

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
import com.hopthanh.galagala.app.AbstractLayoutFragment;
import com.hopthanh.galagala.app.R;
import com.hopthanh.galagala.app.R.id;
import com.hopthanh.galagala.app.R.layout;
import com.hopthanh.galagala.app.left_menu.AbstractLeftMenuFragment;
import com.hopthanh.galagala.app.left_menu.LeftMenuCategoryFragment;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageView;

public class InCallActivity extends ActionBarActivity implements InComingAudioCallListener{
	
	private static final String TAG = "InCallActivity";
	
	public static final String EXTRAT_SIP_SESSION_ID = "GalaSipCallSess";
	public static final String FRAGMENT_ID = "fragmentId";
	public static final int IN_COMING_AUDIO_CALL = 1;
	public static final int OUT_COMING_AUDIO_CALL = 2;
	
	private static InCallActivity mInstance;
	private NgnAVSession mSession = null;
	
	public static InCallActivity getInstance() {
		return mInstance;
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.call_layout_in_out_come_view);
		
		mInstance = this;
		
        Bundle extras = getIntent().getExtras();
        if(extras != null){
        	mSession = NgnAVSession.getSession(extras.getLong(EXTRAT_SIP_SESSION_ID));
	        if(mSession == null){
	        	Log.e(TAG, "Null session");
	        	finish();
	        	return;
	        }
	        mSession.incRef();
	        mSession.setContext(this);
        	displayView(extras.getInt(FRAGMENT_ID));
        }
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
	       if(mSession != null){
	    	   mSession.setContext(null);
	    	   mSession.decRef();
	    	   mSession = null;
	       }

		super.onDestroy();
	}
	
	private void displayView(int fragmentId) {
		// update the main content by replacing fragments
		AbstractLayoutFragment<?> fragment = null;
		switch (fragmentId) {
		case IN_COMING_AUDIO_CALL:
			fragment = new InComingAudioCallFrament();
			break;
		case OUT_COMING_AUDIO_CALL:
			fragment = new OutComingAudioCallFrament();
			break;
		}

		if (fragment != null) {
			fragment.addListener(this);
			FragmentManager fragmentManager = getSupportFragmentManager();
			fragmentManager
					.beginTransaction()
					.replace(R.id.containerInOutComingCall,
							fragment).commit();
		} else {
			// error in creating fragment
			Log.e("MainActivity", "Error in creating fragment");
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

//	public String getPeerName(){
//		String mRemotePartyDisplayName;
//		if(mSession != null){
//			final NgnContact remoteParty = mEngine.getContactService().getContactByUri(mSession.getRemotePartyUri());
//			if(remoteParty != null){
//				mRemotePartyDisplayName = remoteParty.getDisplayName();
//			}
//			else{
//				mRemotePartyDisplayName = NgnUriUtils.getDisplayName(mSession.getRemotePartyUri());
//			}
//			if(NgnStringUtils.isNullOrEmpty(mRemotePartyDisplayName)){
//				mRemotePartyDisplayName = "Unknown";
//			}
//			return mRemotePartyDisplayName;
//		}
//		return "";
//	}
	
	public NgnAVSession getSession() {
		return mSession;
	}

	@Override
	public void notifyUpdateFragment(int fragmentId) {
		// TODO Auto-generated method stub
		displayView(fragmentId);
	}

}
