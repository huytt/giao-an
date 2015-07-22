package com.hopthanh.galagala.app;

import org.doubango.ngn.NgnApplication;

import android.util.Log;

public class GalagalaDroid extends NgnApplication{
	private final static String TAG = GalagalaDroid.class.getCanonicalName();
	
	public GalagalaDroid() {
    	Log.e(TAG,"GalagalaDroid()");
    }
	
	@Override
	public void onCreate() {
		// TODO Auto-generated method stub
		super.onCreate();
		LanguageManager.getInstance().init(getApplicationContext());
	}
}
