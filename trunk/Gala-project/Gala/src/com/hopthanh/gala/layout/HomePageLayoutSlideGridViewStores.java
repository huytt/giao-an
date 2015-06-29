package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.adapter.HomePageSlideGridViewStoresPagerAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.app.WebViewActivity;
import com.hopthanh.gala.app.WebViewActivityListener;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.gala.objects.StoreInMedia;

import android.content.Context;
import android.content.Intent;
import android.graphics.Typeface;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.TextView;

public class HomePageLayoutSlideGridViewStores extends AbstractLayout<ArrayList<ArrayList<StoreInMedia>>>{

	public HomePageLayoutSlideGridViewStores(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_GRIDVIEW;
	}

	@Override
	public View getView(LayoutInflater inflater,
			ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(
				R.layout.home_page_layout_slide_non_scrollable_gridview_stores, container,
				false);
		
//		ArrayList<String> arrtempstore = new ArrayList<String>();
//		
//		for (int i = 0; i < imageStoreObjects.length; i++) {
//			arrtempstore.add(imageStoreObjects[i]);
//		}
//		
//		ArrayList<ArrayList<String>> dataSources = new ArrayList<ArrayList<String>>();
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
	
		// Custom font.
//		TextView tvStoreName = (TextView) v
//				.findViewById(R.id.tvTitle);
//		Typeface custom_font = Typeface.createFromAsset(
//				mContext.getAssets(), "fonts/SFUFUTURABOOK.TTF");
//		tvStoreName.setTypeface(custom_font);
	     
		
		TextView tvViewAll = (TextView) v.findViewById(R.id.tvViewAll);
		tvViewAll.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				((WebViewActivityListener) mListener).notifyStartWebViewActivity("http://galagala.vn:88/Store/All");
			}
		});
		
		CustomViewPagerWrapContent vpGridView = (CustomViewPagerWrapContent) v.findViewById(R.id.vpGridView);
		
		HomePageSlideGridViewStoresPagerAdapter slgvAdapter = new HomePageSlideGridViewStoresPagerAdapter(mContext,
				mDataSource
				);
		slgvAdapter.addListener(mListener);
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
