package com.hopthanh.galagala.app;

import java.util.ArrayList;

import org.apache.http.client.methods.AbortableHttpRequest;

import com.hopthanh.gala.app.actionbar_custom.AbstractActionBarFragment;
import com.hopthanh.gala.app.actionbar_custom.ActionBarFragmentListener;
import com.hopthanh.gala.app.actionbar_custom.ActionBarMainFragment;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewProducts;
import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewBrand;
import com.hopthanh.gala.layout.HomePageLayoutSlideGridViewStores;
import com.hopthanh.gala.layout.HomePageLayoutSlideImageMalls;
import com.hopthanh.gala.layout.StorePageLayoutNormalBanner;
import com.hopthanh.gala.web_api_util.LoadHomePageDataTask;
import com.hopthanh.galagala.app.R;

import android.app.Activity;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBar.LayoutParams;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.util.TypedValue;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.support.v4.view.GravityCompat;
import android.support.v4.view.ViewPager;
import android.support.v4.widget.DrawerLayout;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ArrayAdapter;
import android.widget.GridView;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends ActionBarActivity implements
		NavigationDrawerFragment.NavigationDrawerCallbacks,
		ActionBarFragmentListener,
		WebViewActivityListener {

	/**
	 * Fragment managing the behaviors, interactions and presentation of the
	 * navigation drawer.
	 */
	private NavigationDrawerFragment mNavigationDrawerFragment;

	/**
	 * Used to store the last screen title. For use in
	 * {@link #restoreActionBar()}.
	 */
	private CharSequence mTitle;
	private int mCurrentViewDisplay = -1;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		LanguageManager.getInstance().changeLang(getApplicationContext(), LanguageManager.getInstance().getCurrentLanguage());
		// Prevent auto rotate.
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_NOSENSOR);
		
		// Custom actionbar
		ActionBar mActionBar = getSupportActionBar();
		mActionBar.setDisplayShowHomeEnabled(false);
		mActionBar.setDisplayShowTitleEnabled(false);
		mActionBar.setDisplayHomeAsUpEnabled(false);
		mActionBar.setHomeButtonEnabled(false);
		mActionBar.setDisplayOptions( ActionBar.DISPLAY_SHOW_CUSTOM,  ActionBar.DISPLAY_SHOW_CUSTOM | ActionBar.DISPLAY_SHOW_HOME | ActionBar.DISPLAY_SHOW_TITLE);
		LayoutInflater mInflater = LayoutInflater.from(this);

		View actionbarCus = mInflater.inflate(R.layout.layout_actionbar, null);
		displayActionBarView(new ActionBarMainFragment());

