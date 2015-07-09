package com.hopthanh.gala.layout;

import com.hopthanh.gala.objects.MenuDataClass;
import com.hopthanh.galagala.app.R;
import com.squareup.picasso.Picasso;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

public class LayoutLeftMenuItemFocus<T> extends LayoutLeftMenuItem<T>{

	public LayoutLeftMenuItemFocus(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	public LayoutLeftMenuItemFocus(Context context, T objectHolder) {
		super(context, objectHolder);
		// TODO Auto-generated constructor stub
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		MenuDataClass item  = mDataSource;
		View v = inflater.inflate(R.layout.layout_menu_item_selected, container, false);
		tvMenuItem = (TextView) v.findViewById(R.id.tvMenuItem);
		ImageView icMenuItem = (ImageView) v.findViewById(R.id.icMenuItem);
		
		tvMenuItem.setText(item.getTitle());
		
		if(item.getDrawableIcon() != -1) {
			icMenuItem.setImageResource(item.getDrawableIcon());
		} else if (item.getImgUrl() != null && !item.getImgUrl().equals("")) {
			Picasso.with(mContext).load(item.getImgUrl()).resize(icMenuItem.getLayoutParams().width, icMenuItem.getLayoutParams().height)
			.into(icMenuItem);
		}
		return v;
	}
}
