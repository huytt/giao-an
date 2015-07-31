package com.hopthanh.galagala.app;

import org.doubango.ngn.sip.NgnAVSession;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;

public class MessageActivity extends ActionBarActivity implements MessageActivityListener{
	private static String TAG = MessageActivity.class.getCanonicalName();
	
	private static final String MESSAGE_ACTIVITY_IS_AVAILABLE = "MessageActivityIsAv";
	public final static int MESSAGE_TAB_FRAGMENT = 0;
	public final static int MESSAGE_FRAGMENT = 1;
	
	private boolean hasBack = false;
	private int mCurrentFragment = MESSAGE_TAB_FRAGMENT;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.message_layout);
        Bundle extras = getIntent().getExtras();
        if(extras != null){
        	mCurrentFragment = extras.getInt("fragmentIndex"); 
        }
		displayView(mCurrentFragment);
	}
	
	private void displayView(int fragmentIndex) {
		// update the main content by replacing fragments
		AbstractLayoutFragment<?> fragment = null;
		android.support.v4.app.FragmentManager fragmentManager = getSupportFragmentManager();
		FragmentTransaction ft = fragmentManager.beginTransaction();
		mCurrentFragment = fragmentIndex;

		switch (fragmentIndex) {
		case MESSAGE_TAB_FRAGMENT:
		default:
			ft.setCustomAnimations(R.anim.slide_in_left, R.anim.slide_out_left);
			fragment = new MessageTabFragment();
			fragment.addListener(this);
			mCurrentFragment = MESSAGE_TAB_FRAGMENT;
			break;
		case MESSAGE_FRAGMENT:
			ft.setCustomAnimations(R.anim.slide_in_right, R.anim.slide_out_right);
			fragment = new MessageFragment();
			hasBack = true;
			break;
		}

		if (fragment != null) {
			ft.replace(R.id.messageContainer, fragment);
			ft.commit();
		} else {
			// error in creating fragment
			Log.e(TAG, "Error in creating fragment");
		}
	}

	@Override
	public void onBackPressed() {
		// TODO Auto-generated method stub
		if(hasBack) {
			displayView(0);
			hasBack = false;
		} else {
			super.onBackPressed();
		}
	}
	
	@Override
	public void onStart() {
		// TODO Auto-generated method stub
		super.onStart();
		
        // Store our shared preference
        SharedPreferences sp = GalagalaDroid.getContext().getSharedPreferences("CommonPrefs", Context.MODE_PRIVATE);
        Editor ed = sp.edit();
        ed.putBoolean(MESSAGE_ACTIVITY_IS_AVAILABLE, true);
        ed.commit();
	}
	
	@Override
	public void onStop() {
		// TODO Auto-generated method stub
		super.onStop();
		
		// Store our shared preference
        SharedPreferences sp = GalagalaDroid.getContext().getSharedPreferences("CommonPrefs", Context.MODE_PRIVATE);
        Editor ed = sp.edit();
        ed.putBoolean(MESSAGE_ACTIVITY_IS_AVAILABLE, false);
        ed.commit();
	}

	public static boolean isAvailable(Context context) {
		return context.getSharedPreferences("CommonPrefs", MODE_PRIVATE).getBoolean(MESSAGE_ACTIVITY_IS_AVAILABLE, false);
	}
	
	@Override
	public void notifyUpdateFragment(int fragmentIndex) {
		// TODO Auto-generated method stub
		displayView(fragmentIndex);
	}
}
