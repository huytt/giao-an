package com.hopthanh.gala.layout;

import java.util.ArrayList;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.gala.customview.NonScrollableListView;
import com.hopthanh.galagala.app.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class StorePageLayoutNormalContact extends AbstractLayout<Object>{

	public StorePageLayoutNormalContact(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_normal_contact, container, false);
		
		// Load Address
		NonScrollableListView lsAddress = (NonScrollableListView) v.findViewById(R.id.lsAddress);
		ArrayList<AbstractLayout<?>> arrLayouts = new ArrayList<AbstractLayout<?>>();
		StorePageLayoutNormalAddress layoutContact = new StorePageLayoutNormalAddress(mContext);
		arrLayouts.add(layoutContact);
		arrLayouts.add(layoutContact);
		arrLayouts.add(layoutContact);
		
		MultiLayoutContentListViewAdapter adapter = new MultiLayoutContentListViewAdapter(arrLayouts, mContext);
		lsAddress.setAdapter(adapter);
		
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
