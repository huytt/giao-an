package com.gala.layout;

import java.util.ArrayList;

import com.gala.adapter.SlideGridViewPagerAdapter;
import com.gala.app.R;
import com.gala.customview.CustomViewPager;

import android.app.Activity;
import android.graphics.Typeface;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

public class LayoutSlideGridViewStores extends AbstractLayout{

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_GRIDVIEW;
	}

	@Override
	public View getView(Activity context, LayoutInflater inflater,
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
	
		TextView tvStoreName = (TextView) v
				.findViewById(R.id.tvTitle);
		Typeface custom_font = Typeface.createFromAsset(
				context.getAssets(), "fonts/SFUFUTURABOOK.TTF");
		tvStoreName.setTypeface(custom_font);
	      
		CustomViewPager vpGridView = (CustomViewPager) v.findViewById(R.id.vpGridView);
		int pos = context.getIntent().getIntExtra("position", 0);
		
		@SuppressWarnings("unchecked")
		SlideGridViewPagerAdapter slgvAdapter = new SlideGridViewPagerAdapter(context,
				(ArrayList<ArrayList<String>>) mDataSource
				);
		vpGridView.setAdapter(slgvAdapter);
		// displaying selected gridview first
		vpGridView.setCurrentItem(pos);
		
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}
}
