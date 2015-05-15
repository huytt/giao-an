package com.gala.app;

import java.util.ArrayList;

import com.gala.layout.AbstractLayout;
import com.gala.layout.LayoutHorizontalScrollView;
import com.gala.layout.LayoutHorizontalScrollViewSpecialStores;
import com.gala.layout.LayoutSlideGridView;
import com.gala.layout.LayoutSlideImage;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;


public class HomePageFragment extends Fragment {
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		View rootView = inflater.inflate(R.layout.fragment_main, container, false);
		
		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
		arrLayouts.add(new LayoutSlideImage());
		arrLayouts.add(new LayoutSlideGridView());
		arrLayouts.add(new LayoutHorizontalScrollViewSpecialStores());
		arrLayouts.add(new LayoutHorizontalScrollView());
		arrLayouts.add(new LayoutHorizontalScrollView());
		arrLayouts.add(new LayoutHorizontalScrollView());
		
		MainContentAdapter mainContentAdapter= new MainContentAdapter(arrLayouts, getActivity());
		
		ListView lsLayoutContainer = (ListView) rootView.findViewById(R.id.lsLayoutContain);
		lsLayoutContainer.setAdapter(mainContentAdapter);
		return rootView;
	}
}
