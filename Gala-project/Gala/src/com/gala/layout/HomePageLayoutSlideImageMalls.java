package com.gala.layout;

import java.util.ArrayList;

import com.gala.adapter.HomePageSlideImageMallsPagerAdapter;
import com.gala.app.R;

import android.app.Activity;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class HomePageLayoutSlideImageMalls extends AbstractLayout{
	
	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_IMAGE;
	}

	@Override
	public View getView(Activity context, LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.home_page_layout_slide_image_malls, container, false);
		
		ViewPager vpImage = (ViewPager) v.findViewById(R.id.vpImage);

		int pos = context.getIntent().getIntExtra("position", 0);

		@SuppressWarnings("unchecked")
		HomePageSlideImageMallsPagerAdapter sliAdapter = new HomePageSlideImageMallsPagerAdapter(context,
				(ArrayList<String>) mDataSource
				);
		vpImage.setAdapter(sliAdapter);
		// displaying selected image first
		vpImage.setCurrentItem(pos);
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}
}
