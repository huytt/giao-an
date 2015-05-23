package com.gala.xone.app;

import java.util.ArrayList;

import com.gala.xone.adapter.ContentListViewAdapter;
import com.gala.xone.adapter.ContentListViewAdapter_test;
import com.squareup.picasso.Picasso;

import android.support.v7.app.ActionBarActivity;
import android.opengl.Visibility;
import android.os.Bundle;
import android.os.Handler;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.ProgressBar;

public class MainActivity extends ActionBarActivity implements LoadMoreDataListener{

	private ListView mLvContents;
	 private static final int PROGRESS = 0x1;

     private ProgressBar mProgress;
     private int mProgressStatus = 0;

     private Handler mHandler = new Handler();
     
     ContentListViewAdapter_test adapter;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
//		mProgress = (ProgressBar) findViewById(R.id.progress_bar);
//
//        // Start lengthy operation in a background thread
//        new Thread(new Runnable() {
//            public void run() {
//                while (mProgressStatus < 100) {
//                    mProgressStatus+=5;
//
//                    // Update the progress bar
//                    mHandler.post(new Runnable() {
//                        public void run() {
//                            mProgress.setProgress(mProgressStatus);
//                        }
//                    });
//                }
//            }
//        }).start();
        
		ArrayList<String> dataSource = new ArrayList<String>();
		
		for (int i = 0; i < imageStoreObjects.length; i++) {
			dataSource.add(imageStoreObjects[i]);
		}

		adapter = new ContentListViewAdapter_test(this, dataSource);
		mLvContents = (ListView) findViewById(R.id.lvContents);
//		mLvContents.setOnScrollListener(new EndlessScrollListener() {
//			
//			@Override
//			public void onLoadMore(int page, int totalItemsCount) {
//				// TODO Auto-generated method stub
////				for (int i = 0; i < 3; i++) {
////					adapter.getDataSource().add(imageStoreObjects[i]);
////				}
////				adapter.notifyDataSetChanged();
//			}
//		});
		mLvContents.setAdapter(adapter);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	private static String[] imageStoreObjects = new String[] {
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif" ,
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg"
	};

	@Override
	public void loadMoreData(ProgressBar bar) {
		// TODO Auto-generated method stub
		try{
		Thread.sleep(30 * 1000);
		} catch (Exception e) {
			System.out.println(e);
		}
		
		for (int i = 0; i < 3; i++) {
			adapter.getDataSource().add(imageStoreObjects[i]);
		}
		adapter.notifyDataSetChanged();
		
		bar.setVisibility(View.GONE);
	}
}
