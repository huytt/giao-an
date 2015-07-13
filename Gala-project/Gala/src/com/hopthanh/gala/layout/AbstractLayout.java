package com.hopthanh.gala.layout;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public abstract class AbstractLayout<T> {
	
	public static final int NUM_OF_STYLES = 5;
	public static final int GIDVIEW_MAX_ITEM = 6;
	public static final int LAYOUT_TYPE_SLIDE_IMAGE = 1;
	public static final int LAYOUT_TYPE_SLIDE_GRIDVIEW = 2;
	public static final int LAYOUT_TYPE_SLIDE_LIST_VIEW = 3;
	public static final int LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW = 4;
	public static final int LAYOUT_TYPE_NORMAL = 5;

	
	public static final int OBJECT_TYPE_STORE = 20;
	public static final int OBJECT_TYPE_PRODUCT = 21;
	
	protected T mDataSource = null;
	protected Context mContext = null;
	protected Object mListener = null;
	
	public AbstractLayout (Context context) {
		mContext = context;
	}
	
	public void addListener(Object listener) {
		mListener = listener;
	}
	
	public void removeListener(Object listener) {
		mListener = null;
	}

	public abstract View getView(LayoutInflater inflater, ViewGroup container);
	public abstract int getLayoutType();
	
	// Temp for test.
	public abstract int getObjectType();
//	public abstract void setDataSource(Object dataSource);
	
	public T getDataSource() {
		return mDataSource;
	}
	
	public void setDataSource(T dataSource) {
        mDataSource = dataSource;
	}
	
	public void clearDataSource() {
		mDataSource = null;
		System.gc();
	}
	
	@Override
	protected void finalize() throws Throwable {
		// TODO Auto-generated method stub
		clearDataSource();
		mListener = null;
		mContext = null;
		super.finalize();
	}
}
