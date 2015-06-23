package com.hopthanh.gala.objects;

import java.util.ArrayList;

public class MenuDataClass {
	private String mTitle;
	private int mDrawableIcon;
	private ArrayList<MenuDataClass> mChilds = null;
	
	public MenuDataClass () {
		this.mTitle = "";
		this.mDrawableIcon = -1;
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public MenuDataClass (String title, int icon) {
		this.mTitle = title;
		this.mDrawableIcon = icon;
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public String getTitle() {
		return mTitle;
	}
	public void setTitle(String mTitle) {
		this.mTitle = mTitle;
	}
	public int getDrawableIcon() {
		return mDrawableIcon;
	}
	public void setDrawableIcon(int mDrawableIcon) {
		this.mDrawableIcon = mDrawableIcon;
	}
	public ArrayList<MenuDataClass> getChilds() {
		return mChilds;
	}
	public void setChilds(ArrayList<MenuDataClass> mChilds) {
		this.mChilds = mChilds;
	}
}
