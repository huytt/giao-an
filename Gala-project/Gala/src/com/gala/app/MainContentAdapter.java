package com.gala.app;

import java.util.ArrayList;

import com.devsmart.android.ui.HorizontalListView;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.GridView;
import android.widget.HorizontalScrollView;
import android.widget.ImageView;
import android.widget.LinearLayout;
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
		Utils utils = new Utils(mContext.getApplicationContext());
		if (v == null) {
			// Inflate the layout according to the view type
			LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
			switch (type) {
			case AppConstant.LAYOUT_TYPE_SLIDE_IMAGE:
				v = inflater.inflate(R.layout.layout_main_malls_slide_image,
						parent, false);
				
				ViewPager viewPager = (ViewPager) v.findViewById(R.id.pager);

				// Create utils for scaning the path to image from SD card
				//Utils utils = new Utils(mContext.getApplicationContext());

				int pos = mContext.getIntent().getIntExtra("position", 0);

				ArrayList<String> arrtemp = new ArrayList<String>();
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg");
				arrtemp.add("http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg");
				
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
						R.layout.layout_main_stores_gridview, parent,
						false);
				
				ArrayList<String> arrtempstore = new ArrayList<String>();
//				arrtempstore.add("http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg");
//				arrtempstore.add("http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif");
//				arrtempstore.add("http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg");
				
				for (int i = 0; i < imageStoreObjects.length; i++) {
					arrtempstore.add(imageStoreObjects[i]);
				}
				NonScrollableGridView gridView = (NonScrollableGridView) v.findViewById(R.id.gvStores);
				
				Resources r = mContext.getResources();
				float padding = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP,
						AppConstant.GRID_PADDING, r.getDisplayMetrics());

				int columnWidth = (int) ((utils.getScreenWidth() - ((AppConstant.NUM_OF_COLUMNS + 1) * padding)) / AppConstant.NUM_OF_COLUMNS);
//
				gridView.setNumColumns(AppConstant.NUM_OF_COLUMNS);
				gridView.setColumnWidth(columnWidth);
				gridView.setStretchMode(GridView.NO_STRETCH);
				gridView.setPadding((int) padding, (int) padding, (int) padding,
						(int) padding);
				gridView.setHorizontalSpacing((int) padding);
				gridView.setVerticalSpacing((int) padding);
				
				// loading all image paths from SD card

				// Gridview adapter
				GridViewImageAdapter gvadapter = new GridViewImageAdapter(mContext, arrtempstore, columnWidth);
//
//				// setting grid view adapter
				gridView.setAdapter(gvadapter);
				
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
						R.layout.layout_main_products_horizontal_list,
						parent, false);
				
				ArrayList<String> arrtemsproduct = new ArrayList<String>();
//				arrtempstore.add("http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg");
//				arrtempstore.add("http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif");
//				arrtempstore.add("http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg");
				
				for (int i = 0; i < imageProductObjects.length; i++) {
					arrtemsproduct.add(imageProductObjects[i]);
				}
				
				CustomHorizontalLayout chsvProductsLayout = (CustomHorizontalLayout) v.findViewById(R.id.hsvProducts);
				chsvProductsLayout.setDataSource(arrtemsproduct);
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
	
	private static String[] dataObjects = new String[]{
		"Text #1",
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
		"Text #4"
	}; 
	
	private static String[] imageStoreObjects = new String[] {
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif" 
	};
	
	private static String[] imageProductObjects = new String[]{ 
		"http://galagala.vn:8888//Media/Store/S000008/Product/P00000013/20150509_STORE-3_b3a54d79-1720-44d8-8fbc-30732a3e5a2f.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000008/Hana-Store-PHB-D1-VayVOK001-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/Product/P00000013/20150509_STORE-3_b3a54d79-1720-44d8-8fbc-30732a3e5a2f.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000008/Hana-Store-PHB-D1-VayVOK001-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg"
	};
	
	private BaseAdapter mAdapter = new BaseAdapter() {

//		private OnClickListener mOnButtonClicked = new OnClickListener() {
//			
//			@Override
//			public void onClick(View v) {
//				AlertDialog.Builder builder = new AlertDialog.Builder(mContext);
//				builder.setMessage("hello from " + v);
//				builder.setPositiveButton("Cool", null);
//				builder.show();
//				
//			}
//		};

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
//			Button button = (Button) retval.findViewById(R.id.clickbutton);
//			button.setOnClickListener(mOnButtonClicked);
			title.setText(dataObjects[position]);
			
			ImageView imgDisplay = (ImageView) retval.findViewById(R.id.image);
			
			Utils utils = new Utils(mContext.getApplicationContext());
	        // Load image from SD card.
	        BitmapFactory.Options options = new BitmapFactory.Options();
	        options.inPreferredConfig = Bitmap.Config.ARGB_8888;
	        Bitmap bitmap = BitmapFactory.decodeFile( utils.getFilePaths().get(position), options);
	        imgDisplay.setImageBitmap(bitmap);
			
	        // Load image from URL.
//	        Picasso.with(mContext)
//	        .load(imageObjects[position])
//	        .into(imgDisplay);
			
			return retval;
		}
		
	};
}
