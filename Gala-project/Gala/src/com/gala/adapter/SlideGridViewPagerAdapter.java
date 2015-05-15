package com.gala.adapter;

import java.util.ArrayList;

import com.gala.app.R;
import com.gala.app.R.id;
import com.gala.app.R.layout;
import com.gala.customview.CustomViewPager;
import com.gala.customview.NonScrollableGridView;
import com.gala.utils.AppConstant;
import com.gala.utils.Utils;

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

public class SlideGridViewPagerAdapter extends PagerAdapter {

	private Activity mActivity;
	private ArrayList<ArrayList<String>> mDataSource;

	// constructor
	public SlideGridViewPagerAdapter(Activity activity,
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
				R.layout.layout_slide_gridview_view,
				container, false);

		NonScrollableGridView gridView = (NonScrollableGridView) viewLayout
				.findViewById(R.id.gvStores);

		Resources r = mActivity.getResources();
		float padding = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP,
				AppConstant.GRID_PADDING, r.getDisplayMetrics());

		Utils utils = new Utils(mActivity.getApplicationContext());

		int numOfColumns = AppConstant.NUM_OF_COLUMNS_PORTRAIT;
		
		if(mActivity.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
			numOfColumns = AppConstant.NUM_OF_COLUMNS_LANDSCAPE;
		}
		
		int columnWidth = (int) ((utils.getScreenWidth() - ((numOfColumns + 1) * padding)) / numOfColumns);
		//
		gridView.setNumColumns(numOfColumns);
		gridView.setColumnWidth(columnWidth);
		gridView.setStretchMode(GridView.NO_STRETCH);
		gridView.setPadding((int) padding, (int) padding, (int) padding,
				(int) padding);
		gridView.setHorizontalSpacing((int) padding);
		gridView.setVerticalSpacing((int) padding);

		// Gridview adapter
		GridViewImageAdapter gvadapter = new GridViewImageAdapter(mActivity,
				mDataSource.get(position), columnWidth);
		//
		// // setting grid view adapter
		gridView.setAdapter(gvadapter);

		((CustomViewPager) container).addView(viewLayout);
		return viewLayout;
	}

	@Override
	public void destroyItem(ViewGroup container, int position, Object object) {
		((CustomViewPager) container).removeView((LinearLayout) object);

	}

}
