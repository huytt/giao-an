package com.hopthanh.gala.layout;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;

import com.hopthanh.galagala.app.R;

public class StorePageLayoutNormalDescription extends AbstractLayout<Object>{

	public StorePageLayoutNormalDescription(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_normal_description, container, false);
		WebView wvDescription = (WebView) v.findViewById(R.id.wvDescription);
		String data = "";
		wvDescription.loadDataWithBaseURL(null, data, "text/html", "UTF-8", null);
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
