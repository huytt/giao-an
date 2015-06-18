package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.adapter.HomePageSlideGridViewStoresPagerAdapter;
import com.hopthanh.gala.adapter.StorePageSlideGridViewProductsPagerAdapter;
import com.hopthanh.gala.adapter.StorePageSlideImageProductsPagerAdapter;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;

import android.app.Activity;
import android.content.Context;
import android.database.AbstractWindowedCursor;
import android.graphics.Typeface;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

public class StorePageLayoutSlideGridViewProducts extends AbstractLayout{

	public StorePageLayoutSlideGridViewProducts(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_GRIDVIEW;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(
				R.layout.store_page_layout_slide_non_scrollable_gridview_products, container,
				false);
		
		ArrayList<String> data = (ArrayList<String>) mDataSource;
		ArrayList<ArrayList<String>> dataSources = new ArrayList<ArrayList<String>>();
		ArrayList<String> arrtempProducts = new ArrayList<String>();
		
		int count = 1;
		for (int i = 0; i < data.size(); i++) {
			arrtempProducts.add(data.get(i));
			if(i == AbstractLayout.GIDVIEW_MAX_ITEM * count - 1) {
				dataSources.add(arrtempProducts);
				arrtempProducts = new ArrayList<String>();
				count++;
			}
		}
		
		if (data.size() < AbstractLayout.GIDVIEW_MAX_ITEM * count) {
			dataSources.add(arrtempProducts);
		}
		
		TextView tvStoreName = (TextView) v
				.findViewById(R.id.tvTitle);
		Typeface custom_font = Typeface.createFromAsset(
				mContext.getAssets(), "fonts/SFUFUTURABOOK.TTF");
		tvStoreName.setTypeface(custom_font);
	      
		CustomViewPagerWrapContent vpGridView = (CustomViewPagerWrapContent) v.findViewById(R.id.vpGridView);
		
		StorePageSlideGridViewProductsPagerAdapter slgvAdapter = new StorePageSlideGridViewProductsPagerAdapter(mContext,
				dataSources
				);
		vpGridView.setAdapter(slgvAdapter);
		// displaying selected gridview first
		vpGridView.setCurrentItem(0);
		
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}
}
