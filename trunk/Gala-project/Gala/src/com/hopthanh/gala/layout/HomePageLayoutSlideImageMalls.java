package com.hopthanh.gala.layout;

import android.app.Activity;
import android.support.v4.view.ViewPager.OnPageChangeListener;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.hopthanh.gala.adapter.HomePageSlideImageMallsPagerAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.gala.objects.Media;

import java.util.ArrayList;

public class HomePageLayoutSlideImageMalls extends AbstractLayout{
	
	private int mCurrentPosition = 0;
	private CustomViewPagerWrapContent vpImage = null;
	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_IMAGE;
	}

	@Override
	public View getView(Activity context, LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.home_page_layout_slide_image_malls, container, false);
		
		vpImage = (CustomViewPagerWrapContent) v.findViewById(R.id.vpImage);

		@SuppressWarnings("unchecked")
		HomePageSlideImageMallsPagerAdapter sliAdapter = new HomePageSlideImageMallsPagerAdapter(context,
				(ArrayList<Media>) mDataSource
				);
		vpImage.setAdapter(sliAdapter);
		// displaying selected image first
		vpImage.setCurrentItem(mCurrentPosition);
		
		vpImage.setOnPageChangeListener(new OnPageChangeListener() {
			
			@Override
			public void onPageSelected(int position) {
				// TODO Auto-generated method stub
				mCurrentPosition = position;
			}
			
			@Override
			public void onPageScrolled(int arg0, float arg1, int arg2) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void onPageScrollStateChanged(int arg0) {
				// TODO Auto-generated method stub
				
			}
		});
		
		return v;
	}

	public void notifyDataSetChanged() {
		vpImage.getAdapter().notifyDataSetChanged();
		vpImage.invalidate();
	}
	
	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}
}
