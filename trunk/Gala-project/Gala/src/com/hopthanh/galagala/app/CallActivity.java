package com.hopthanh.galagala.app;

import android.app.Fragment;
import android.app.FragmentManager;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ImageButton;

public class CallActivity extends ActionBarActivity {
	
	private static final String TAG = "CallActivity";
	private int mCurrentViewDisplay = -1;
	
	private ImageButton ibtnKeypad;
	private ImageButton ibtnRecent;
	private ImageButton ibtnContact;
	private ImageButton ibtnFavor;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.call_layout);
		
		displayView(0);
		
//		GridLayout glNumbericKey = (GridLayout) findViewById(R.id.glNumbericKey);
		
		ibtnKeypad = (ImageButton) findViewById(R.id.ibtnKeypad);
//		ibtnKeypad.requestFocus();
		ibtnKeypad.setFocusableInTouchMode(true);
		
		ibtnKeypad.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				displayView(0);
				highLightBtnFooter(true, false, false, false);
				ibtnKeypad.requestFocus();
			}
		});

		ibtnRecent = (ImageButton) findViewById(R.id.ibtnRecent);
		ibtnRecent.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				displayView(1);
				highLightBtnFooter(false, true, false, false);
				ibtnRecent.requestFocus();
			}
		});
		
		ibtnContact = (ImageButton) findViewById(R.id.ibtnContact);
		ibtnContact.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				displayView(2);
				highLightBtnFooter(false, false, true, false);
				ibtnContact.requestFocus();
			}
		});
		
		ibtnFavor = (ImageButton) findViewById(R.id.ibtnFavor);
		ibtnFavor.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				displayView(3);
				highLightBtnFooter(false, false, false, true);
				ibtnFavor.requestFocus();
			}
		});

		highLightBtnFooter(true, false, false, false);
		ibtnKeypad.requestFocus();
	}
	
	private void highLightBtnFooter(boolean keypadHighLight, boolean recentHighLight, boolean contactHighLight, boolean favorHighLight) {
		ibtnKeypad.setFocusableInTouchMode(keypadHighLight);
		ibtnRecent.setFocusableInTouchMode(recentHighLight);
		ibtnContact.setFocusableInTouchMode(contactHighLight);
		ibtnFavor.setFocusableInTouchMode(favorHighLight);
	}
	
	private void displayView(int position) {
		// update the main content by replacing fragments
		mCurrentViewDisplay = position;
		Fragment fragment = null;
		switch (position) {
		case 0:
			fragment = new KeyPadFragment();
			break;
		case 1:
			fragment = new RecentFragment();
			break;
		case 2:
			fragment = new ContactFragment();
			break;
		case 3:
			fragment = new FavorFragment();
			break;
//		case 4:
//			fragment = new PagesFragment();
//			break;
//		case 5:
//			fragment = new WhatsHotFragment();
//			break;
		}

		if (fragment != null) {
			FragmentManager fragmentManager = getFragmentManager();
			android.app.FragmentTransaction ft = fragmentManager.beginTransaction();
//			ft.setCustomAnimations(R.anim.slide_in_right, R.anim.slide_out_right);
			ft.replace(R.id.container, fragment);
			ft.commit();
		} else {
			// error in creating fragment
			Log.e(TAG, "Error in creating fragment");
		}
	}
}
