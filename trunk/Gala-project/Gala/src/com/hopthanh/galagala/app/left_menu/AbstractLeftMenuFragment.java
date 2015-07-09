package com.hopthanh.galagala.app.left_menu;

import android.support.v4.app.Fragment;
import android.view.View;
import android.widget.ListView;

import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.galagala.app.NavigationDrawerFragmentListener;

import org.javatuples.Quintet;

import java.util.ArrayList;
import java.util.HashMap;

public abstract class AbstractLeftMenuFragment extends Fragment{

	public static final String CATEGORY_LEVEL = "cateLevel";
	public static final String CATEGORY_PARENT_ID = "cateParentId";
	public static final String CATEGORY_PARENT_PRE_ID = "catePreParentId";
	public static final String LEFT_MENU_TITLE = "leftMenuTitle";


	protected NavigationDrawerFragmentListener mListener = null;
	protected View mView = null;
	protected int mCurrentSelectedPosition = 0;
	protected ListView mDrawerListView;
	protected LeftMenuTitle mTitle = null;

	protected HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>> mDataSource = null;

	public AbstractLeftMenuFragment () {
		super();
	}

	public void addListener(NavigationDrawerFragmentListener mListener) {
		this.mListener = mListener;
	}

	public HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>> getDataSource() {
		return mDataSource;
	}

	public void setDataSource(HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>> mDataSource) {
		this.mDataSource = mDataSource;
	}

	@Override
	public void onDetach() {
		// TODO Auto-generated method stub
		super.onDetach();
		mListener = null;
	}
}
