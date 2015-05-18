package com.gala.layout;

import java.util.ArrayList;

import com.gala.adapter.HomePageSlideImageMallsPagerAdapter;
import com.gala.adapter.StorePageSlideImageProductsPagerAdapter;
import com.gala.adapter.StorePageSlideListViewLayoutPagerAdapter;
import com.gala.app.R;
import com.gala.customview.CustomViewPagerWrapContent;

import android.app.Activity;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class StorePageLayoutSlideImageProducts extends AbstractLayout{
	
	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_IMAGE;
	}

	@Override
	public View getView(Activity context, LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_slide_image_products, container, false);
		
		CustomViewPagerWrapContent vpImage = (CustomViewPagerWrapContent) v.findViewById(R.id.vpImage);

		int pos = context.getIntent().getIntExtra("position", 0);

		@SuppressWarnings("unchecked")
		StorePageSlideImageProductsPagerAdapter sliAdapter = new StorePageSlideImageProductsPagerAdapter(context,
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
