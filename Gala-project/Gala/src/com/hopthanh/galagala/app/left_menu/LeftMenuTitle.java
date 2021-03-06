package com.hopthanh.galagala.app.left_menu;

import java.io.Serializable;

public class LeftMenuTitle implements Serializable {
	private String mTitle;
	private LeftMenuTitle mParent = null;
	
	public LeftMenuTitle(String title) {
		mTitle = title;
		mParent = this;
	}

	public LeftMenuTitle(String title, String titleParent) {
		mTitle = title;
		mParent = new LeftMenuTitle(titleParent);
	}

	public LeftMenuTitle(String title, LeftMenuTitle titleParent) {
		mTitle = title;
		mParent = titleParent;
	}

	public String getTitle() {
		return mTitle;
	}
	public void setTitle(String mTitle) {
		this.mTitle = mTitle;
	}
	public LeftMenuTitle getParent() {
		return mParent;
	}
	public void setParent(LeftMenuTitle mParent) {
		this.mParent = mParent;
	}
}
