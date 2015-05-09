package com.gala.app;

import java.util.ArrayList;

import com.devsmart.android.ui.HorizontalListView;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

public class MainContentAdapter extends BaseAdapter {

	private ArrayList<AbstractLayout> mArrLayouts = null;
	private Activity mContext = null;

	MainContentAdapter(ArrayList<AbstractLayout> arrLayouts, Activity context) {
		this.mArrLayouts = arrLayouts;
		this.mContext = context;
	}

	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return this.mArrLayouts.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return this.mArrLayouts.get(position);
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	@Override
	public int getViewTypeCount() {
		// TODO Auto-generated method stub
		// return super.getViewTypeCount();
		return AppConstant.NUM_OF_STYLES;
	}

	@Override
	public int getItemViewType(int position) {
		// TODO Auto-generated method stub
		//return super.getItemViewType(position);
		return this.mArrLayouts.get(position).getLayoutType();
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View v = convertView;
		int type = getItemViewType(position);
		if (v == null) {
			// Inflate the layout according to the view type
			LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			switch (type) {
			case AppConstant.LAYOUT_TYPE_SLIDE_IMAGE:
				v = inflater.inflate(R.layout.fragment_main_slide_image_malls,
						parent, false);
				
				ViewPager viewPager = (ViewPager) v.findViewById(R.id.pager);

				// Create utils for scaning the path to image from SD card
				//Utils utils = new Utils(mContext.getApplicationContext());

				int pos = mContext.getIntent().getIntExtra("position", 0);

				ArrayList<String> arrtemp = new ArrayList<String>();
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg");
				
				FullScreenImageAdapter adapter = new FullScreenImageAdapter(mContext,
						// Paths to image on SD card
						//utils.getFilePaths()
						// URL images.
						arrtemp
						);
				viewPager.setAdapter(adapter);
				// displaying selected image first
				viewPager.setCurrentItem(pos);
				break;
			case AppConstant.LAYOUT_TYPE_HORIZONTAL_LIST_2:
				v = inflater.inflate(
						R.layout.fragment_main_horizontal_list_stores, parent,
						false);
				
				HorizontalListView listview = (HorizontalListView) v.findViewById(R.id.listview);
				listview.setAdapter(mAdapter);
				
//				WebView mWebview;
//				mWebview = (WebView) v.findViewById(R.id.webView1);
//				mWebview.getSettings().setJavaScriptEnabled(true);
//				WebViewClient mWebViewClient = new WebViewClient() {
//				    @Override
//				    public boolean shouldOverrideUrlLoading(WebView view, String url) {
//					return super.shouldOverrideUrlLoading(view, url);
//				    }
//		
//				    @Override
//				    public void onPageStarted(WebView view, String url,
//					    android.graphics.Bitmap favicon) {
//				    }
//				};
//				mWebview.setWebViewClient(mWebViewClient);
//				mWebview.loadUrl("http://galagala.vn:88");
				break;
			case AppConstant.LAYOUT_TYPE_HORIZONTAL_LIST_3:
				v = inflater.inflate(
						R.layout.fragment_main_horizontal_list_products,
						parent, false);
				break;
			case AppConstant.LAYOUT_TYPE_SEARCH:
				v = inflater.inflate(
						R.layout.fragment_main_search,
						parent, false);
				break;
			}
		}
		return v;
	}
	
	private static String[] dataObjects = new String[]{ "Text #1",
		"Text #2",
		"Text #3",
		"Text #4",
		"Text #2",
		"Text #3",
		"Text #4",
		"Text #2",
		"Text #3",
		"Text #4",
		"Text #2",
		"Text #3",
		"Text #4"}; 
	
	private BaseAdapter mAdapter = new BaseAdapter() {

		private OnClickListener mOnButtonClicked = new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				AlertDialog.Builder builder = new AlertDialog.Builder(mContext);
				builder.setMessage("hello from " + v);
				builder.setPositiveButton("Cool", null);
				builder.show();
				
			}
		};

		@Override
		public int getCount() {
			return dataObjects.length;
		}

		@Override
		public Object getItem(int position) {
			return null;
		}

		@Override
		public long getItemId(int position) {
			return 0;
		}

		@Override
		public View getView(int position, View convertView, ViewGroup parent) {
			View retval = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_horizontal_list_view_2, null);
			TextView title = (TextView) retval.findViewById(R.id.title);
			Button button = (Button) retval.findViewById(R.id.clickbutton);
			button.setOnClickListener(mOnButtonClicked);
			title.setText(dataObjects[position]);
			
			return retval;
		}
		
	};
}
