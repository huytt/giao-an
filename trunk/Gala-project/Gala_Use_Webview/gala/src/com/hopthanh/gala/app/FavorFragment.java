package com.hopthanh.gala.app;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class FavorFragment extends Fragment {
	private View mView = null;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.call_layout_favor, container, false);
		return mView;
//		return super.onCreateView(inflater, container, savedInstanceState);
	}

	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if (mView != null && mView.getParent() != null) {
			((ViewGroup) mView.getParent()).removeView(mView);
			mView = null;
			System.gc();
		}

		super.onDestroyView();
	}
}
