package com.gala.app;

import java.util.ArrayList;

import com.devsmart.android.ui.HorizontalListView;
import com.gala.adapter.SlideGridViewPagerAdapter;
import com.gala.adapter.SlideImagePagerAdapter;
import com.gala.app.R;
import com.gala.app.R.id;
import com.gala.app.R.layout;
import com.gala.customview.CustomHorizontalLayoutProducts;
import com.gala.customview.CustomViewPager;
import com.gala.layout.AbstractLayout;
import com.gala.utils.AppConstant;
import com.gala.utils.Utils;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.GridView;
import android.widget.HorizontalScrollView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public class MainContentAdapter extends BaseAdapter {

	private ArrayList<AbstractLayout> mArrLayouts = null;
	private Activity mContext = null;

	public MainContentAdapter(ArrayList<AbstractLayout> arrLayouts, Activity context) {
		this.mArrLayouts = arrLayouts;
		this.mContext = context;
	}

	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return this.mArrLayouts.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return this.mArrLayouts.get(position);
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	@Override
	public int getViewTypeCount() {
		// TODO Auto-generated method stub
		// return super.getViewTypeCount();
		return AppConstant.NUM_OF_STYLES;
	}

	@Override
	public int getItemViewType(int position) {
		// TODO Auto-generated method stub
		//return super.getItemViewType(position);
		return this.mArrLayouts.get(position).getLayoutType();
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View v = convertView;
		int type = getItemViewType(position);
		int pos = 0;
		Utils utils = new Utils(mContext.getApplicationContext());
		if (v == null) {
			// Inflate the layout according to the view type
			LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			switch (type) {
			case AppConstant.LAYOUT_TYPE_SLIDE_IMAGE:
				v = inflater.inflate(R.layout.layout_slide_image_malls,
						parent, false);
				
				ViewPager vpImage = (ViewPager) v.findViewById(R.id.vpImage);

				pos = mContext.getIntent().getIntExtra("position", 0);

				ArrayList<String> arrtemp = new ArrayList<String>();
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg");
				
				SlideImagePagerAdapter sliAdapter = new SlideImagePagerAdapter(mContext,
						arrtemp
						);
				vpImage.setAdapter(sliAdapter);
				// displaying selected image first
				vpImage.setCurrentItem(pos);
				break;
			case AppConstant.LAYOUT_TYPE_SLIDE_GRIDVIEW:
				v = inflater.inflate(
						R.layout.layout_slide_gridview_stores, parent,
						false);
				
				ArrayList<String> arrtempstore = new ArrayList<String>();
				
				for (int i = 0; i < imageStoreObjects.length; i++) {
					arrtempstore.add(imageStoreObjects[i]);
				}
				
				ArrayList<ArrayList<String>> dataSources = new ArrayList<ArrayList<String>>();
				dataSources.add(arrtempstore);
				dataSources.add(arrtempstore);
				dataSources.add(arrtempstore);
				dataSources.add(arrtempstore);
				dataSources.add(arrtempstore);
				dataSources.add(arrtempstore);
			
				CustomViewPager vpGridView = (CustomViewPager) v.findViewById(R.id.vpGridView);
				pos = mContext.getIntent().getIntExtra("position", 0);
				
				SlideGridViewPagerAdapter slgvAdapter = new SlideGridViewPagerAdapter(mContext,
						dataSources
						);
				vpGridView.setAdapter(slgvAdapter);
				// displaying selected gridview first
				vpGridView.setCurrentItem(pos);
				break;
			case AppConstant.LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW:
				v = inflater.inflate(
						R.layout.layout_horizontal_scroll_view_products,
						parent, false);
				
				ArrayList<String> arrtemsproduct = new ArrayList<String>();

				for (int i = 0; i < imageProductObjects.length; i++) {
					arrtemsproduct.add(imageProductObjects[i]);
				}
				
				CustomHorizontalLayoutProducts chsvProductsLayout = (CustomHorizontalLayoutProducts) v.findViewById(R.id.hsvDisplay);
				chsvProductsLayout.setDataSource(arrtemsproduct);
				break;
			case AppConstant.LAYOUT_TYPE_NORMAL:
				v = inflater.inflate(
						R.layout.fragment_main_search,
						parent, false);
				break;
			}
		}
		return v;
	}
	
	private static String[] dataObjects = new String[]{
		"Text #1",
		"Text #2",
		"Text #3",
		"Text #4",
		"Text #2",
		"Text #3",
		"Text #4",
		"Text #2",
		"Text #3",
		"Text #4",
		"Text #2",
		"Text #3",
		"Text #4"
	}; 
	
	private static String[] imageStoreObjects = new String[] {
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif" ,
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg"
	};
	
	private static String[] imageProductObjects = new String[]{ 
		"http://galagala.vn:8888//Media/Store/S000008/Product/P00000013/20150509_STORE-3_b3a54d79-1720-44d8-8fbc-30732a3e5a2f.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000008/Hana-Store-PHB-D1-VayVOK001-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/Product/P00000013/20150509_STORE-3_b3a54d79-1720-44d8-8fbc-30732a3e5a2f.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000008/Hana-Store-PHB-D1-VayVOK001-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg"
	};
}
