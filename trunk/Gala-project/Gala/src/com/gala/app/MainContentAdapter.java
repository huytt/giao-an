package com.gala.app;

import java.util.ArrayList;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

public class MainContentAdapter extends BaseAdapter {

	private ArrayList<AbstractLayout> mArrLayouts = null;
	private Activity mContext = null;

	MainContentAdapter(ArrayList<AbstractLayout> arrLayouts, Activity context) {
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
		if (v == null) {
			// Inflate the layout according to the view type
			LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			switch (type) {
			case AppConstant.LAYOUT_TYPE_SLIDE_IMAGE:
				v = inflater.inflate(R.layout.fragment_main_slide_image_malls,
						parent, false);
				break;
			case AppConstant.LAYOUT_TYPE_HORIZONTAL_LIST_2:
				v = inflater.inflate(
						R.layout.fragment_main_horizontal_list_stores, parent,
						false);
				break;
			case AppConstant.LAYOUT_TYPE_HORIZONTAL_LIST_3:
				v = inflater.inflate(
						R.layout.fragment_main_horizontal_list_products,
						parent, false);
				break;
			case AppConstant.LAYOUT_TYPE_SEARCH:
				v = inflater.inflate(
						R.layout.fragment_main_search,
						parent, false);
				break;
			}
		}
		return v;
	}
}
