package com.hopthanh.gala.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.hopthanh.gala.app.R;
import com.hopthanh.object.CallLogClass;

import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class MultiLayoutContentListViewAdapter extends BaseAdapter {

	private Context mContext = null;
	private ArrayList<CallLogClass> mCallLogs = null;

	public MultiLayoutContentListViewAdapter(ArrayList<CallLogClass> arrCallLogs, Context context) {
		this.mContext = context;
		this.mCallLogs = arrCallLogs;
	}

	public Object getLayout(int index) {
		return null;
	}
	
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		if (mCallLogs == null)
			return 0;
		return this.mCallLogs.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return this.mCallLogs.get(position);
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
		PreviousViewHolder holder = null;
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (v == null) {
			v = inflater.inflate(R.layout.call_layout_recent_detail_item, parent, false);
			TextView tvNumberContact = (TextView) v.findViewById(R.id.tvNumberContact);
			TextView tvNumber = (TextView) v.findViewById(R.id.tvNumber);
			TextView tvHour = (TextView) v.findViewById(R.id.tvHour);
			
			String displayName = mCallLogs.get(position).getDisplayName();
			String number = mCallLogs.get(position).getPhNumber();
//			if(!mCallLogs.get(position).isSaved()){
//				number = displayName;
//				displayName = mCallLogs.get(position).getPhNumber();
//			}
			tvNumberContact.setText(displayName);
			tvNumber.setText(number);
			SimpleDateFormat df = new SimpleDateFormat("HH:mm");
			tvHour.setText(df.format(mCallLogs.get(position).getCallDayTime()));
			
			
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
//			v = this.mArrLayouts.get(position).getView(inflater, parent);
//			holder = new PreviousViewHolder();
//			holder.position = position;
//			v.setTag(holder);
		} 
//		else if (((PreviousViewHolder) v.getTag()).position != position) {
//			v = this.mArrLayouts.get(position).getView(inflater, parent);
//			holder = new PreviousViewHolder();
//			holder.position = position;
//			v.setTag(holder);
//		}
		return v;
	}
	
	private class PreviousViewHolder {
		int position;
	}
}
