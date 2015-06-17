package com.hopthanh.gala.layout;

import java.util.ArrayList;

import org.javatuples.Triplet;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomHorizontalLayoutSpecialStores;
import com.hopthanh.gala.objects.Brand;
import com.hopthanh.gala.objects.Media;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class HomePageLayoutHorizontalScrollViewBrand extends AbstractLayout{

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW;
	}

	@SuppressWarnings("unchecked")
	@Override
	public View getView(Activity context, LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(
				R.layout.home_page_layout_horizontal_scroll_view_special_stores,
				container, false);

		CustomHorizontalLayoutSpecialStores chsvSpecialStoresLayout = (CustomHorizontalLayoutSpecialStores) v
				.findViewById(R.id.hsvDisplay);
		chsvSpecialStoresLayout.setDataSource((ArrayList<Triplet<Brand, Media, Media>>) mDataSource);
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