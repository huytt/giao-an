package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.adapter.HomePageSlideGridViewStoresPagerAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.gala.objects.StoreInMedia;

import android.content.Context;
import android.graphics.Typeface;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

public class HomePageLayoutSlideGridViewStores extends AbstractLayout{

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
	
		TextView tvStoreName = (TextView) v
				.findViewById(R.id.tvTitle);
		Typeface custom_font = Typeface.createFromAsset(
				mContext.getAssets(), "fonts/SFUFUTURABOOK.TTF");
		tvStoreName.setTypeface(custom_font);
	      
		CustomViewPagerWrapContent vpGridView = (CustomViewPagerWrapContent) v.findViewById(R.id.vpGridView);
		
		@SuppressWarnings("unchecked")
		HomePageSlideGridViewStoresPagerAdapter slgvAdapter = new HomePageSlideGridViewStoresPagerAdapter(mContext,
				(ArrayList<ArrayList<StoreInMedia>>) mDataSource
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
