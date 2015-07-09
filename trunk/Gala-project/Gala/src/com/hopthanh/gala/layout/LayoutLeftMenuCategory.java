package com.hopthanh.gala.layout;

import com.hopthanh.galagala.app.left_menu.LeftMenuTitle;

import android.content.Context;

public class LayoutLeftMenuCategory extends LayoutLeftMenuItem<Object>{
	private long mCategoryId = 0;
	private long mParentCateId = 0;
	private LeftMenuTitle mTitle = null;

	public LayoutLeftMenuCategory(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	public LayoutLeftMenuCategory(Context context, long categoryId, long parentCateId) {
		super(context);
		// TODO Auto-generated constructor stub
		mCategoryId = categoryId;
		mParentCateId = parentCateId;
	}

	public LayoutLeftMenuCategory(Context context, long categoryId, long parentCateId, LeftMenuTitle title) {
		super(context);
		// TODO Auto-generated constructor stub
		mCategoryId = categoryId;
		mParentCateId = parentCateId;
		mTitle = title;
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	public long getCategoryId() {
		return mCategoryId;
	}

	public void setCategoryId(long mCategoryId) {
		this.mCategoryId = mCategoryId;
	}

	public long getParentCateId() {
		return mParentCateId;
	}

	public void setParentCateId(long mParentCateId) {
		this.mParentCateId = mParentCateId;
	}

	public LeftMenuTitle getTitle() {
		return mTitle;
	}

	public void setTitle(LeftMenuTitle mTitle) {
		this.mTitle = mTitle;
	}
}
