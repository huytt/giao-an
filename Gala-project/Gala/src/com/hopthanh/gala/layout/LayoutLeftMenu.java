package com.hopthanh.gala.layout;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.objects.MenuDataClass;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

public class LayoutLeftMenu extends AbstractLayout{

	private TextView tvMenuItem;
	protected boolean mHasChild = false;
	public LayoutLeftMenu(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	public LayoutLeftMenu(Context context, boolean hasChild) {
		super(context);
		// TODO Auto-generated constructor stub
		mHasChild = hasChild;
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		MenuDataClass item  = (MenuDataClass) mDataSource;
		View v = inflater.inflate(R.layout.layout_menu_item, container, false);
		tvMenuItem = (TextView) v.findViewById(R.id.tvMenuItem);
		tvMenuItem.setText(item.getTitle());
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

	public boolean isHasChild() {
		return mHasChild;
	}

	public void setHasChild(boolean mHasChild) {
		this.mHasChild = mHasChild;
	}
}
