package com.hopthanh.gala.app.actionbar_custom;

import android.support.v4.app.Fragment;
import android.view.View;

public abstract class AbstractActionBarFragment extends Fragment{

	protected ActionBarFragmentListener mListener = null;
	protected View mView = null;

	protected Object mDataSource = null;
	
	public AbstractActionBarFragment () {
		super();
	}

	public void addListener(ActionBarFragmentListener mListener) {
		this.mListener = mListener;
	}

	public Object getDataSource() {
		return mDataSource;
	}

	public void setDataSource(Object mDataSource) {
		this.mDataSource = mDataSource;
	}
	
	@Override
	public void onDetach() {
		// TODO Auto-generated method stub
		super.onDetach();
		mListener = null;
	}
}
