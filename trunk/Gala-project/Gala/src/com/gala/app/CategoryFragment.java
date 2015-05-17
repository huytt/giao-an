package com.gala.app;

import com.gala.customview.CustomViewPager;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;


public class CategoryFragment extends Fragment {
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
//		View rootView = inflater.inflate(R.layout.fragment_main_category, container, false);
		View rootView = inflater.inflate(R.layout.store_page_fragment_main, container, false);
		CustomViewPager vpListLayout = (CustomViewPager) rootView.findViewById(R.id.vpListLayouts);
		
		return rootView;
	}
}
