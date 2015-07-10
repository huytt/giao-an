package com.hopthanh.gala.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.hopthanh.galagala.app.R;
import com.hopthanh.gala.objects.ContactClass;
import com.hopthanh.gala.utils.Utils;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Locale;

public class ContentContactListViewAdapter extends BaseAdapter {

	private Context mContext = null;
	private ArrayList<ContactClass> mContacts = null;
	private ArrayList<ContactClass> mContactsFilter = null;

	public ContentContactListViewAdapter(ArrayList<ContactClass> arrCallLogs, Context context) {
		this.mContext = context;
		this.mContacts = arrCallLogs;
		this.mContactsFilter = new ArrayList<ContactClass>();
		this.mContactsFilter.addAll(this.mContacts);
	}

	public Object getLayout(int index) {
		return null;
	}
	
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		if (mContacts == null)
			return 0;
		return this.mContacts.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return this.mContacts.get(position);
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
//		return 3;
//	}
//
//	@Override
//	public int getItemViewType(int position) {
//		// TODO Auto-generated method stub
//		//return super.getItemViewType(position);
//		if(this.mCallLogs.get(position) instanceof String) {
//			return DATE_STYLE;
//		} else if (this.mCallLogs.get(position) instanceof CallLogClass) {
//			return CALL_LOG_STYLE;
//		}
//		return -1;
//	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View v = convertView;
//		int type = getItemViewType(position);
//		PreviousViewHolder holder = null;
		ViewHolder holder = null;
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (v == null) {
			v = inflater.inflate(R.layout.call_layout_contact_detail_item, parent, false);
			holder = new ViewHolder();
			holder.position = position;
			holder.tvDisplayName = (TextView) v.findViewById(R.id.tvDisplayName);
			holder.imgAvatar = (ImageView) v.findViewById(R.id.imgAvatar);
			v.setTag(holder);
		} else if (((ViewHolder) v.getTag()).position != position) {
			v = inflater.inflate(R.layout.call_layout_contact_detail_item, parent, false);
			holder = new ViewHolder();
			holder.position = position;
			holder.tvDisplayName = (TextView) v.findViewById(R.id.tvDisplayName);
			holder.imgAvatar = (ImageView) v.findViewById(R.id.imgAvatar);
			v.setTag(holder);
		} else {
			holder = (ViewHolder) v.getTag();
		}

		holder.tvDisplayName.setText(mContacts.get(holder.position).getDisplayName());

		return v;
	}
	

	public void filter(String charText) {
		charText = charText.toLowerCase(Locale.getDefault());
		mContacts.clear();
		if (charText.length() == 0) {
			mContacts.addAll(mContactsFilter);
		} 
		else 
		{
			for (ContactClass contact : mContactsFilter) 
			{
				if (contact.getDisplayName().toLowerCase(Locale.getDefault()).contains(charText)) 
				{
					mContacts.add(contact);
				}
			}
		}
		notifyDataSetChanged();
	}
	
	private class ViewHolder {
		int position;
		TextView tvDisplayName;
		ImageView imgAvatar;
	}
//	
//	private class PreviousViewHolder {
//		int position;
//		TextView tvNumberContact;
//		TextView tvNumber;
//		TextView tvHour;
//	}
}
