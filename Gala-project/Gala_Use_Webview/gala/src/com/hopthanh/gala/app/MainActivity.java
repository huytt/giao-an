package com.hopthanh.gala.app;

import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.app.ProgressDialog;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.webkit.WebView;
import android.webkit.WebViewClient;

import com.hopthanh.gala.app.R;

public class MainActivity extends ActionBarActivity {

	private WebView mWebview;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		ActionBar mActionBar = getSupportActionBar();
		mActionBar.hide();
		
		final ProgressDialog progressDialog = new ProgressDialog(this);
		progressDialog.setCanceledOnTouchOutside(false);
		
		mWebview = (WebView) findViewById(R.id.webView);
		mWebview.getSettings().setJavaScriptEnabled(true);
		WebViewClient mWebViewClient = new WebViewClient() {
		    @Override
		    public boolean shouldOverrideUrlLoading(WebView view, String url) {
			return super.shouldOverrideUrlLoading(view, url);
		    }

		    @Override
		    public void onPageStarted(WebView view, String url,
			    android.graphics.Bitmap favicon) {
				progressDialog.setMessage("Loading. Please wait...");
				progressDialog.show();
			}
		    @Override
		    public void onPageFinished(WebView view, String url) {
		    	// TODO Auto-generated method stub
		    	progressDialog.dismiss();
		    	super.onPageFinished(view, url);
			}
		};
		mWebview.setWebViewClient(mWebViewClient);
		mWebview.loadUrl("http://galagala.vn");


		progressDialog.setMessage("Loading. Please wait...");
        progressDialog.show();

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public void onBackPressed() {
		if(mWebview.canGoBack()) {
			mWebview.goBack();
		} else {
			super.onBackPressed();
		}
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
}