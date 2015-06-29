package com.hopthanh.gala.app.left_menu;

import java.util.ArrayList;
import java.util.HashMap;

import org.javatuples.Quintet;

import com.hopthanh.gala.app.NavigationDrawerFragmentListener;
import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;

import android.support.v4.app.Fragment;
import android.view.View;
import android.widget.ListView;

public abstract class AbstractLeftMenuFragment extends Fragment{

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
