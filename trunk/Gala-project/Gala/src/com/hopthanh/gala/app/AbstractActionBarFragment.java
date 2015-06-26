package com.hopthanh.gala.app;

import android.support.v4.app.Fragment;
import android.view.View;

public abstract class AbstractActionBarFragment extends Fragment{

	protected ActionBarFragmentListener mListener;
	protected View mView = null;

	protected Object mDataSource = null;
	
	public AbstractActionBarFragment () {
		super();
	}

	public ActionBarFragmentListener getListener() {
		return mListener;
	}

	public void setListener(ActionBarFragmentListener mListener) {
		this.mListener = mListener;
	}

	public Object getDataSource() {
		return mDataSource;
	}

	public void setDataSource(Object mDataSource) {
		this.mDataSource = mDataSource;
	}
}
