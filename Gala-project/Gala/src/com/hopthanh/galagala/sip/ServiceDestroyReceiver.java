package com.hopthanh.galagala.sip;

import org.doubango.ngn.utils.NgnConfigurationEntry;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.util.Log;

public class ServiceDestroyReceiver extends BroadcastReceiver{

	private static final String TAG = "ServiceDestroyReceiver";

	@Override
	public void onReceive(Context context, Intent intent) {
		// TODO Auto-generated method stub
		Log.e(TAG, "ServeiceDestroy onReceive...");
		Log.e(TAG, "action:" + intent.getAction());
		Log.e(TAG, "Starting Service");
		
		SharedPreferences settings = context.getSharedPreferences(NgnConfigurationEntry.SHARED_PREF_NAME, 0);
		if (settings != null && settings.getBoolean(NgnConfigurationEntry.GENERAL_AUTOSTART.toString(), NgnConfigurationEntry.DEFAULT_GENERAL_AUTOSTART)) {
			Intent i = new Intent(context, NativeSipService.class);
			context.startService(i);
		}

	}
}
