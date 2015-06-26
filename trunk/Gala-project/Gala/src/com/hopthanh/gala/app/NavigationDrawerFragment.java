package com.hopthanh.gala.app;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import org.javatuples.Quintet;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.web_api_util.JSONHttpClient;

import android.support.v7.app.ActionBarActivity;
import android.app.Activity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.support.v4.app.ActionBarDrawerToggle;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

/**
 * Fragment used for managing interactions for and presentation of a navigation
 * drawer. See the <a href=
 * "https://developer.android.com/design/patterns/navigation-drawer.html#Interaction"
 * > design guidelines</a> for a complete explanation of the behaviors
 * implemented here.
 */
public class NavigationDrawerFragment extends Fragment implements NavigationDrawerFragmentListener{

	/**
	 * Remember the position of the selected item.
	 */
	private static final String STATE_SELECTED_POSITION = "selected_navigation_drawer_position";

	/**
	 * Per the design guidelines, you should show the drawer on launch until the
	 * user manually expands it. This shared preference tracks this.
	 */
	private static final String PREF_USER_LEARNED_DRAWER = "navigation_drawer_learned";

	private static final String TAG = "NavigationDrawerFragment";

	/**
	 * A pointer to the current callbacks instance (the Activity).
	 */
	private NavigationDrawerCallbacks mCallbacks;

	/**
	 * Helper component that ties the action bar to the navigation drawer.
	 */
	private ActionBarDrawerToggle mDrawerToggle;

	public static final int SLIDE_LEFT_RIGHT = 1;
	public static final int SLIDE_RIGHT_LEFT = 2;
	
	private DrawerLayout mDrawerLayout;
	private ListView mDrawerListView;
	private View mFragmentContainerView;

	private int mCurrentSelectedPosition = 0;
	private boolean mFromSavedInstanceState;
	private boolean mUserLearnedDrawer;
	private boolean mSlideMenuOpen = false;
	
	public NavigationDrawerFragment() {
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		// Read in the flag indicating whether or not the user has demonstrated
		// awareness of the
		// drawer. See PREF_USER_LEARNED_DRAWER for details.
		SharedPreferences sp = PreferenceManager
				.getDefaultSharedPreferences(getActivity());
		mUserLearnedDrawer = sp.getBoolean(PREF_USER_LEARNED_DRAWER, false);

		if (savedInstanceState != null) {
			mCurrentSelectedPosition = savedInstanceState
					.getInt(STATE_SELECTED_POSITION);
			mFromSavedInstanceState = true;
		}

		// Call change language to current lang because this doesn't use MainActivity's context.
		LanguageManager.getInstance().changeLang(getActivity().getApplicationContext(), LanguageManager.getInstance().getCurrentLanguage());
//		mCategoryInMenu = new ArrayList<Quintet<Category,Media,Media,Category_MultiLang,Integer>>();
		mCategoryInMenu = new HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>>();
		new Thread(new Runnable() {
			public void run() {
				loadCategoryInMenu();
			}
		}).start();
		
		// Select either the default item (0) or the last selected item.
		selectItem(mCurrentSelectedPosition);
	}

	@Override
	public void onActivityCreated(Bundle savedInstanceState) {
		super.onActivityCreated(savedInstanceState);
		// Indicate that this fragment would like to influence the set of
		// actions in the action bar.
		setHasOptionsMenu(true);
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		View v = inflater.inflate(R.layout.layout_menu, container, false);
		TextView tvCart = (TextView) v.findViewById(R.id.tvCart);
		tvCart.setText(R.string.leftMenuCart);
		
		LeftMenuMainFragment fragment = new LeftMenuMainFragment();
		displayLeftMenuView(fragment, -1);
		return v;
	}

	private void displayLeftMenuView(AbstractLeftMenuFragment fragment, int styleAnimate) {
		// update the main content by replacing fragments
		android.support.v4.app.FragmentManager fragmentManager = getFragmentManager();
		FragmentTransaction ft = fragmentManager.beginTransaction();
		switch (styleAnimate) {
		case NavigationDrawerFragment.SLIDE_LEFT_RIGHT:
			ft.setCustomAnimations(R.anim.slide_in_left, R.anim.slide_out_left);
			break;
		case NavigationDrawerFragment.SLIDE_RIGHT_LEFT:
			ft.setCustomAnimations(R.anim.slide_in_right, R.anim.slide_out_right);
			break;
		}
		
		if(fragment instanceof LeftMenuCategoryFragment) {
			fragment.setDataSource(mCategoryInMenu);
		}
		
		fragment.setListener(this);
		ft.replace(R.id.containerMenu, fragment);
		ft.commit();
	}
	
	public boolean isDrawerOpen() {
		return mDrawerLayout != null
				&& mDrawerLayout.isDrawerOpen(mFragmentContainerView);
	}

