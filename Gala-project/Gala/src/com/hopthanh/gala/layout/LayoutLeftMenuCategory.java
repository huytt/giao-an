package com.hopthanh.gala.layout;

import com.hopthanh.gala.app.LeftMenuCategoryFragment.LeftMenuCategoryTitle;

import android.content.Context;

public class LayoutLeftMenuCategory extends LayoutLeftMenu{
	private long mCategoryId = 0;
	private long mParentCateId = 0;
	private LeftMenuCategoryTitle mTitle = null;

	public LayoutLeftMenuCategory(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	public LayoutLeftMenuCategory(Context context, boolean hasChild, long categoryId, long parentCateId) {
		super(context, hasChild);
		// TODO Auto-generated constructor stub
		mCategoryId = categoryId;
		mParentCateId = parentCateId;
	}

	public LayoutLeftMenuCategory(Context context, boolean hasChild, long categoryId, long parentCateId, LeftMenuCategoryTitle title) {
		super(context, hasChild);
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

	public LeftMenuCategoryTitle getTitle() {
		return mTitle;
	}

	public void setTitle(LeftMenuCategoryTitle mTitle) {
		this.mTitle = mTitle;
	}
}
