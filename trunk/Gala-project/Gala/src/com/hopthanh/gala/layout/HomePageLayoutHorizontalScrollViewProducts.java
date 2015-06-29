package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.app.MainActivity;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.app.WebViewActivity;
import com.hopthanh.gala.app.WebViewActivityListener;
import com.hopthanh.gala.customview.CustomHorizontalLayoutProducts;
import com.hopthanh.gala.objects.ProductInMedia;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.TextView;

public class HomePageLayoutHorizontalScrollViewProducts extends AbstractLayout<ArrayList<ProductInMedia>>{

	private String mTitle = "";
	
	public HomePageLayoutHorizontalScrollViewProducts(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	public HomePageLayoutHorizontalScrollViewProducts(Context context, String tilte) {
		super(context);
		// TODO Auto-generated constructor stub
		mTitle = tilte;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.home_page_layout_horizontal_scroll_view_products, container, false);
		if(!mTitle.isEmpty()){
			TextView tvTitle = (TextView) v.findViewById(R.id.tvTitle);
			tvTitle.setText(mTitle);
		}

		TextView tvViewMore = (TextView) v.findViewById(R.id.tvViewMore);
		tvViewMore.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				((WebViewActivityListener) mListener).notifyStartWebViewActivity("http://galagala.vn:88/Search?typeSearch=1");
			}
		});
		
		CustomHorizontalLayoutProducts chsvProductsLayout = (CustomHorizontalLayoutProducts) v
				.findViewById(R.id.hsvDisplay);
		chsvProductsLayout.addListener(mListener);
		chsvProductsLayout.setDataSource(mDataSource);
		return v;
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		synchronized (this) {
			return OBJECT_TYPE_PRODUCT;
		}
	}

}
