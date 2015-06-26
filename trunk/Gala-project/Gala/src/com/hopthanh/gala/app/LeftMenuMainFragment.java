package com.hopthanh.gala.app;

import java.util.ArrayList;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.LayoutLeftMenuItem;
import com.hopthanh.gala.objects.MenuDataClass;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;

public class LeftMenuMainFragment extends AbstractLeftMenuFragment {
//	private static final String TAG = "MenuMainFragment";
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
		
		ArrayList<AbstractLayout<?>> arrLayouts = new ArrayList<AbstractLayout<?>>();
		MenuDataClass[] dataLeftMenu = {
				new MenuDataClass(getString(R.string.titleHomePage), R.drawable.icon_left_menu_home),
				new MenuDataClass(getString(R.string.titleCategory), R.drawable.icon_left_menu_category, true),
				new MenuDataClass(getString(R.string.titleBrand), R.drawable.icon_left_menu_brand),
				new MenuDataClass(getString(R.string.titleFavor), R.drawable.icon_left_menu_favor),
				new MenuDataClass(getString(R.string.titleProfile), R.drawable.icon_left_menu_user),
				new MenuDataClass(getString(R.string.titleLogin), R.drawable.icon_left_menu_user),
				new MenuDataClass(getString(R.string.titleRegister), R.drawable.icon_left_menu_registry),
				new MenuDataClass(getString(R.string.titleHelp), R.drawable.icon_left_menu_home),
				new MenuDataClass(getString(R.string.titleLang), R.drawable.icon_left_menu_help, true),
				new MenuDataClass(getString(R.string.titleTerm), R.drawable.icon_left_menu_registry),
				new MenuDataClass(getString(R.string.titleSercurity), R.drawable.icon_left_menu_sercurity),
		};
		
		for(MenuDataClass item : dataLeftMenu) {
			LayoutLeftMenuItem<Object> temp = new LayoutLeftMenuItem<Object>(getActivity().getApplicationContext());
			temp.setDataSource(item);
			arrLayouts.add(temp);
		}

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
		
		// 1 - item category; 8 - item language;
		if (position == 1) {
			LeftMenuCategoryFragment fragment = new LeftMenuCategoryFragment(0, 0, new LeftMenuTitle(getString(R.string.titleCategory)));
			mListener.notifyUpdateFragment(fragment, NavigationDrawerFragment.SLIDE_RIGHT_LEFT);
		} else if (position == 8) {
			LeftMenuLanguageFragment fragment = new LeftMenuLanguageFragment(new LeftMenuTitle(getString(R.string.titleLang)), 
					LanguageManager.getInstance(getActivity().getApplicationContext()).getCurrentLanguage());
			mListener.notifyUpdateFragment(fragment, NavigationDrawerFragment.SLIDE_RIGHT_LEFT);			
		}
		
		if (position != 1 && position != 8) {
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
