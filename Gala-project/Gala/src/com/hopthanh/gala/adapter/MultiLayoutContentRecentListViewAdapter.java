package com.hopthanh.gala.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.hopthanh.galagala.app.R;
import com.hopthanh.gala.objects.CallLogClass;
import com.hopthanh.gala.utils.Utils;

import java.text.SimpleDateFormat;
import java.util.ArrayList;

public class MultiLayoutContentRecentListViewAdapter extends BaseAdapter {

	private Context mContext = null;
	private ArrayList<Object> mCallLogs = null;
	private final int DATE_STYLE = 1;
	private final int CALL_LOG_STYLE = 2;
	

	public MultiLayoutContentRecentListViewAdapter(ArrayList<Object> arrCallLogs, Context context) {
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

	@Override
	public int getViewTypeCount() {
		// TODO Auto-generated method stub
		// return super.getViewTypeCount();
		return 3;
	}

	@Override
	public int getItemViewType(int position) {
		// TODO Auto-generated method stub
		//return super.getItemViewType(position);
		if(this.mCallLogs.get(position) instanceof String) {
			return DATE_STYLE;
		} else if (this.mCallLogs.get(position) instanceof CallLogClass) {
			return CALL_LOG_STYLE;
		}
		return -1;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View v = convertView;
		int type = getItemViewType(position);
		PreviousViewHolder holder = null;
		DateStyleViewHolder dateStyleHolder = null;
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (v == null) {
			switch (type) {
			case DATE_STYLE:
				v = inflater.inflate(R.layout.call_layout_recent_date_style, parent, false);
				dateStyleHolder = new DateStyleViewHolder();
				dateStyleHolder.position = position;
				dateStyleHolder.tvDate = (TextView) v.findViewById(R.id.tvDate);
				v.setTag(dateStyleHolder);
				break;
			case CALL_LOG_STYLE:
				v = inflater.inflate(R.layout.call_layout_recent_detail_item, parent, false);
				holder = new PreviousViewHolder();
				holder.tvNumberContact = (TextView) v.findViewById(R.id.tvNumberContact);
				holder.tvNumber = (TextView) v.findViewById(R.id.tvNumber);
				holder.tvHour = (TextView) v.findViewById(R.id.tvHour);
				
				CallLogClass callDetail = (CallLogClass) mCallLogs.get(position);
				String displayName = callDetail.getDisplayName();
				String number = callDetail.getPhNumber();
				if(!callDetail.isSaved()){
					number = displayName;
					displayName = callDetail.getPhNumber();
				}
				holder.tvNumberContact.setText(displayName);
				holder.tvNumber.setText(number);
				SimpleDateFormat df = new SimpleDateFormat("HH:mm");
				holder.tvHour.setText(df.format(callDetail.getCallDayTime()));
				v.setTag(holder);
				break;
			}
		} else {
			switch (type) {
			case DATE_STYLE:
				if (((DateStyleViewHolder) v.getTag()).position != position) {
					v = inflater.inflate(R.layout.call_layout_recent_date_style, parent, false);
					dateStyleHolder = new DateStyleViewHolder();
					dateStyleHolder.position = position;
					dateStyleHolder.tvDate = (TextView) v.findViewById(R.id.tvDate);
					v.setTag(dateStyleHolder);
				} else {
					dateStyleHolder = (DateStyleViewHolder) v.getTag();
				}
				break;
			case CALL_LOG_STYLE:
				if (((PreviousViewHolder) v.getTag()).position != position) {
					v = inflater.inflate(R.layout.call_layout_recent_detail_item, parent, false);
					holder = new PreviousViewHolder();
					holder.tvNumberContact = (TextView) v.findViewById(R.id.tvNumberContact);
					holder.tvNumber = (TextView) v.findViewById(R.id.tvNumber);
					holder.tvHour = (TextView) v.findViewById(R.id.tvHour);
					v.setTag(holder);
				} else {
					holder = (PreviousViewHolder) v.getTag();
				}
				break;
			}
		}

		switch (type) {
		case DATE_STYLE:
			SimpleDateFormat df = new SimpleDateFormat("MM/dd/yyyy");
			String dateString = (String) mCallLogs.get(dateStyleHolder.position);
			if(dateString.equals(df.format(Utils.getToday()))) {
				dateString = "Today";
			} else if(dateString.equals(df.format(Utils.getYesterday()))) {
				dateString = "Yesterday";
			}
			dateStyleHolder.tvDate.setText(dateString);
			break;
		case CALL_LOG_STYLE:
			
			CallLogClass callDetail = (CallLogClass) mCallLogs.get(position);
			String displayName = callDetail.getDisplayName();
			String number = callDetail.getPhNumber();
			if(!callDetail.isSaved()){
				number = displayName;
				displayName = callDetail.getPhNumber();
			}
			holder.tvNumberContact.setText(displayName);
			holder.tvNumber.setText(number);
			SimpleDateFormat dfHM = new SimpleDateFormat("HH:mm");
			holder.tvHour.setText(dfHM.format(callDetail.getCallDayTime()));
			break;
		}

		return v;
	}
	
	private class DateStyleViewHolder {
		int position;
		TextView tvDate;
	}
	
	private class PreviousViewHolder {
		int position;
		TextView tvNumberContact;
		TextView tvNumber;
		TextView tvHour;
	}
}
