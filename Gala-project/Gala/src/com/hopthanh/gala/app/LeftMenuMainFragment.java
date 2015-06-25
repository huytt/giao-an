package com.hopthanh.gala.app;

import java.util.ArrayList;

import org.javatuples.Quintet;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.gala.app.LeftMenuCategoryFragment.LeftMenuCategoryTitle;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.LayoutLeftMenu;
import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;
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
		
		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
		LayoutLeftMenu temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleHomePage), R.drawable.icon_left_menu_home));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext(), true);
		temp.setDataSource(new MenuDataClass(getString(R.string.titleCategory), R.drawable.icon_left_menu_category));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleBrand), R.drawable.icon_left_menu_brand));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleFavor), R.drawable.icon_left_menu_favor));
		arrLayouts.add(temp);

		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleProfile), R.drawable.icon_left_menu_user));
		arrLayouts.add(temp);

		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleLogin), R.drawable.icon_left_menu_user));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleRegister), R.drawable.icon_left_menu_registry));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleHelp), R.drawable.icon_left_menu_help));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext(), true);
		temp.setDataSource(new MenuDataClass(getString(R.string.titleLang), R.drawable.icon_left_menu_lang));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleTerm), R.drawable.icon_left_menu_registry));
		arrLayouts.add(temp);
		
		temp = new LayoutLeftMenu(getActivity().getApplicationContext());
		temp.setDataSource(new MenuDataClass(getString(R.string.titleSercurity), R.drawable.icon_left_menu_sercurity));
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
			LeftMenuCategoryFragment fragment = new LeftMenuCategoryFragment(0, 0, new LeftMenuCategoryTitle(getString(R.string.titleCategory)));
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
