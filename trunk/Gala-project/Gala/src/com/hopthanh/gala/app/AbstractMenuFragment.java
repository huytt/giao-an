package com.hopthanh.gala.app;

import android.support.v4.app.Fragment;

public abstract class AbstractMenuFragment extends Fragment{

	protected NavigationDrawerFragmentListener mListener;
	
	public AbstractMenuFragment () {
		super();
	}

	public NavigationDrawerFragmentListener getListener() {
		return mListener;
	}

	public void setListener(NavigationDrawerFragmentListener mListener) {
		this.mListener = mListener;
	}
}
