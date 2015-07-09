package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.adapter.StorePageSlideImageProductsPagerAdapter;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.galagala.app.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class StorePageLayoutSlideImageProducts extends AbstractLayout<ArrayList<String>>{
	
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

		StorePageSlideImageProductsPagerAdapter sliAdapter = new StorePageSlideImageProductsPagerAdapter(mContext,
				mDataSource
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
