package com.hopthanh.gala.adapter;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.gala.customview.NonScrollableGridView;
import com.hopthanh.gala.utils.AppConstant;
import com.hopthanh.gala.utils.Utils;

import android.app.Activity;
import android.content.Context;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.support.v4.view.PagerAdapter;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.GridView;
import android.widget.LinearLayout;

public class MainSlideGridViewEDirectoryPagerAdapter extends PagerAdapter {

	private Activity mActivity;
	private ArrayList<ArrayList<String>> mDataSource;

	// constructor
	public MainSlideGridViewEDirectoryPagerAdapter(Activity activity,
			ArrayList<ArrayList<String>> imagePaths) {
		this.mActivity = activity;
		this.mDataSource = imagePaths;
	}

	@Override
	public int getCount() {
		return this.mDataSource.size();
	}

	@Override
	public boolean isViewFromObject(View view, Object object) {
		return view == ((LinearLayout) object);
	}

	@Override
	public Object instantiateItem(ViewGroup container, int position) {
		LayoutInflater inflater = (LayoutInflater) mActivity
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View viewLayout = inflater.inflate(
				R.layout.main_layout_slide_non_scrollable_gridview_edirectory_view,
				container, false);

		NonScrollableGridView gridView = (NonScrollableGridView) viewLayout
				.findViewById(R.id.gvEDirectory);

		Resources r = mActivity.getResources();
		float padding = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP,
				AppConstant.GRID_PADDING, r.getDisplayMetrics());

		float spacing = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP,
				AppConstant.GRID_SPACE, r.getDisplayMetrics());
		
		Utils utils = new Utils(mActivity.getApplicationContext());

		int numOfColumns = AppConstant.NUM_OF_COLUMNS_PORTRAIT;
		
		if(mActivity.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
			numOfColumns = AppConstant.NUM_OF_COLUMNS_LANDSCAPE;
		}
		
//		int columnWidth = (int) ((utils.getScreenWidth() - ((numOfColumns + 1) * padding)) / numOfColumns);
		
		int columnWidth = (int) ((utils.getScreenWidth() - 2 * spacing - (numOfColumns - 1) * spacing) / numOfColumns);
		
		gridView.setNumColumns(numOfColumns);
		gridView.setColumnWidth(columnWidth);
		gridView.setStretchMode(GridView.NO_STRETCH);
		gridView.setPadding((int) spacing, (int) padding, (int) spacing,
				(int) padding);
		gridView.setHorizontalSpacing((int) spacing);
		gridView.setVerticalSpacing((int) padding);

		// Gridview adapter
		MainGridViewImageEDirectoryAdapter gvadapter = new MainGridViewImageEDirectoryAdapter(mActivity,
				mDataSource.get(position), columnWidth);
		//
		// // setting grid view adapter
		gridView.setAdapter(gvadapter);

		((CustomViewPagerWrapContent) container).addView(viewLayout);
		return viewLayout;
	}

	@Override
	public void destroyItem(ViewGroup container, int position, Object object) {
		LinearLayout ln = (LinearLayout) object;
		((CustomViewPagerWrapContent) container).removeView(ln);
		ln = null;

	}

}
