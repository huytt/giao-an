package com.hopthanh.gala.adapter;

import java.util.ArrayList;

import com.hopthanh.gala.layout.AbstractLayout;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;

public class MultiLayoutContentListViewAdapter extends BaseAdapter {

	private ArrayList<AbstractLayout> mArrLayouts = null;
	private Activity mContext = null;

	public MultiLayoutContentListViewAdapter(ArrayList<AbstractLayout> arrLayouts, Activity context) {
		this.mArrLayouts = arrLayouts;
		this.mContext = context;
	}

	public Object getLayout(int index) {
		return this.mArrLayouts.get(index);
	}
	
	public void clearAllLayouts() {
		for (int i = 0;i < mArrLayouts.size(); i++) {
			mArrLayouts.get(i).clearDataSource();
		}
		mArrLayouts.clear();
		mArrLayouts = null;
		System.gc();
	}
	
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		if (mArrLayouts == null)
			return 0;
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

//	@Override
//	public int getViewTypeCount() {
//		// TODO Auto-generated method stub
//		// return super.getViewTypeCount();
//		return AbstractLayout.NUM_OF_STYLES;
//	}
//
//	@Override
//	public int getItemViewType(int position) {
//		// TODO Auto-generated method stub
//		//return super.getItemViewType(position);
//		return this.mArrLayouts.get(position).getLayoutType();
//	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View v = convertView;
//		int type = getItemViewType(position);
		Log.d("========huytt=========","=========position======"+position+"==========convertView====="+convertView);
		PreviousViewHolder holder = null;
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (v == null) {
			// Inflate the layout according to the view type for debuging.
//			switch (type) {
//			case AbstractLayout.LAYOUT_TYPE_SLIDE_IMAGE:
//				v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
//				break;
//			case AbstractLayout.LAYOUT_TYPE_SLIDE_GRIDVIEW:
//				v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
//				break;
//			case AbstractLayout.LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW:
//				v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
//				break;
//			case AbstractLayout.LAYOUT_TYPE_NORMAL:
//				v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
//				break;
//			}
			v = this.mArrLayouts.get(position).getView(mContext, inflater, parent);
			holder = new PreviousViewHolder();
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
		int position;
	}
}
