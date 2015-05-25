package com.gala.xone.app;

import com.gala.xone.adapter.GenericAdapter;
import android.os.AsyncTask;
import android.view.View;
import android.widget.ProgressBar;

public class LoadMoreDataTask extends AsyncTask<String, String, String>{

	private ProgressBar mBar;
	private GenericAdapter<String> mAdapter = null;
	
	public LoadMoreDataTask (ProgressBar bar, GenericAdapter<String> adapter) {
		mBar = bar;
		mAdapter = adapter;
	}
	
	@Override
	protected String doInBackground(String... params) {
		// TODO Auto-generated method stub
		try {
			Thread.sleep(5*1000);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
//		mBar.setVisibility(View.GONE);
		return null;
	}

	@Override
	protected void onPostExecute(String result) {
		// TODO Auto-generated method stub
		mBar.setVisibility(View.GONE);
		
		for (int i = 0; i < 3; i++) {
			mAdapter.getDataSource().add(imageStoreObjects[i]);
		}
		mAdapter.notifyDataSetChanged();
		super.onPostExecute(result);
	}
	
	private static String[] imageStoreObjects = new String[] {
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif" ,
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg"
	};
}
