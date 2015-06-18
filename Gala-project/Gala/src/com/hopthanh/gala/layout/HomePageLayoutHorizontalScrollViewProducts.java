package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomHorizontalLayoutProducts;
import com.hopthanh.gala.objects.ProductInMedia;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class HomePageLayoutHorizontalScrollViewProducts extends AbstractLayout{

	public HomePageLayoutHorizontalScrollViewProducts(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@SuppressWarnings("unchecked")
	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.home_page_layout_horizontal_scroll_view_products, container, false);

		CustomHorizontalLayoutProducts chsvProductsLayout = (CustomHorizontalLayoutProducts) v
				.findViewById(R.id.hsvDisplay);
		chsvProductsLayout.setDataSource((ArrayList<ProductInMedia>) mDataSource);
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
