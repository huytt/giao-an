package com.hopthanh.galagala.sip;

import org.doubango.ngn.events.NgnEventArgs;
import org.doubango.ngn.events.NgnInviteEventArgs;
import org.doubango.ngn.events.NgnRegistrationEventArgs;
import org.doubango.ngn.sip.NgnAVSession;
import org.doubango.ngn.sip.NgnInviteSession.InviteState;

import com.hopthanh.galagala.app.R;

import android.app.Fragment;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class FavorFragment extends Fragment {
	private View mView = null;

	private TextView mTvInfo;
	private EditText mEtUserName;
	private EditText mEtPassword;
	private EditText mEtProxyHost;
	private EditText mEtProxyPort;
	private Button mBtSignInOut;
	private EditText mEtDn;
	private Button mBtDial;
	private BroadcastReceiver mSipBroadCastRecv;
	private SipSingleton mSipEngine = SipSingleton.getInstance();

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
//		mView = inflater.inflate(R.layout.call_layout_favor, container, false);
		mView = inflater.inflate(R.layout.call_layout_setting_sip_temp, container, false);
		
        mTvInfo = (TextView)mView.findViewById(R.id.textViewInfo);
        mEtUserName = (EditText)mView.findViewById(R.id.editTextPrivateIdentity);
        mEtPassword = (EditText)mView.findViewById(R.id.editTextPassword);
        mEtProxyHost = (EditText)mView.findViewById(R.id.editTextProxyHost);
        mEtProxyPort = (EditText)mView.findViewById(R.id.editTextProxyPort);
        mBtSignInOut = (Button)mView.findViewById(R.id.buttonSignInOut);
        mEtDn = (EditText)mView.findViewById(R.id.editTextDn);
        mBtDial = (Button)mView.findViewById(R.id.buttonDial);
 
//        mEtUserName.setText(getConfigUserName());
//        mEtPassword.setText(getConfigPassword());
//        mEtProxyHost.setText(getConfigProxyAddr());
//        mEtProxyPort.setText(getConfigProxyPort());
        
        mBtSignInOut.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if(mSipEngine.isRegistered()){
					mSipEngine.sipUnRegister();
				}else{
					mSipEngine.sipConfigure(mEtUserName.getText().toString(),mEtPassword.getText().toString(),
							mEtProxyHost.getText().toString(),mEtProxyPort.getText().toString());
					mSipEngine.sipRegister(getActivity().getApplicationContext());
				}
			}
		});
        mBtDial.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if(mSipEngine.isRegistered()){
					callTo(mEtDn.getText().toString());
				}else{
					
				}
			}
		});
        
		// Listen for registration events
        mSipBroadCastRecv = new BroadcastReceiver() {
			@Override
			public void onReceive(Context context, Intent intent) {
				final String action = intent.getAction();
				
				// Registration Event
				if(NgnRegistrationEventArgs.ACTION_REGISTRATION_EVENT.equals(action)){
					NgnRegistrationEventArgs args = intent.getParcelableExtra(NgnEventArgs.EXTRA_EMBEDDED);
					if(args == null){
						mTvInfo.setText("Invalid event args");
						return;
					}
					switch(args.getEventType()){
						case REGISTRATION_NOK:
							mTvInfo.setText("Failed to register :(");
							break;
						case UNREGISTRATION_OK:
							mTvInfo.setText("You are now unregistered :)");
							break;
						case REGISTRATION_OK:
							mTvInfo.setText("You are now registered :)");
							break;
						case REGISTRATION_INPROGRESS:
							mTvInfo.setText("Trying to register...");
							break;
						case UNREGISTRATION_INPROGRESS:
							mTvInfo.setText("Trying to unregister...");
							break;
						case UNREGISTRATION_NOK:
							mTvInfo.setText("Failed to unregister :(");
							break;
					}
				} else if (NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action)) {
					NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
					if(args == null){
//						Log.e(TAG, "Invalid event args");
						return;
					}
					NgnAVSession mSession = NgnAVSession.getSession(args.getSessionId());
					if(mSession == null){
//						Log.e(TAG, "Invalid session object");
						return;
					}
				
					final InviteState callState = mSession.getState();
					if(callState == InviteState.INCOMING)
					{
						incomingCall(mSession.getId());
					}			
				
				}
			}
		};
		
		final IntentFilter intentFilter = new IntentFilter();
		intentFilter.addAction(NgnRegistrationEventArgs.ACTION_REGISTRATION_EVENT);
		intentFilter.addAction(NgnInviteEventArgs.ACTION_INVITE_EVENT);
	    getActivity().getApplicationContext().registerReceiver(mSipBroadCastRecv, intentFilter);
	    
		return mView;
//		return super.onCreateView(inflater, container, savedInstanceState);
	}

	private void incomingCall(long sessId){
		Intent i = new Intent();
		i.setClass(getActivity().getApplicationContext(), InCallActivity.class);
		i.putExtra(InCallActivity.EXTRAT_SIP_SESSION_ID, sessId);
		startActivity(i);
		
		mSipEngine.getEngine().getSoundService().startRingTone();
	}

	
    public void callTo(String dn)
    {
    	long sid = mSipEngine.prepareCall(dn);
 
		Intent i = new Intent();
		i.setClass(getActivity().getApplicationContext(), InCallActivity.class);
		i.putExtra(InCallActivity.EXTRAT_SIP_SESSION_ID, sid);
		startActivity(i);

		mSipEngine.makeCall(sid);
    }

    @Override
    public void onResume() {
    	// TODO Auto-generated method stub
    	mSipEngine.onResume(getActivity().getApplicationContext());
    	super.onResume();
    }
    
	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		getActivity().getApplicationContext().unregisterReceiver(mSipBroadCastRecv);
		if (mView != null && mView.getParent() != null) {
			((ViewGroup) mView.getParent()).removeView(mView);
			mView = null;
			System.gc();
		}

		super.onDestroyView();
	}
}
