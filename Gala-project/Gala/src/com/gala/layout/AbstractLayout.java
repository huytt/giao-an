package com.gala.layout;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public abstract class AbstractLayout {
	public static final int NUM_OF_STYLES = 5;
	public static final int GIDVIEW_MAX_ITEM = 6;
	public static final int LAYOUT_TYPE_SLIDE_IMAGE = 1;
	public static final int LAYOUT_TYPE_SLIDE_GRIDVIEW = 2;
	public static final int LAYOUT_TYPE_SLIDE_LIST_VIEW = 3;
	public static final int LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW = 4;
	public static final int LAYOUT_TYPE_NORMAL = 5;

	
	public static final int OBJECT_TYPE_STORE = 20;
	public static final int OBJECT_TYPE_PRODUCT = 21;
	
	protected Object mDataSource = null;
	
	public abstract View getView(Activity context, LayoutInflater inflater, ViewGroup container);
	public abstract int getLayoutType();
	
	// Temp for test.
	public abstract int getObjectType();
//	public abstract void setDataSource(Object dataSource);
	
	public void setDataSource(Object dataSource) {
		if(dataSource == null) {
			mDataSource = new Object();
		} else {
			mDataSource = dataSource;
		}
	}
	
	public void clearDataSource() {
		mDataSource = null;
		System.gc();
	}
}
