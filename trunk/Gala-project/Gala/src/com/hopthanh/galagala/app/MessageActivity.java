package com.hopthanh.galagala.app;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;

public class MessageActivity extends ActionBarActivity{
	private static String TAG = MessageActivity.class.getCanonicalName();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.message_layout);
		displayView(1);
	}
	
	private void displayView(int position) {
		// update the main content by replacing fragments
		android.support.v4.app.Fragment fragment = null;
		switch (position) {
		case 0:
			fragment = new ChatQueueFragment();
			break;
		case 1:
			fragment = new ChatFragment();
			break;
		}

		if (fragment != null) {
			android.support.v4.app.FragmentManager fragmentManager = getSupportFragmentManager();
			FragmentTransaction ft = fragmentManager.beginTransaction();
			ft.setCustomAnimations(R.anim.slide_in_right, R.anim.slide_out_right);
			ft.replace(R.id.messageContainer, fragment);
			ft.commit();
		} else {
			// error in creating fragment
			Log.e(TAG, "Error in creating fragment");
		}
	}
	
//	private void displayLeftMenuView(AbstractLeftMenuFragment fragment, int styleAnimate) {
//		// update the main content by replacing fragments
//		android.support.v4.app.FragmentManager fragmentManager = getFragmentManager();
//		FragmentTransaction ft = fragmentManager.beginTransaction();
//		switch (styleAnimate) {
//		case NavigationDrawerFragment.SLIDE_LEFT_RIGHT:
//			ft.setCustomAnimations(R.anim.slide_in_left, R.anim.slide_out_left);
//			break;
//		case NavigationDrawerFragment.SLIDE_RIGHT_LEFT:
//			ft.setCustomAnimations(R.anim.slide_in_right, R.anim.slide_out_right);
//			break;
//		}
//		
//		if(fragment instanceof LeftMenuCategoryFragment) {
//			fragment.setDataSource(mCategoryInMenu);
//		}
//		
//		fragment.addListener(this);
//		ft.replace(R.id.containerMenu, fragment);
//		ft.commit();
//	}
}
