package com.hopthanh.gala.adapter;

import java.util.ArrayList;

import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.gala.customview.NonScrollableGridView;
import com.hopthanh.gala.objects.StoreInMedia;
import com.hopthanh.gala.utils.AppConstant;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.galagala.app.R;

import android.content.Context;
import android.content.res.Resources;
import android.support.v4.view.PagerAdapter;
import android.util.Log;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.GridView;
import android.widget.LinearLayout;

public class HomePageSlideGridViewStoresPagerAdapter extends PagerAdapter {

	private static final String TAG = "HomePageSlideGridViewStoresPagerAdapter";
	private Context mContext;
	private ArrayList<ArrayList<StoreInMedia>> mDataSource;
	private Object mListener = null;
	private NonScrollableGridView mGridView = null;
	private HomePageGridViewImageStoresAdapter mGvadapter = null;

	// constructor
	public HomePageSlideGridViewStoresPagerAdapter(Context context,
			ArrayList<ArrayList<StoreInMedia>> dataSource) {
		this.mContext = context;
		this.mDataSource = dataSource;
	}

	public void addListener(Object listener) {
		mListener = listener;
	}
	
	public void removeListener() {
		mListener = null;
	}

	@Override
	public int getCount() {
		if(this.mDataSource == null) {
			return 0;
		}
		return this.mDataSource.size();
	}

	@Override
	public boolean isViewFromObject(View view, Object object) {
		return view == ((LinearLayout) object);
	}

	@Override
	public Object instantiateItem(ViewGroup container, int position) {
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View viewLayout = inflater.inflate(
				R.layout.home_page_layout_slide_non_scrollable_gridview_stores_view,
				container, false);

		mGridView = (NonScrollableGridView) viewLayout
				.findViewById(R.id.gvStores);

		Resources r = mContext.getResources();
		float padding = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP,
				AppConstant.GRID_PADDING, r.getDisplayMetrics());

		float spacing = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP,
				AppConstant.GRID_SPACING, r.getDisplayMetrics());
		
		Utils utils = new Utils(mContext.getApplicationContext());

		int numOfColumns = AppConstant.NUM_OF_COLUMNS_PORTRAIT;
		
//		if(mContext.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
//			numOfColumns = AppConstant.NUM_OF_COLUMNS_LANDSCAPE;
//		}
		
//		int columnWidth = (int) ((utils.getScreenWidth() - ((numOfColumns + 1) * padding)) / numOfColumns);
		
		int columnWidth = (int) (((utils.getScreenWidth() - 2*padding - (numOfColumns - 1)*spacing) / numOfColumns) - 6*spacing);
		int columnHigh = (int) columnWidth * 9 / 16;
		//
		mGridView.setNumColumns(numOfColumns);
		mGridView.setColumnWidth(columnWidth);
		mGridView.setStretchMode(GridView.NO_STRETCH);
		mGridView.setPadding((int) padding, (int) padding, (int) padding,
				(int) padding);
//		mGridView.setHorizontalSpacing((int) padding);
//		mGridView.setVerticalSpacing((int) padding);
		
		mGridView.setHorizontalSpacing((int) spacing);
		mGridView.setVerticalSpacing((int) spacing);

		// Gridview adapter
		mGvadapter = new HomePageGridViewImageStoresAdapter(mContext,
				mDataSource.get(position), columnWidth, columnHigh);
		mGvadapter.addListener(mListener);
		//
		// // setting grid view adapter
		mGridView.setAdapter(mGvadapter);

		((CustomViewPagerWrapContent) container).addView(viewLayout);
		return viewLayout;
	}

	@Override
	public void destroyItem(ViewGroup container, int position, Object object) {
		LinearLayout ln = (LinearLayout) object;
		((CustomViewPagerWrapContent) container).removeView(ln);
		ln = null;
	}
	
	public void Destroy() {
		if(mDataSource != null) {
			mDataSource.clear();
			mDataSource = null;
		}
		mListener = null;
		mContext = null;
		if(mGvadapter != null) {
			mGvadapter.Destroy();
			mGvadapter = null;
		}
		mGridView = null;
	}
	
	@Override
	protected void finalize() throws Throwable {
		// TODO Auto-generated method stub
		Log.e(TAG, "finalize is called");
		super.finalize();
	}

}
