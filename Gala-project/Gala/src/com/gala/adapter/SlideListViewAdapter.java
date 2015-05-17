package com.gala.adapter;

import java.util.ArrayList;

import com.devsmart.android.ui.HorizontalListView;
import com.gala.adapter.SlideGridViewPagerAdapter;
import com.gala.adapter.SlideImagePagerAdapter;
import com.gala.app.R;
import com.gala.app.R.id;
import com.gala.app.R.layout;
import com.gala.customview.CustomHorizontalLayoutProducts;
import com.gala.customview.CustomHorizontalLayoutSpecialStores;
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
import android.graphics.Typeface;
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

public class SlideListViewAdapter extends BaseAdapter {

	private ArrayList<AbstractLayout> mArrLayouts = null;
	private Activity mContext = null;

	public SlideListViewAdapter(ArrayList<AbstractLayout> arrLayouts, Activity context) {
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
		return AbstractLayout.NUM_OF_STYLES;
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
		Log.d("========huytt=========","=========position======"+position+"==========convertView====="+convertView);
		PreviousViewHolder holder = null;
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (v == null) {
			// Inflate the layout according to the view type
			switch (type) {
			case AbstractLayout.LAYOUT_TYPE_SLIDE_IMAGE:
				v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
				break;
			case AbstractLayout.LAYOUT_TYPE_SLIDE_GRIDVIEW:
				v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
				break;
			case AbstractLayout.LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW:
				v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
//				v = ((AbstractLayout) getItem(position)).getView(mContext, inflater, parent);
				break;
//			case AppConstant.LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW_SPECIAL_STORES:
//				v = inflater.inflate(
//						R.layout.layout_horizontal_scroll_view_special_stores,
//						parent, false);
//				
//				ArrayList<String> arrtemspecialstores = new ArrayList<String>();
//
//				for (int i = 0; i < imageSpecialStoreObjects.length; i++) {
//					arrtemspecialstores.add(imageSpecialStoreObjects[i]);
//				}
//				
//				CustomHorizontalLayoutSpecialStores chsvSpecialStoresLayout = (CustomHorizontalLayoutSpecialStores) v.findViewById(R.id.hsvDisplay);
//				chsvSpecialStoresLayout.setDataSource(arrtemspecialstores);
//				break;
//			case AppConstant.LAYOUT_TYPE_NORMAL:
//				v = inflater.inflate(
//						R.layout.layout_normal_search,
//						parent, false);
//				break;
			}
			holder = new PreviousViewHolder();
			holder.viewLayout = v;
			holder.position = position;
			v.setTag(holder);
		} 
		else if (((PreviousViewHolder) v.getTag()).position != position) {
			v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
			holder = new PreviousViewHolder();
			holder.position = position;
			v.setTag(holder);
		}
		return v;
	}
	
	private class PreviousViewHolder {
		View viewLayout;
		int position;
	}
//	private static String[] dataObjects = new String[]{
//		"Text #1",
//		"Text #2",
//		"Text #3",
//		"Text #4",
//		"Text #2",
//		"Text #3",
//		"Text #4",
//		"Text #2",
//		"Text #3",
//		"Text #4",
//		"Text #2",
//		"Text #3",
//		"Text #4"
//	}; 
}