	/**
	 * Users of this fragment must call this method to set up the navigation
	 * drawer interactions.
	 *
	 * @param fragmentId
	 *            The android:id of this fragment in its activity's layout.
	 * @param drawerLayout
	 *            The DrawerLayout containing this fragment's UI.
	 */
	public void setUp(int fragmentId, DrawerLayout drawerLayout) {
		mFragmentContainerView = getActivity().findViewById(fragmentId);
		mDrawerLayout = drawerLayout;

		// set a custom shadow that overlays the main content when the drawer
		// opens
		mDrawerLayout.setDrawerShadow(R.drawable.drawer_shadow,
				GravityCompat.START);
		// set up the drawer's list view with items and click listener

//		ActionBar actionBar = getActionBar();
//		actionBar.setDisplayHomeAsUpEnabled(true);
//		actionBar.setHomeButtonEnabled(true);

		// ActionBarDrawerToggle ties together the the proper interactions
		// between the navigation drawer and the action bar app icon.
		mDrawerToggle = new ActionBarDrawerToggle(getActivity(), /* host Activity */
		mDrawerLayout, /* DrawerLayout object */
		R.drawable.ic_drawer, /* nav drawer image to replace 'Up' caret */
		R.string.navigation_drawer_open, /*
										 * "open drawer" description for
										 * accessibility
										 */
		R.string.navigation_drawer_close /*
										 * "close drawer" description for
										 * accessibility
										 */
		) {
			@Override
			public void onDrawerClosed(View drawerView) {
				super.onDrawerClosed(drawerView);
				mSlideMenuOpen = false;
				if (!isAdded()) {
					return;
				}

				getActivity().supportInvalidateOptionsMenu(); // calls
																// onPrepareOptionsMenu()
			}

			@Override
			public void onDrawerOpened(View drawerView) {
				super.onDrawerOpened(drawerView);
				mSlideMenuOpen = true;
				if (!isAdded()) {
					return;
				}

				if (!mUserLearnedDrawer) {
					// The user manually opened the drawer; store this flag to
					// prevent auto-showing
					// the navigation drawer automatically in the future.
					mUserLearnedDrawer = true;
					SharedPreferences sp = PreferenceManager
							.getDefaultSharedPreferences(getActivity());
					sp.edit().putBoolean(PREF_USER_LEARNED_DRAWER, true)
							.commit();
				}

				getActivity().supportInvalidateOptionsMenu(); // calls
																// onPrepareOptionsMenu()
			}
		};

		// If the user hasn't 'learned' about the drawer, open it to introduce
		// them to the drawer,
		// per the navigation drawer design guidelines.
		if (!mUserLearnedDrawer && !mFromSavedInstanceState) {
			mDrawerLayout.openDrawer(mFragmentContainerView);
		}

		// Defer code dependent on restoration of previous instance state.
		mDrawerLayout.post(new Runnable() {
			@Override
			public void run() {
				mDrawerToggle.syncState();
			}
		});

		mDrawerLayout.setDrawerListener(mDrawerToggle);
	}

	private void selectItem(int position) {
		mCurrentSelectedPosition = position;
		if (mDrawerListView != null) {
			mDrawerListView.setItemChecked(position, true);
		}
		if (mDrawerLayout != null) {
			mDrawerLayout.closeDrawer(mFragmentContainerView);
		}
		if (mCallbacks != null) {
			mCallbacks.onNavigationDrawerItemSelected(position);
		}
	}

	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		try {
			mCallbacks = (NavigationDrawerCallbacks) activity;
		} catch (ClassCastException e) {
			throw new ClassCastException(
					"Activity must implement NavigationDrawerCallbacks.");
		}
	}

	@Override
	public void onDetach() {
		super.onDetach();
		mCallbacks = null;
	}

	@Override
	public void onSaveInstanceState(Bundle outState) {
		super.onSaveInstanceState(outState);
		outState.putInt(STATE_SELECTED_POSITION, mCurrentSelectedPosition);
	}

	@Override
	public void onConfigurationChanged(Configuration newConfig) {
		super.onConfigurationChanged(newConfig);
		// Forward the new configuration the drawer toggle component.
		mDrawerToggle.onConfigurationChanged(newConfig);
	}

	@Override
	public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
		// If the drawer is open, show the global app actions in the action bar.
		// See also
		// showGlobalContextActionBar, which controls the top-left area of the
		// action bar.
		if (mDrawerLayout != null && isDrawerOpen()) {
			inflater.inflate(R.menu.global, menu);
			// Hide all menu items on action bar
			for (int i = 0; i < menu.size(); i++) {
	            menu.getItem(i).setVisible(false);
			}
			showGlobalContextActionBar();
		}
		super.onCreateOptionsMenu(menu, inflater);
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		if (mDrawerToggle.onOptionsItemSelected(item)) {
			return true;
		}

		if (item.getItemId() == R.id.action_example) {
			Toast.makeText(getActivity(), "Example action.", Toast.LENGTH_SHORT)
					.show();
			return true;
		}

		return super.onOptionsItemSelected(item);
	}

	/**
	 * Per the navigation drawer design guidelines, updates the action bar to
	 * show the global app 'context', rather than just what's in the current
	 * screen.
	 */
	private void showGlobalContextActionBar() {
		ActionBar actionBar = getActionBar();
		actionBar.setDisplayShowTitleEnabled(true);
		actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_STANDARD);
		actionBar.setTitle(R.string.app_name);
	}

	private ActionBar getActionBar() {
		return ((ActionBarActivity) getActivity()).getSupportActionBar();
	}

	public boolean isSlideMenuOpen() {
		return mSlideMenuOpen;
	}

	public DrawerLayout getDrawerLayout() {
		return mDrawerLayout;
	}
	
	public ListView getDrawerListView() {
		return mDrawerListView;
	}
	
	public int getCurrentSelectedPosition () {
		return mCurrentSelectedPosition;
	}
	/**
	 * Callbacks interface that all activities using this fragment must
	 * implement.
	 */
	public static interface NavigationDrawerCallbacks {
		/**
		 * Called when an item in the navigation drawer is selected.
		 */
		void onNavigationDrawerItemSelected(int position);
		void nofityChangedLanguage(String newLang);
	}
	
	@Override
	public void notifyUpdateFragment(AbstractLeftMenuFragment fragment, int styleAnimate) {
		// TODO Auto-generated method stub
		displayLeftMenuView(fragment, styleAnimate);
	}

	@Override
	public void notifyDrawerClose() {
		// TODO Auto-generated method stub
		if (mDrawerLayout != null) {
			mDrawerLayout.closeDrawer(mFragmentContainerView);
		}
	}

	@Override
	public void notifyNavigationDrawerItemSelected(int position) {
		// TODO Auto-generated method stub
		if (mCallbacks != null) {
			mCallbacks.onNavigationDrawerItemSelected(position);
		}
	}
	
	private HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>> mCategoryInMenu;
