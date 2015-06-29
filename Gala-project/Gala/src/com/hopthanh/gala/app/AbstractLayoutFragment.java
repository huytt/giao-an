package com.hopthanh.gala.app;

import android.support.v4.app.Fragment;
import android.view.View;

public abstract class AbstractLayoutFragment<T> extends Fragment{

	protected Object mListener = null;
	protected View mView = null;

	protected T mDataSource = null;
	
	public AbstractLayoutFragment () {
		super();
	}

	public T getDataSource() {
		return mDataSource;
	}

	public void setDataSource(T mDataSource) {
		this.mDataSource = mDataSource;
	}

	public void addListener(Object mWebViewActivityListener) {
		this.mListener = mWebViewActivityListener;
	}
	
	@Override
	public void onDetach() {
		// TODO Auto-generated method stub
		super.onDetach();
		mListener = null;
	}
}
