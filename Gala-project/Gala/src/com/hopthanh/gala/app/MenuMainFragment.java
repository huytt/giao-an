package com.hopthanh.gala.app;

import java.util.ArrayList;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.LayoutMenu;
import com.hopthanh.gala.objects.MenuDataClass;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;

public class MenuMainFragment extends AbstractMenuFragment {
//	private static final String TAG = "MenuMainFragment";

	private View mView = null;
	private int mCurrentSelectedPosition = 0;
	private ListView mDrawerListView;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub

		mView = inflater.inflate(R.layout.layout_menu_main, container, false);
		mDrawerListView = (ListView) mView.findViewById(R.id.lsMenu);
		
		mDrawerListView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
					@Override
					public void onItemClick(AdapterView<?> parent, View view,
							int position, long id) {
						selectItem(position);
					}
				});
		
		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
		LayoutMenu temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleHomePage), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleCategory), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleBrand), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleFavor), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleLogin), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleRegister), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleHelp), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleLang), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleTerm), -1));
		arrLayouts.add(temp);
		
		temp = new LayoutMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleSercurity), -1));
		arrLayouts.add(temp);

		MultiLayoutContentListViewAdapter adapter = new MultiLayoutContentListViewAdapter(arrLayouts, getActivity().getApplicationContext());
		
		mDrawerListView.setAdapter(adapter);
		mDrawerListView.setItemChecked(mCurrentSelectedPosition, true);		
		return mView;
	}
	
	private void selectItem(int position) {
		mCurrentSelectedPosition = position;
		if (mDrawerListView != null) {
			mDrawerListView.setItemChecked(position, true);
		}
		
		if (position == 1) {
			MenuChildFragment fragment = new MenuChildFragment();
			mListener.notifyUpdateFragment(fragment, NavigationDrawerFragment.SLIDE_RIGHT_LEFT);
		}
		
		if (position != 1 && position != 7) {
			mListener.notifyDrawerClose();
		}
		
		mListener.notifyNavigationDrawerItemSelected(position);
	}

	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if (mView != null && mView.getParent() != null) {
//			ListView ls = (ListView) mView.findViewById(R.id.lsLayoutContain);
//			((MultiLayoutContentListViewAdapter) ls.getAdapter()).clearAllLayouts();
//			ls = null;
//			mLvLayoutContainer = null;
//			mInflater = null;
//			mContainer = null;
//			mLayoutContain.removeAllViews();
            ((ViewGroup) mView.getParent()).removeView(mView);
            mView = null;
            System.gc();
        }
		super.onDestroyView();
	}
}