//	private ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>> mCategoryInMenu;
	void loadCategoryInMenu() {
		// Load CategoryInMenu's data.
		String url = "http://galagala.vn:88/home/category_app?lang="+LanguageManager.getInstance().getCurrentLanguage();
		JSONArray jArray;
		try {
			jArray = new JSONArray(JSONHttpClient.getJsonString(url));
			
			mCategoryInMenu.clear();
			for (int i=0; i < jArray.length(); i++)
			{
				JSONObject oneObject = jArray.getJSONObject(i);
				
				String temp = oneObject.getString("Item5");
				int tempCount = 0;
				if (!temp.equals("null")) {
					tempCount = Integer.parseInt(temp);
				}
				
		        Quintet<Category, Media, Media,Category_MultiLang, Integer> item = new Quintet<Category, Media, Media,Category_MultiLang, Integer>(
		        		Category.parseJonData(oneObject.getString("Item1")), 
		        		Media.parseJonData(oneObject.getString("Item2")), 
		        		Media.parseJonData(oneObject.getString("Item3")),
		        		Category_MultiLang.parseJonData(oneObject.getString("Item4")),
		        		tempCount
		        );
		        long parentCateId = item.getValue0().getParentCateId();
		        int categoryLevel = item.getValue0().getCateLevel();
		        if(!mCategoryInMenu.containsKey(categoryLevel)) {
		        	mCategoryInMenu.put(categoryLevel, new HashMap<Long, ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>());
		        }
		        
		        HashMap<Long, ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>> itemChild = mCategoryInMenu.get(categoryLevel);
		        if(!itemChild.containsKey(parentCateId)) {
		        	itemChild.put(parentCateId, new ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>());
		        }
		        
		        itemChild.get(parentCateId).add(item);
//		        mCategoryInMenu.add(item);
			}
			
			for(Integer key:mCategoryInMenu.keySet()) {
				for(Long keyChild:mCategoryInMenu.get(key).keySet()) {
					SortCategoryInMenu(mCategoryInMenu.get(key).get(keyChild));
				}
			}
			
//			SortCategoryInMenu(mCategoryInMenu);
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private void SortCategoryInMenu(ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>> datas) {
		// Sort increase by category level and order number.
		Comparator<Quintet<Category, Media, Media, Category_MultiLang, Integer>> comparator = new Comparator<Quintet<Category, Media, Media, Category_MultiLang, Integer>>() {
			
			@Override
			public int compare(Quintet<Category, Media, Media, Category_MultiLang, Integer> lhs, Quintet<Category, Media, Media, Category_MultiLang, Integer> rhs) {
				// TODO Auto-generated method stub
				int result = Integer.valueOf(lhs.getValue0().getCateLevel()).compareTo(Integer.valueOf(rhs.getValue0().getCateLevel()));

				if(result == 0) {
					result = Integer.valueOf(lhs.getValue0().getOrderNumber()).compareTo(Integer.valueOf(rhs.getValue0().getOrderNumber()));
				}
				
				return result;
			}
		};
			
		Collections.sort(datas, comparator);
	}

	@Override
	public void nofityChangedLanguage(String newLang) {
		// TODO Auto-generated method stub
		mCallbacks.nofityChangedLanguage(newLang);
	}
}
