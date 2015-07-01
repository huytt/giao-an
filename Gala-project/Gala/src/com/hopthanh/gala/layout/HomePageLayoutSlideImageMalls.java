package com.hopthanh.gala.layout;

import android.content.Context;
import android.support.v4.view.ViewPager.OnPageChangeListener;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.hopthanh.gala.adapter.HomePageSlideImageMallsPagerAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.gala.objects.Media;

import java.util.ArrayList;

public class HomePageLayoutSlideImageMalls extends AbstractLayout<ArrayList<Media>>{
	
	public HomePageLayoutSlideImageMalls(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	private int mCurrentPosition = 1;
	private CustomViewPagerWrapContent vpImage = null;
	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_SLIDE_IMAGE;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.home_page_layout_slide_image_malls, container, false);
		
		vpImage = (CustomViewPagerWrapContent) v.findViewById(R.id.vpImage);

		if(mDataSource != null && mDataSource.size() > 2) {
			mDataSource.add(0, mDataSource.get(mDataSource.size() - 1));
			mDataSource.add(mDataSource.get(1));
		}
		
		HomePageSlideImageMallsPagerAdapter sliAdapter = new HomePageSlideImageMallsPagerAdapter(mContext,
				mDataSource
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
			public void onPageScrollStateChanged(int state) {
				// TODO Auto-generated method stub
				if (state == CustomViewPagerWrapContent.SCROLL_STATE_IDLE) {
					int pageCount = mDataSource.size();
	                if (mCurrentPosition == 0){
	                	vpImage.setCurrentItem(pageCount-2, false);
	                } else if (mCurrentPosition == pageCount-1){
	                	vpImage.setCurrentItem(1, false);
	                }				
				}
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
