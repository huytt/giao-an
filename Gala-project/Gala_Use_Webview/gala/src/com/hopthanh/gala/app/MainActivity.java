package com.hopthanh.gala.app;

import java.util.ArrayList;

import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBar.LayoutParams;
import android.support.v7.app.ActionBarActivity;
import android.app.ProgressDialog;
import android.content.pm.ApplicationInfo;
import android.graphics.Typeface;
import android.os.Build;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.TextView;

import com.hopthanh.gala.adapter.MainSlideGridViewEDirectoryPagerAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;

public class MainActivity extends ActionBarActivity {

	// private WebView mWebview;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// setContentView(R.layout.activity_main);
		setContentView(R.layout.main_layout);
		// ActionBar mActionBar = getSupportActionBar();
		// mActionBar.hide();
		//
		// final ProgressDialog progressDialog = new ProgressDialog(this);
		// progressDialog.setCanceledOnTouchOutside(false);
		//
		// mWebview = (WebView) findViewById(R.id.webView);
		// mWebview.getSettings().setJavaScriptEnabled(true);
		//
		// if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
		// WebView.setWebContentsDebuggingEnabled(true);
		// }
		//
		// WebViewClient mWebViewClient = new WebViewClient() {
		// @Override
		// public boolean shouldOverrideUrlLoading(WebView view, String url) {
		// return super.shouldOverrideUrlLoading(view, url);
		// }
		//
		// @Override
		// public void onPageStarted(WebView view, String url,
		// android.graphics.Bitmap favicon) {
		// progressDialog.setMessage("Loading. Please wait...");
		// progressDialog.show();
		// }
		// @Override
		// public void onPageFinished(WebView view, String url) {
		// // TODO Auto-generated method stub
		// progressDialog.dismiss();
		// super.onPageFinished(view, url);
		// }
		// };
		// mWebview.setWebViewClient(mWebViewClient);
		// mWebview.loadUrl("http://galagala.vn");
		//
		//
		// progressDialog.setMessage("Loading. Please wait...");
		// progressDialog.show();

		// Custom actionbar
		ActionBar mActionBar = getSupportActionBar();
		mActionBar.setDisplayShowHomeEnabled(false);
		mActionBar.setDisplayShowTitleEnabled(false);
		mActionBar.setDisplayHomeAsUpEnabled(false);
		mActionBar.setHomeButtonEnabled(false);
		mActionBar.setDisplayOptions(ActionBar.DISPLAY_SHOW_CUSTOM,
				ActionBar.DISPLAY_SHOW_CUSTOM | ActionBar.DISPLAY_SHOW_HOME
						| ActionBar.DISPLAY_SHOW_TITLE);
		LayoutInflater mInflater = LayoutInflater.from(this);

		View mCustomView = mInflater.inflate(R.layout.actionbar_layout, null);
		LayoutParams lp = new LayoutParams(LayoutParams.FILL_PARENT,
				LayoutParams.FILL_PARENT);
		mActionBar.setCustomView(mCustomView, lp);
		mActionBar.setDisplayShowCustomEnabled(true);

		ArrayList<String> arrtempstore = new ArrayList<String>();

		for (int i = 0; i < imageIconObjects.length; i++) {
			arrtempstore.add(imageIconObjects[i]);
		}

		ArrayList<ArrayList<String>> dataSources = new ArrayList<ArrayList<String>>();
		dataSources.add(arrtempstore);
		dataSources.add(arrtempstore);

//		TextView tvEDirectory = (TextView) findViewById(R.id.tvEDirectory);
//
//		tvEDirectory.setTypeface(null, Typeface.BOLD_ITALIC);

		CustomViewPagerWrapContent vpGridView = (CustomViewPagerWrapContent) findViewById(R.id.vpGridViewEDirectory);

		MainSlideGridViewEDirectoryPagerAdapter slgvAdapter = new MainSlideGridViewEDirectoryPagerAdapter(
				this, (ArrayList<ArrayList<String>>) dataSources);
		vpGridView.setAdapter(slgvAdapter);
		// displaying selected gridview first
		vpGridView.setCurrentItem(0);

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		// Hide all menu items on action bar
		for (int i = 0; i < menu.size(); i++) {
			menu.getItem(i).setVisible(false);
		}
		return true;
	}

	@Override
	public void onBackPressed() {
		// if(mWebview.canGoBack()) {
		// mWebview.goBack();
		// } else {
		super.onBackPressed();
		// }
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

	private static String[] imageIconObjects = new String[] {
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			"http://galagala.vn:8888//Media/Icon-Test/unnamed%20(1)%20(2).png",
			 };

}
