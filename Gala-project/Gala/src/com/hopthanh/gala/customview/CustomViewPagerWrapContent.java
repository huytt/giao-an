package com.hopthanh.gala.customview;

import android.content.Context;
import android.support.v4.view.ViewPager;
import android.util.AttributeSet;
import android.view.View;

public class CustomViewPagerWrapContent extends ViewPager{

	public CustomViewPagerWrapContent(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}
	
	public CustomViewPagerWrapContent(Context context, AttributeSet attrs) {
		super(context, attrs);
		// TODO Auto-generated constructor stub
	}

	@Override
	protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
	    int mode = MeasureSpec.getMode(heightMeasureSpec);
	    // Unspecified means that the ViewPager is in a ScrollView WRAP_CONTENT.
	    // At Most means that the ViewPager is not in a ScrollView WRAP_CONTENT.
	    if (mode == MeasureSpec.UNSPECIFIED || mode == MeasureSpec.AT_MOST) {
	        // super has to be called in the beginning so the child views can be initialized.
	        super.onMeasure(widthMeasureSpec, heightMeasureSpec);
	        int height = 0;
	        for (int i = 0; i < getChildCount(); i++) {
	            View child = getChildAt(i);
	            child.measure(widthMeasureSpec, MeasureSpec.makeMeasureSpec(0, MeasureSpec.UNSPECIFIED));
	            int h = child.getMeasuredHeight();
	            if (h > height) height = h;
	        }
	        heightMeasureSpec = MeasureSpec.makeMeasureSpec(height, MeasureSpec.EXACTLY);
	    }
	    // super has to be called again so the new specs are treated as exact measurements
	    super.onMeasure(widthMeasureSpec, heightMeasureSpec);
	}
	
//	@Override
//	protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
//		// TODO Auto-generated method stub
//		int height = 0;
//	    for(int i = 0; i < getChildCount(); i++) {
//	        View child = getChildAt(i);
//	        child.measure(widthMeasureSpec, MeasureSpec.makeMeasureSpec(0, MeasureSpec.UNSPECIFIED));
//	        int h = child.getMeasuredHeight();
//	        if(h > height) height = h;
//	    }
//	    
//	    if(height > 0) {
//	    	heightMeasureSpec = MeasureSpec.makeMeasureSpec(height, MeasureSpec.EXACTLY);
//	    }
//
//	    super.onMeasure(widthMeasureSpec, heightMeasureSpec);
//	}
}
