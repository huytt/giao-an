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
import org.doubango.ngn.utils.NgnConfigurationEntry;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;
import org.doubango.ngn.media.NgnMediaType;
import org.doubango.tinyWRAP.SipStack;
import org.doubango.tinyWRAP.tdav_codec_id_t;
import org.doubango.ngn.events.NgnMediaPluginEventArgs;
import org.doubango.ngn.media.NgnMediaType;
import org.doubango.ngn.model.NgnContact;

import android.app.Application;
import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.util.Log;
import android.media.AudioManager;

public class SipEngine extends Activity{
	private static final String TAG = SipEngine.class.getCanonicalName();

	private final NgnEngine mEngine;
	private final INgnConfigurationService mConfigurationService;
	private final INgnSipService mSipService;
	private String userName;
	private String password;
	private String proxyAddr;
	private String proxyPort;
	
	public final static String EXTRAT_SIP_SESSION_ID = "GalaSipSess";
	
	public enum SipRegistrationEventTypes{
		SIP_REGISTRATION_NOK,
		SIP_UNREGISTRATION_OK,
		SIP_REGISTRATION_OK,
		SIP_REGISTRATION_INPROGRESS,
		SIP_UNREGISTRATION_INPROGRESS,
		SIP_UNREGISTRATION_NOK
	}
	public enum SipCallState{
        CALL_NONE,
        CALL_INCOMING,
        CALL_INPROGRESS,
        CALL_REMOTE_RINGING,
        CALL_EARLY_MEDIA,
        CALL_INCALL,
        CALL_TERMINATING,
        CALL_TERMINATED,
    }

