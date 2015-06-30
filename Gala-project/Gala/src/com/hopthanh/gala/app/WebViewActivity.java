package com.hopthanh.gala.app;

import android.app.ProgressDialog;
import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.webkit.WebView;
import android.webkit.WebViewClient;

public class WebViewActivity extends ActionBarActivity {
	
	private WebView mWebview;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.layout_webview);
		// setContentView(R.layout.main_layout);
		ActionBar mActionBar = getSupportActionBar();
		mActionBar.hide();

		final ProgressDialog progressDialog = new ProgressDialog(this);
		progressDialog.setCanceledOnTouchOutside(false);

		mWebview = (WebView) findViewById(R.id.webView);
		mWebview.getSettings().setJavaScriptEnabled(true);

		// if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
		// WebView.setWebContentsDebuggingEnabled(true);
		// }

		WebViewClient mWebViewClient = new WebViewClient() {
			@Override
			public boolean shouldOverrideUrlLoading(WebView view, String url) {
				return super.shouldOverrideUrlLoading(view, url);
			}

			@Override
			public void onPageStarted(WebView view, String url,
					android.graphics.Bitmap favicon) {
				progressDialog.setMessage(getString(R.string.OSD_please_wait));
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
		
		String url = getIntent().getStringExtra("URL");
		mWebview.loadUrl(url);
	}
	
	@Override
	public void onBackPressed() {
		// TODO Auto-generated method stub
		if (mWebview.canGoBack()) {
			mWebview.goBack();
		} else {
			super.onBackPressed();
		}
	}
}
