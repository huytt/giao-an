package com.hopthanh.gala.objects;

import java.util.ArrayList;

import com.hopthanh.galagala.app.GalagalaDroid;

public class MenuDataClass {
	private String mTitle;
	private int mTitleId = -1;
	private int mDrawableIcon;
	private String imgUrl;
	private boolean hasChild = false;
	private ArrayList<MenuDataClass> mChilds = null;
	
	public MenuDataClass () {
		this.mTitle = "";
		this.mDrawableIcon = -1;
		this.imgUrl="";
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public MenuDataClass (String title, int icon) {
		this.mTitle = title;
		this.mDrawableIcon = icon;
		this.imgUrl = "";
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public MenuDataClass (String title, String imgURL) {
		this.mTitle = title;
		this.mDrawableIcon = -1;
		this.imgUrl = imgURL;
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public MenuDataClass (int titleID, int icon) {
		this.mTitleId = titleID;
		this.mTitle = GalagalaDroid.getContext().getString(titleID);
		this.mDrawableIcon = icon;
		this.imgUrl = "";
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public MenuDataClass (int titleID, int icon, boolean hasChild) {
		this.mTitleId = titleID;
		this.mTitle = GalagalaDroid.getContext().getString(titleID);
		this.mDrawableIcon = icon;
		this.imgUrl = "";
		this.hasChild = hasChild;
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public MenuDataClass (String title, int icon, boolean hasChild) {
		this.mTitle = title;
		this.mDrawableIcon = icon;
		this.imgUrl = "";
		this.hasChild = hasChild;
		this.mChilds = new ArrayList<MenuDataClass>();
	}

	public MenuDataClass (String title, String imgURL, boolean hasChild) {
		this.mTitle = title;
		this.mDrawableIcon = -1;
		this.imgUrl = imgURL;
		this.hasChild = hasChild;
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

	public String getImgUrl() {
		return imgUrl;
	}

	public void setImgUrl(String imgUrl) {
		this.imgUrl = imgUrl;
	}

	public boolean isHasChild() {
		return hasChild;
	}

	public void setHasChild(boolean hasChild) {
		this.hasChild = hasChild;
	}

	public int getTitleId() {
		return mTitleId;
	}

	public void setTitleId(int mTitleId) {
		this.mTitleId = mTitleId;
	}
}
