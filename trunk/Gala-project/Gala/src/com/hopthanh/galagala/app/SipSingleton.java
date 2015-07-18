package com.hopthanh.galagala.app;

import org.doubango.ngn.NgnEngine;
import org.doubango.ngn.events.NgnInviteEventArgs;
import org.doubango.ngn.media.NgnMediaType;
import org.doubango.ngn.services.INgnConfigurationService;
import org.doubango.ngn.services.INgnSipService;
import org.doubango.ngn.sip.NgnAVSession;
import org.doubango.ngn.sip.NgnInviteSession.InviteState;
import org.doubango.ngn.utils.NgnConfigurationEntry;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;
import org.doubango.tinyWRAP.SipStack;
import org.doubango.tinyWRAP.tdav_codec_id_t;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
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

		NgnAVSession avSession = NgnAVSession.createOutgoingSession(mSipService.getSipStack(), NgnMediaType.Audio);
		avSession.setRemotePartyUri(validUri);
        return avSession.getId();
	}

	public boolean makeCall(long sessId){
		NgnAVSession avSession = NgnAVSession.getSession(sessId);
		if(avSession == null){
			Log.e(TAG, "Invalid session object");
			return false;
		}
		return avSession.makeCall(avSession.getRemotePartyUri());
	}

	public boolean acceptCall(NgnAVSession session){
		if(session != null){
			return session.acceptCall();
		}
		return false;
	}

	public boolean hangUpCall(NgnAVSession session) {
		if(session != null){
			return session.hangUpCall();
		}
		return false;
	}
	
	public void in_out_Coming_Call(Context context, long sessId, int typeComing){
		Intent i = new Intent();
		i.setClass(context, InCallActivity.class);
		i.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		i.putExtra(InCallActivity.EXTRAT_SIP_SESSION_ID, sessId);
		i.putExtra(InCallActivity.FRAGMENT_ID, typeComing);
		context.startActivity(i);
		
		switch(typeComing) {
		case InCallActivity.IN_COMING_AUDIO_CALL:
			mEngine.getSoundService().startRingTone();
			break;
		case InCallActivity.OUT_COMING_AUDIO_CALL:
			mEngine.getSoundService().startRingBackTone();
			break;
		}
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

	public void onDestroy(Context context) {
		// Stops the engine
		if(mEngine.isStarted()){
			mEngine.stop();
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
	
//	private String getStateDesc(InviteState state){
//		switch(state){
//			case NONE:
//			default:
//				return "Unknown";
//			case INCOMING:
//				return "Incoming";
//			case INPROGRESS:
//				return "Inprogress";
//			case REMOTE_RINGING:
//				return "Ringing";
//			case EARLY_MEDIA:
//				return "Early media";
//			case INCALL:
//				return "In Call";
//			case TERMINATING:
//				return "Terminating";
//			case TERMINATED:
//				return "termibated";
//		}
//	}
	
//	public void handleSipEvent(Intent intent, NgnAVSession session, Context context){
//		if(session == null){
//			Log.e(TAG, "Invalid session object");
//			return;
//		}
//		final String action = intent.getAction();
//		if(NgnInviteEventArgs.ACTION_INVITE_EVENT.equals(action)){
//			NgnInviteEventArgs args = intent.getParcelableExtra(NgnInviteEventArgs.EXTRA_EMBEDDED);
//			if(args == null){
//				Log.e(TAG, "Invalid event args");
//				return;
//			}
//			if(args.getSessionId() != session.getId()){
//				return;
//			}
//			
//			final InviteState callState = session.getState();
//			switch(callState){
//				case REMOTE_RINGING:
//					mEngine.getSoundService().startRingBackTone();
//					break;
//				case INCOMING:
//					mEngine.getSoundService().startRingTone();
//					Intent i = new Intent();
//					i.setClass(context, InCallActivity.class);
//					i.putExtra(InCallActivity.EXTRAT_SIP_SESSION_ID, session.getId());
//					context.startActivity(i);
//					break;
//				case EARLY_MEDIA:
//				case INCALL:
//					mEngine.getSoundService().stopRingTone();
//					mEngine.getSoundService().stopRingBackTone();
//					session.setSpeakerphoneOn(false);
//					break;
//				case TERMINATING:
//				case TERMINATED:
//					mEngine.getSoundService().stopRingTone();
//					mEngine.getSoundService().stopRingBackTone();
//					((Activity) context).finish();
//					break;
//				default:
//						break;
//			}
//		}
//	}
}
