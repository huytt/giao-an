package com.gala.adapter;

import java.util.ArrayList;

import com.gala.app.R;
import com.gala.customview.CustomViewPagerWrapContent;
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

public class StorePageSlideGridViewProductsPagerAdapter extends PagerAdapter {

	private Activity mActivity;
	private ArrayList<ArrayList<String>> mDataSource;

	// constructor
	public StorePageSlideGridViewProductsPagerAdapter(Activity activity,
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
				R.layout.store_page_layout_slide_non_scrollable_gridview_products_view,
				container, false);

		NonScrollableGridView gridView = (NonScrollableGridView) viewLayout
				.findViewById(R.id.gvProduct);

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
		StorePageGridViewImageProductsAdapter gvadapter = new StorePageGridViewImageProductsAdapter(mActivity,
				mDataSource.get(position), columnWidth);
		
		// setting grid view adapter
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
