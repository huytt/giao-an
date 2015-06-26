package com.hopthanh.gala.app;

import java.util.ArrayList;
import java.util.HashMap;

import org.javatuples.Quintet;

import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;

import android.support.v4.app.Fragment;
import android.view.View;
import android.widget.ListView;

public abstract class AbstractLeftMenuFragment extends Fragment{

	protected NavigationDrawerFragmentListener mListener;
	protected View mView = null;
	protected int mCurrentSelectedPosition = 0;
	protected ListView mDrawerListView;
	protected LeftMenuTitle mTitle = null;

	protected HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>> mDataSource = null;
	
	public AbstractLeftMenuFragment () {
		super();
	}

//	protected void selectItem(int position) {
//		mCurrentSelectedPosition = position;
//		if (mDrawerListView != null) {
//			mDrawerListView.setItemChecked(position, true);
//		}
//		
////		if (position == 1) {
////			MenuChildFragment fragment = new MenuChildFragment(0,0);
////			mListener.notifyUpdateFragment(fragment, NavigationDrawerFragment.SLIDE_RIGHT_LEFT);
////		}
//		
////		if (position != 1 && position != 7) {
////			mListener.notifyDrawerClose();
////		}
//		
//		mListener.notifyNavigationDrawerItemSelected(position);
//	}

	public NavigationDrawerFragmentListener getListener() {
		return mListener;
	}

	public void setListener(NavigationDrawerFragmentListener mListener) {
		this.mListener = mListener;
	}

	public HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>> getDataSource() {
		return mDataSource;
	}

	public void setDataSource(HashMap<Integer, HashMap<Long,ArrayList<Quintet<Category, Media, Media, Category_MultiLang, Integer>>>> mDataSource) {
		this.mDataSource = mDataSource;
	}
}
