package com.hopthanh.gala.layout;

import java.util.ArrayList;

import org.javatuples.Triplet;

import com.hopthanh.gala.customview.CustomHorizontalLayoutSpecialStores;
import com.hopthanh.gala.objects.Brand;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.galagala.app.LanguageManager;
import com.hopthanh.galagala.app.R;
import com.hopthanh.galagala.app.WebViewActivity;
import com.hopthanh.galagala.app.WebViewActivityListener;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.TextView;

public class HomePageLayoutHorizontalScrollViewBrand extends AbstractLayout<ArrayList<Triplet<Brand, Media, Media>>>{

	public HomePageLayoutHorizontalScrollViewBrand(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(
				R.layout.home_page_layout_horizontal_scroll_view_special_stores,
				container, false);
		
		TextView tvViewAll = (TextView) v.findViewById(R.id.tvViewAll);
		tvViewAll.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				String xoneServer = Utils.XONE_SERVER_WEB + "/Home/setLanguage?lang="+ LanguageManager.getInstance().getCurLangName() + "&u=";
				String url = xoneServer + "/Brand.html";
//				((WebViewActivityListener) mListener).notifyStartWebViewActivity("http://galagala.vn:88/Brand.html");
				((WebViewActivityListener) mListener).notifyStartWebViewActivity(url);
			}
		});

		CustomHorizontalLayoutSpecialStores chsvSpecialStoresLayout = (CustomHorizontalLayoutSpecialStores) v
				.findViewById(R.id.hsvDisplay);
		chsvSpecialStoresLayout.addListener(mListener);
		chsvSpecialStoresLayout.setDataSource(mDataSource);
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		synchronized (this) {
			return OBJECT_TYPE_STORE;
		}
	}
}
