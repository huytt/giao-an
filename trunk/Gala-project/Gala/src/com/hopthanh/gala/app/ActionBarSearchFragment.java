package com.hopthanh.gala.app;

import com.hopthanh.gala.app.R;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnFocusChangeListener;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.ImageButton;

public class ActionBarSearchFragment extends AbstractActionBarFragment {
//	private static final String TAG = "MenuMainFragment";
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.layout_actionbar_search_custom, container, false);
		ImageButton ibtnBack = (ImageButton) mView.findViewById(R.id.ibtnBack);
		EditText inputSearch = (EditText) mView.findViewById(R.id.inputSearch);
		
		ibtnBack.setOnClickListener(new OnClickListener() {

			public void onClick(View view) {
				mListener.notifyUpdateActionBarFragment(new ActionBarMainFragment());
			}
		});
		
		inputSearch.setOnFocusChangeListener(new OnFocusChangeListener() {
			
			@Override
			public void onFocusChange(View v, boolean hasFocus) {
				// TODO Auto-generated method stub
				if(v.getId() == R.id.inputSearch && !hasFocus) {

		            InputMethodManager imm =  (InputMethodManager) getActivity().getSystemService(Context.INPUT_METHOD_SERVICE);
		            imm.hideSoftInputFromWindow(v.getWindowToken(), 0);
		        }
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