package com.gala.layout;

import java.util.ArrayList;

import com.gala.adapter.MultiLayoutContentListViewAdapter;
import com.gala.app.R;
import com.gala.customview.NonScrollableListView;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.widget.ListView;

public class StorePageLayoutNormalContact extends AbstractLayout{

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(Activity context, LayoutInflater inflater,
			ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_normal_contact, container, false);
		
		// Load Address
		NonScrollableListView lsAddress = (NonScrollableListView) v.findViewById(R.id.lsAddress);
		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
		StorePageLayoutNormalAddress layoutContact = new StorePageLayoutNormalAddress();
		arrLayouts.add(layoutContact);
		arrLayouts.add(layoutContact);
		arrLayouts.add(layoutContact);
		
		MultiLayoutContentListViewAdapter adapter = new MultiLayoutContentListViewAdapter(arrLayouts, context);
		lsAddress.setAdapter(adapter);
		
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
