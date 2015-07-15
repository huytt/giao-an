package com.hopthanh.galagala.app;

import org.doubango.ngn.NgnEngine;
import org.doubango.ngn.media.NgnMediaType;
import org.doubango.ngn.services.INgnConfigurationService;
import org.doubango.ngn.services.INgnSipService;
import org.doubango.ngn.sip.NgnAVSession;
import org.doubango.ngn.utils.NgnConfigurationEntry;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;
import org.doubango.tinyWRAP.SipStack;
import org.doubango.tinyWRAP.tdav_codec_id_t;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.util.Log;

public class SipSingleton{
	private static final String TAG = "SipEngineSingleton";

	private final static String SIP_DOMAIN = "iptel.org";
	private final static String SIP_USERNAME = "huyphuong2223";
	private final static String SIP_PASSWORD = "huyphuong2223";
	private final static String SIP_SERVER_HOST = "iptel.org";
	private final static int SIP_SERVER_PORT = 5060;

	public final static String EXTRAT_SIP_SESSION_ID = "GalaSipSess";
	private static SipSingleton instance;
	
	private NgnEngine mEngine;
	private INgnConfigurationService mConfigurationService;
	private INgnSipService mSipService;
	private BroadcastReceiver mSipBroadCastRecv;

	private SipSingleton() {
		mEngine = NgnEngine.getInstance();
		mConfigurationService = mEngine.getConfigurationService();
		mSipService = mEngine.getSipService();
	}

	public static synchronized SipSingleton getInstance() {
		if (instance == null) {
			instance = new SipSingleton();
		}
		return instance;
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
		
//		mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_IMPI, SIP_USERNAME);
//		mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_IMPU, String.format("sip:%s@%s", SIP_USERNAME, SIP_DOMAIN));
//		mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_PASSWORD, SIP_PASSWORD);
//		mConfigurationService.putString(NgnConfigurationEntry.NETWORK_PCSCF_HOST, SIP_SERVER_HOST);
//		mConfigurationService.putInt(NgnConfigurationEntry.NETWORK_PCSCF_PORT, SIP_SERVER_PORT);
//		mConfigurationService.putString(NgnConfigurationEntry.NETWORK_REALM, SIP_DOMAIN);
//		// VERY IMPORTANT: Commit changes
//		mConfigurationService.commit();

		return true;
	}
	
	public void sipRegister(Context context) {
		mSipService.register(context);
	}
	
	public void sipUnRegister() {
		mSipService.unRegister();
	}
	
	public boolean isRegistered() {
		return mEngine.isStarted() && mSipService.isRegistered();
	}
	
	public long prepareCall(String dn){
		String domain = mConfigurationService.getString(NgnConfigurationEntry.NETWORK_PCSCF_HOST, "127.0.0.1");
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

	public NgnEngine getEngine() {
		return mEngine;
	}

	public INgnConfigurationService getConfigurationService() {
		return mConfigurationService;
	}

	public INgnSipService getSipService() {
		return mSipService;
	}

	public BroadcastReceiver getSipBroadCastRecv() {
		return mSipBroadCastRecv;
	}

	public void setSipBroadCastRecv(BroadcastReceiver mSipBroadCastRecv) {
		this.mSipBroadCastRecv = mSipBroadCastRecv;
	}
	
	public void onDestroy(Context context) {
		// Stops the engine
		if(mEngine.isStarted()){
			mEngine.stop();
		}
		// release the listener
		if (mSipBroadCastRecv != null) {
			context.unregisterReceiver(mSipBroadCastRecv);
			mSipBroadCastRecv = null;
		}
	}

	public void onResume(Context context) {
		// Starts the engine
		if(!mEngine.isStarted()){
			if(mEngine.start()){
				Log.e(TAG, "Engine started :)");
			}
			else{
				Log.e(TAG, "Failed to start the engine :(");
			}
		}
		// Register
		if(mEngine.isStarted()){
			if(!mSipService.isRegistered()){
				// Set credentials
				mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_IMPI, SIP_USERNAME);
				mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_IMPU, String.format("sip:%s@%s", SIP_USERNAME, SIP_DOMAIN));
				mConfigurationService.putString(NgnConfigurationEntry.IDENTITY_PASSWORD, SIP_PASSWORD);
				mConfigurationService.putString(NgnConfigurationEntry.NETWORK_PCSCF_HOST, SIP_SERVER_HOST);
				mConfigurationService.putInt(NgnConfigurationEntry.NETWORK_PCSCF_PORT, SIP_SERVER_PORT);
				mConfigurationService.putString(NgnConfigurationEntry.NETWORK_REALM, SIP_DOMAIN);
				// VERY IMPORTANT: Commit changes
				mConfigurationService.commit();
				// register (log in)
				mSipService.register(context);
			}
		}
	}
}