	private BroadcastReceiver mSipBroadCastRecv = new BroadcastReceiver() {
		public void onReceive(Context context, Intent intent) {
			final String action = intent.getAction();
			
			// Registration Event
			if(NgnRegistrationEventArgs.ACTION_REGISTRATION_EVENT.equals(action)){
				NgnRegistrationEventArgs args = intent.getParcelableExtra(NgnEventArgs.EXTRA_EMBEDDED);
				if(args == null){
					return;
				}else{
					switch(args.getEventType()){
					case REGISTRATION_NOK:
						sipRegEventCallback(SipRegistrationEventTypes.SIP_REGISTRATION_NOK);
						break;
					case UNREGISTRATION_OK:
						sipRegEventCallback(SipRegistrationEventTypes.SIP_UNREGISTRATION_OK);
						break;
					case REGISTRATION_OK:
						sipRegEventCallback(SipRegistrationEventTypes.SIP_REGISTRATION_OK);
						break;
					case REGISTRATION_INPROGRESS:
						sipRegEventCallback(SipRegistrationEventTypes.SIP_REGISTRATION_INPROGRESS);
						break;
					case UNREGISTRATION_INPROGRESS:
						sipRegEventCallback(SipRegistrationEventTypes.SIP_UNREGISTRATION_INPROGRESS);
						break;
					case UNREGISTRATION_NOK:
						sipRegEventCallback(SipRegistrationEventTypes.SIP_UNREGISTRATION_NOK);
						break;
					}
				}
			} 
			else if (NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action))
			{
				NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
				if(args == null){
					Log.e(TAG, "Invalid event args");
					return;
				}
				NgnAVSession mSession = NgnAVSession.getSession(args.getSessionId());
				if(mSession == null){
					Log.e(TAG, "Invalid session object");
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

	protected SipEngine(){
		mEngine = NgnEngine.getInstance();
		mConfigurationService = mEngine.getConfigurationService();
		mSipService = mEngine.getSipService();
	}
	public boolean sipConfigure(String username, String passwd, String sipProxyAddr, String sipProxyPort){
		 if(username == null || sipProxyAddr == null
			|| username.length() <= 0 || sipProxyAddr.length() <=0)
		 {
			 return false;
		 }
		int mCodecs;
		//lee zou, configure the codecs
		mCodecs = tdav_codec_id_t.tdav_codec_id_pcma.swigValue() |
				tdav_codec_id_t.tdav_codec_id_pcmu.swigValue() |
				tdav_codec_id_t.tdav_codec_id_g729ab.swigValue();
		mConfigurationService.putInt(NgnConfigurationEntry.MEDIA_CODECS, mCodecs);
		SipStack.setCodecs_2(mCodecs);
		// Compute
		if(!mConfigurationService.commit()){
			Log.e(TAG, "Failed to Commit() Codecs configuration");
		}
		mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_IMPI, username);
		mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_IMPU, "sip:"+username+"@"+sipProxyAddr);
		mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_PASSWORD,passwd);
		mConfigurationService.putString(NgnConfigurationEntry.NETWORK_PCSCF_HOST,sipProxyAddr);
		mConfigurationService.putInt(NgnConfigurationEntry.NETWORK_PCSCF_PORT, NgnStringUtils.parseInt(sipProxyPort, 5060));
		mConfigurationService.putString(NgnConfigurationEntry.NETWORK_REALM,sipProxyAddr);
		// VERY IMPORTANT: Commit changes
		mConfigurationService.commit();
		userName=username;
		password=passwd;
		proxyAddr=sipProxyAddr;
		proxyPort=sipProxyPort;
		return true;
	}
	public void sipRegister(){
		mSipService.register(SipEngine.this);
	}
	public void sipUnRegister(){
		mSipService.unRegister();
	}
	public boolean isRegistered()
	{
		return mEngine.isStarted() && mSipService.isRegistered();
	}
	public void sipRegEventCallback(SipRegistrationEventTypes event)
	{
	}
	public long prepareCall(String dn){
		String domain = mConfigurationService.getString(NgnConfigurationEntry.NETWORK_PCSCF_HOST, "172.0.0.1");
        final String validUri = NgnUriUtils.makeValidSipUri(String.format("sip:%s@%s", dn, domain));
		if(validUri == null){
			Log.e(TAG, "failed to normalize sip uri '" + dn + "'");
			return 0;
		}
		//NgnAVSession.makeAudioCall(validUri, mSipService.getSipStack());
		NgnAVSession avSession = NgnAVSession.createOutgoingSession(mSipService.getSipStack(), NgnMediaType.Audio);
		avSession.setRemotePartyUri(validUri);
        return avSession.getId();
	}
	public boolean makeCall(long sessId){
		NgnAVSession avSession=NgnAVSession.getSession(sessId);
		if(avSession == null){
			Log.e(TAG, "Invalid session object");
			return false;
		}
		return avSession.makeCall(avSession.getRemotePartyUri());
	}
	public void incomingCall(long sessId){
		
	}
	protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.e(TAG, "OnCreate sipengine");
        
        setVolumeControlStream(AudioManager.STREAM_VOICE_CALL);
		final IntentFilter intentFilter = new IntentFilter();
		intentFilter.addAction(NgnRegistrationEventArgs.ACTION_REGISTRATION_EVENT);
		intentFilter.addAction(NgnInviteEventArgs.ACTION_INVITE_EVENT);
	    registerReceiver(mSipBroadCastRecv, intentFilter);
	}
	
	protected void onDestroy() {
		// Stops the engine
		if(mEngine.isStarted()){
			mEngine.stop();
		}
		// release the listener
		if (mSipBroadCastRecv != null) {
			unregisterReceiver(mSipBroadCastRecv);
			mSipBroadCastRecv = null;
		}
		
		super.onDestroy();
	}

	@Override
	protected void onResume() {
		super.onResume();
		// Starts the engine
		if(!mEngine.isStarted()){
			if(mEngine.start()){
			}
			else{
			}
		}
	}
	public String getConfigUserName(){
		return userName;
	}
	public String getConfigPassword(){
		return password;
	}
	public String getConfigProxyAddr(){
		return proxyAddr;
	}
	public String getConfigProxyPort(){
		return proxyPort;
	}
}

