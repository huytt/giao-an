package com.hopthanh.gala.app.left_menu;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.LayoutLeftMenuItem;
import com.hopthanh.gala.layout.LayoutLeftMenuItemFocus;
import com.hopthanh.gala.objects.MenuDataClass;
import com.hopthanh.galagala.app.LanguageDescription;
import com.hopthanh.galagala.app.LanguageManager;
import com.hopthanh.galagala.app.NavigationDrawerFragment;
import com.hopthanh.galagala.app.R;

import java.util.ArrayList;


public class LeftMenuLanguageFragment extends AbstractLeftMenuFragment{
//	private static final String TAG = "HomePageFragment";
	
	public LeftMenuLanguageFragment() {
		super();
	}

	public static LeftMenuLanguageFragment newInstance(Bundle args) {
		LeftMenuLanguageFragment fragment = new LeftMenuLanguageFragment();
		fragment.setArguments(args);
		fragment.updateParams();
		return fragment;
	}

	private void updateParams() {
		mTitle = (LeftMenuTitle) getArguments().getSerializable(AbstractLeftMenuFragment.LEFT_MENU_TITLE);
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.layout_menu_child, container, false);
		
		RelativeLayout rlPrevious = (RelativeLayout) mView.findViewById(R.id.rlPrevious);
		TextView tvLeftMenuTitle = (TextView) mView.findViewById(R.id.tvLeftMenuTitle);
		
		tvLeftMenuTitle.setText(mTitle.getTitle());
		
		rlPrevious.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				mListener.notifyUpdateFragment(new LeftMenuMainFragment(), NavigationDrawerFragment.SLIDE_LEFT_RIGHT);
			}
		});
		
		mDrawerListView = (ListView) mView.findViewById(R.id.lsMenuChild);
		mDrawerListView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				selectItem(position);
			}
		});

		ArrayList<AbstractLayout<?>> arrLayouts = new ArrayList<AbstractLayout<?>>();
		
		for(LanguageDescription langSupport : LanguageManager.LANG_SUPPORTS) {
			if(langSupport.getLangCode().equals(LanguageManager.getInstance().getCurrentLanguage())) {
				LayoutLeftMenuItemFocus<String> temp = new LayoutLeftMenuItemFocus<String>(getActivity().getApplicationContext(), langSupport.getLangCode());
				temp.setDataSource(new MenuDataClass(getString(langSupport.getLangNameId()), langSupport.getIconResId()));
				arrLayouts.add(temp);
			} else {
				LayoutLeftMenuItem<String> temp = new LayoutLeftMenuItem<String>(getActivity().getApplicationContext(), langSupport.getLangCode());
				temp.setDataSource(new MenuDataClass(getString(langSupport.getLangNameId()), langSupport.getIconResId()));
				arrLayouts.add(temp);
			}
		}
		
		MultiLayoutContentListViewAdapter adapter = new MultiLayoutContentListViewAdapter(arrLayouts, getActivity().getApplicationContext());
		mDrawerListView.setAdapter(adapter);
		return mView;
	}
	
	@SuppressWarnings("unchecked")
	private void selectItem(int position) {
		mCurrentSelectedPosition = position;
		if (mDrawerListView != null) {
			mDrawerListView.setItemChecked(position, true);
		}
		
		MultiLayoutContentListViewAdapter adapter = (MultiLayoutContentListViewAdapter) mDrawerListView.getAdapter();
		LayoutLeftMenuItem<String> layout =  (LayoutLeftMenuItem<String>) adapter.getLayout(position);
		String valueObjectHolder = layout.getObjectHolder();
		
		if (!LanguageManager.getInstance().getCurrentLanguage().equals(valueObjectHolder)) {
			// Only update mCurrentLanguage because change language will be call after restarting main activity to avoid change duplicate.
			LanguageManager.getInstance().setCurrentLanguage(valueObjectHolder);
			mListener.nofityChangedLanguage();
			mListener.notifyDrawerClose();
		}

//		mListener.notifyNavigationDrawerItemSelected(position);
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
