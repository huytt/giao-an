package com.hopthanh.gala.app.actionbar_custom;

import com.hopthanh.gala.app.R;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ImageButton;

public class ActionBarMainFragment extends AbstractActionBarFragment {
//	private static final String TAG = "MenuMainFragment";
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.layout_actionbar_main_custom, container, false);
		ImageButton ibtnMenu = (ImageButton) mView.findViewById(R.id.ibtnMenu);
		ibtnMenu.setOnClickListener(new OnClickListener() {

			public void onClick(View view) {
				mListener.notifyOpenOrCloseLeftMenu();
			}
		});
		
		ImageButton ibtnFind = (ImageButton) mView.findViewById(R.id.ibtnFind);
		ibtnFind.setOnClickListener(new OnClickListener() {

			public void onClick(View view) {
				mListener.notifyUpdateActionBarFragment(new ActionBarSearchFragment());
			}
		});

		return mView;
	}
	
	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if (mView != null && mView.getParent() != null) {
//			ListView ls = (ListView) mView.findViewById(R.id.lsLayoutContain);
//			((MultiLayoutContentListViewAdapter) ls.getAdapter()).clearAllLayouts();
//			ls = null;
//			mLvLayoutContainer = null;
//			mInflater = null;
//			mContainer = null;
//			mLayoutContain.removeAllViews();
            ((ViewGroup) mView.getParent()).removeView(mView);
            mView = null;
            System.gc();
        }
		super.onDestroyView();
	}
}
