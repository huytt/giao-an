package com.gala.xone.app;

import java.util.ArrayList;

import com.gala.xone.adapter.ContentListViewAdapter;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ListView;

public class MainActivity extends ActionBarActivity{

	private ListView mLvContents;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		ArrayList<String> dataSource = new ArrayList<String>();
		
		for (int i = 0; i < imageStoreObjects.length; i++) {
			dataSource.add(imageStoreObjects[i]);
		}

		ContentListViewAdapter adapter = new ContentListViewAdapter(this, dataSource);
		mLvContents = (ListView) findViewById(R.id.lvContents);
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
}
