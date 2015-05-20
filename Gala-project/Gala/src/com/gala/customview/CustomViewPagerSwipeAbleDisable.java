package com.gala.customview;

import android.content.Context;
import android.support.v4.view.ViewPager;
import android.util.AttributeSet;
import android.view.MotionEvent;

public class CustomViewPagerSwipeAbleDisable extends ViewPager{
	private boolean enableSwipe = false;

	public CustomViewPagerSwipeAbleDisable(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
		enableSwipe = true;
	}
	
	public CustomViewPagerSwipeAbleDisable(Context context, AttributeSet attrs) {
		super(context, attrs);
		// TODO Auto-generated constructor stub
		enableSwipe = true;
	}

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		// TODO Auto-generated method stub
		return enableSwipe && super.onTouchEvent(event);
	}
	
	@Override
	public boolean onInterceptTouchEvent(MotionEvent event) {
		// TODO Auto-generated method stub
		return enableSwipe && super.onInterceptTouchEvent(event);
	}

	public boolean isEnableSwipe() {
		return enableSwipe;
	}

	public void setEnableSwipe(boolean enableSwipe) {
		this.enableSwipe = enableSwipe;
	}
}