//		ImageButton imageButton = (ImageButton) mCustomView
//				.findViewById(R.id.ibtnMenu);
//		imageButton.setOnClickListener(new OnClickListener() {
//
//			public void onClick(View view) {
////				Toast.makeText(getApplicationContext(), "Refresh Clicked!",
////						Toast.LENGTH_LONG).show();
//				if (mNavigationDrawerFragment != null) {
//					DrawerLayout drawerLayout = mNavigationDrawerFragment.getDrawerLayout();
//					if(drawerLayout != null) {
//						if(mNavigationDrawerFragment.isSlideMenuOpen()) {
//							drawerLayout.closeDrawer(Gravity.START);
//						} else {
//							drawerLayout.openDrawer(Gravity.START);
//						}
//					} else {
//						Log.e(MainActivity.class.getName(), "drawerLayout is null");
//					}
//				} else {
//					Log.e(MainActivity.class.getName(), "mNavigationDrawerFragment is null");
//				}
//			}
//		});
		LayoutParams lp = new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT);
		mActionBar.setCustomView(actionbarCus, lp);
		mActionBar.setDisplayShowCustomEnabled(true);
		
		mNavigationDrawerFragment = (NavigationDrawerFragment) getSupportFragmentManager()
				.findFragmentById(R.id.navigation_drawer);
		mTitle = getTitle();

		// Set up the drawer.
		mNavigationDrawerFragment.setUp(R.id.navigation_drawer,
				(DrawerLayout) findViewById(R.id.drawer_layout));
	}

	private void OpenOrCloseLeftMenu() {
		if (mNavigationDrawerFragment != null) {
			DrawerLayout drawerLayout = mNavigationDrawerFragment.getDrawerLayout();
			if (drawerLayout != null) {
				if (mNavigationDrawerFragment.isSlideMenuOpen()) {
					drawerLayout.closeDrawer(Gravity.START);
				} else {
					drawerLayout.openDrawer(Gravity.START);
				}
			} else {
				Log.e(MainActivity.class.getName(), "drawerLayout is null");
			}
		} else {
			Log.e(MainActivity.class.getName(), "mNavigationDrawerFragment is null");
		}
	}
	
	@Override
	public void onBackPressed() {
		// TODO Auto-generated method stub
		//super.onBackPressed();
//		if(mWebview.canGoBack())
//			mWebview.goBack();
//		else
			super.onBackPressed();
	}
	
	private void displayActionBarView(AbstractActionBarFragment fragment) {
		// update the main content by replacing fragments
		android.support.v4.app.FragmentManager fragmentManager = getSupportFragmentManager();
		FragmentTransaction ft = fragmentManager.beginTransaction();
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
		fragment.addListener(this);
		ft.replace(R.id.containerActionbar, fragment);
		ft.commit();
	}
	
	private void startWebViewActivity(String url) {
		finish();
		Intent webViewIntent = new Intent(this, WebViewActivity.class);
		webViewIntent.putExtra("URL",url);
		startActivity(webViewIntent);
	}
	private void displayView(int position) {
		// update the main content by replacing fragments
		mCurrentViewDisplay = position;
		AbstractLayoutFragment<?> fragment = null;
		switch (position) {
		case 0:
			fragment = new HomePageFragment();
			fragment.addListener(this);
			break;
		case 1:
			break;
		case 2:
//			fragment = new SearchFragment();
			startWebViewActivity("http://galagala.vn:88/Brand.html");
			break;
		case 3:
//			fragment = new CommunityFragment();
			startWebViewActivity("http://galagala.vn:88/WishList.html");
			break;
		case 4:
//			fragment = new PagesFragment();
			startWebViewActivity("http://galagala.vn:88/Profile.html");
			break;
		case 5:
//			fragment = new WhatsHotFragment();
			startWebViewActivity("http://galagala.vn:88/Login.html");
			break;

		case 6:
//			fragment = new WhatsHotFragment();
			startWebViewActivity("http://galagala.vn:88/SignUp.html");
			break;

		case 7:
//			fragment = new WhatsHotFragment();
			break;

		case 8:
//			fragment = new WhatsHotFragment();
			break;

		case 9:
//			fragment = new WhatsHotFragment();
			startWebViewActivity("http://galagala.vn:88/Article/Info/20");
			break;
		case 10:
//			fragment = new WhatsHotFragment();
			startWebViewActivity("http://galagala.vn:88/Article/Info/26");
			break;
		default:
			fragment = new HomePageFragment();
			mCurrentViewDisplay = 0;
			break;
		}

		if (fragment != null) {
			FragmentManager fragmentManager = getSupportFragmentManager();
			fragmentManager
					.beginTransaction()
					.replace(R.id.container,
							fragment).commit();
		} else {
			// error in creating fragment
			Log.e("MainActivity", "Error in creating fragment");
		}
	}
	
	@Override
	public void onNavigationDrawerItemSelected(int position) {
		// update the main content by replacing fragments
		// Currently, Not use. This changed to use the class.
//		FragmentManager fragmentManager = getSupportFragmentManager();
//		fragmentManager
//				.beginTransaction()
//				.replace(R.id.container,
//						PlaceholderFragment.newInstance(position + 1)).commit();
		// Only display view if position changes.
		if (mCurrentViewDisplay != position) {
			displayView(position);	
		}
	}

	public void onSectionAttached(int number) {
		switch (number) {
		case 1:
			mTitle = getString(R.string.titleHomePage);
			break;
		case 2:
			mTitle = getString(R.string.titleCategory);
			break;
		case 3:
			mTitle = getString(R.string.titleSearch);
			break;
		}
	}

	public void restoreActionBar() {
		ActionBar actionBar = getSupportActionBar();
		actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_STANDARD);
		actionBar.setDisplayShowTitleEnabled(true);
		actionBar.setTitle(mTitle);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		if (!mNavigationDrawerFragment.isDrawerOpen()) {
			// Only show items in the action bar relevant to this screen
			// if the drawer is not showing. Otherwise, let the drawer
			// decide what to show in the action bar.
			getMenuInflater().inflate(R.menu.main, menu);
			// Hide all menu items on action bar
			for (int i = 0; i < menu.size(); i++) {
	            menu.getItem(i).setVisible(false);
			}
			restoreActionBar();
			return true;
		}
		return super.onCreateOptionsMenu(menu);
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	/**
	 * A placeholder fragment containing a simple view.
	 */
	public static class PlaceholderFragment extends Fragment {
		/**
		 * The fragment argument representing the section number for this
		 * fragment.
		 */
		private static final String ARG_SECTION_NUMBER = "section_number";

		/**
		 * Returns a new instance of this fragment for the given section number.
		 */
		public static PlaceholderFragment newInstance(int sectionNumber) {
			PlaceholderFragment fragment = new PlaceholderFragment();
			Bundle args = new Bundle();
			args.putInt(ARG_SECTION_NUMBER, sectionNumber);
			fragment.setArguments(args);
			return fragment;
		}

		public PlaceholderFragment() {
		}

		@Override
		public View onCreateView(LayoutInflater inflater, ViewGroup container,
				Bundle savedInstanceState) {
			// Currently, Not use. This changed to use the class.
			View rootView;
	        switch(getArguments().getInt(ARG_SECTION_NUMBER)) {
	            case 1:
	                rootView = inflater.inflate(R.layout.home_page_fragment_main, container, false);
//	                ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
//	        		arrLayouts.add(new LayoutSlideImage());
//	        		arrLayouts.add(new LayoutSlideGridView());
//	        		arrLayouts.add(new LayoutHorizontalScrollViewSpecialStores());
//	        		arrLayouts.add(new LayoutHorizontalScrollView());
//	        		arrLayouts.add(new LayoutHorizontalScrollView());
//	        		arrLayouts.add(new LayoutHorizontalScrollView());
//	        		
//	        		MainContentAdapter mainContentAdapter= new MainContentAdapter(arrLayouts, getActivity());
//	        		
//	        		ListView lsLayoutContainer = (ListView) rootView.findViewById(R.id.lsLayoutContain);
//	        		lsLayoutContainer.setAdapter(mainContentAdapter);
	                break;
	            case 2:
	                rootView = inflater.inflate(R.layout.category_page_fragment_main, container, false);
	                break;
	            case 3:
	                rootView = inflater.inflate(R.layout.search_page_fragment_main, container, false);
	                break;
	            default:
	                rootView = inflater.inflate(R.layout.home_page_fragment_main, container, false);
	        }
//	        return rootView;
//			View rootView = inflater.inflate(R.layout.fragment_main, container,
//					false);
			return rootView;
		}

		@Override
		public void onAttach(Activity activity) {
			super.onAttach(activity);
			((MainActivity) activity).onSectionAttached(getArguments().getInt(
					ARG_SECTION_NUMBER));
		}
	}

	private void restartActivity() {
	    Intent intent = getIntent();
	    finish();
	    startActivity(intent);
	}
	
	@Override
	public void nofityChangedLanguage() {
		// TODO Auto-generated method stub
		restartActivity();
	}

	@Override
	public void notifyUpdateActionBarFragment(AbstractActionBarFragment fragment) {
		// TODO Auto-generated method stub
//		if(fragment instanceof ActionBarSearchFragment) {
//			DrawerLayout drawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
//			drawerLayout.setDrawerShadow(R.drawable.drawer_shadow, GravityCompat.START);
//			drawerLayout.openDrawer(fragment.getView());
//		}
		displayActionBarView(fragment);
	}

	@Override
	public void notifyOpenOrCloseLeftMenu() {
		// TODO Auto-generated method stub
		OpenOrCloseLeftMenu();
	}

	@Override
	public void notifyStartWebViewActivity(String url) {
		// TODO Auto-generated method stub
		startWebViewActivity(url);
	}
}
