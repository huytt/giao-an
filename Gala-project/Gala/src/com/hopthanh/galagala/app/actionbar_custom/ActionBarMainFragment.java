package com.hopthanh.galagala.app.actionbar_custom;

import com.hopthanh.gala.utils.Utils;
import com.hopthanh.galagala.app.LanguageManager;
import com.hopthanh.galagala.app.R;

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
		ImageButton ibtnFind = (ImageButton) mView.findViewById(R.id.ibtnFind);
		ImageButton ibtnFav = (ImageButton) mView.findViewById(R.id.ibtnFav);
		ImageButton ibtnCart = (ImageButton) mView.findViewById(R.id.ibtnCart);
		
		ibtnMenu.setOnClickListener(new OnClickListener() {

			public void onClick(View view) {
				mListener.notifyOpenOrCloseLeftMenu();
			}
		});
		
		ibtnFind.setOnClickListener(new OnClickListener() {

			public void onClick(View view) {
				mListener.notifyUpdateActionBarFragment(new ActionBarSearchFragment());
			}
		});

		final String xoneServer = Utils.XONE_SERVER_WEB + "/Home/setLanguage?lang="+ LanguageManager.getInstance().getCurLangName() + "&u=";
		ibtnFav.setOnClickListener(new OnClickListener() {

			public void onClick(View view) {
				String url = xoneServer + "/WishList.html";
//				mListener.notifyStartWebViewActivity("http://galagala.vn:88/WishList.html");
				mListener.notifyStartWebViewActivity(url);
			}
		});
		
		ibtnCart.setOnClickListener(new OnClickListener() {

			public void onClick(View view) {
				String url = xoneServer + "/Cart.html";
//				mListener.notifyStartWebViewActivity("http://galagala.vn:88/Cart.html");
				mListener.notifyStartWebViewActivity(url);
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
