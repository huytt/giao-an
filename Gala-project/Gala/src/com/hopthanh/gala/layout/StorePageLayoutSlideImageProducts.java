package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.adapter.HomePageSlideImageMallsPagerAdapter;
import com.hopthanh.gala.adapter.StorePageSlideImageProductsPagerAdapter;
import com.hopthanh.gala.adapter.StorePageSlideListViewLayoutPagerAdapter;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;

import android.app.Activity;
import android.content.Context;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class StorePageLayoutSlideImageProducts extends AbstractLayout{
	
	public StorePageLayoutSlideImageProducts(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_IMAGE;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_slide_image_products, container, false);
		
		CustomViewPagerWrapContent vpImage = (CustomViewPagerWrapContent) v.findViewById(R.id.vpImage);

		@SuppressWarnings("unchecked")
		StorePageSlideImageProductsPagerAdapter sliAdapter = new StorePageSlideImageProductsPagerAdapter(mContext,
				(ArrayList<String>) mDataSource
				);
		vpImage.setAdapter(sliAdapter);
		// displaying selected image first
		vpImage.setCurrentItem(0);
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}
}
