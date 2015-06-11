package com.hopthanh.gala.app;

import java.util.ArrayList;

import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBar.LayoutParams;
import android.support.v7.app.ActionBarActivity;
import android.animation.Animator;
import android.animation.ValueAnimator;
import android.app.ProgressDialog;
import android.content.pm.ApplicationInfo;
import android.graphics.Typeface;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver;
import android.view.View.OnTouchListener;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.ScrollView;
import android.widget.TextView;

import com.hopthanh.gala.adapter.MainSlideGridViewEDirectoryPagerAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerWrapContent;

public class MainActivity extends ActionBarActivity {

	protected static final String TAG = "MainActivity";

	 private WebView mWebview;
	 RelativeLayout layoutFooter;
	 ValueAnimator mAnimator;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		 setContentView(R.layout.activity_main);
//		setContentView(R.layout.main_layout);
		 ActionBar mActionBar = getSupportActionBar();
		 mActionBar.hide();
		
		 final ProgressDialog progressDialog = new ProgressDialog(this);
		 progressDialog.setCanceledOnTouchOutside(false);
		
		 mWebview = (WebView) findViewById(R.id.webView);
		 mWebview.getSettings().setJavaScriptEnabled(true);
		
//		 if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
//		 WebView.setWebContentsDebuggingEnabled(true);
//		 }
		
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
		
		 layoutFooter = (RelativeLayout) findViewById(R.id.layoutFooter);
		 layoutFooter.getViewTreeObserver().addOnPreDrawListener(
	                new ViewTreeObserver.OnPreDrawListener() {
	            
	            @Override
	            public boolean onPreDraw() {
	            	layoutFooter.getViewTreeObserver().removeOnPreDrawListener(this);
//	            	layoutFooter.setVisibility(View.GONE);
	        
//	                final int widthSpec = View.MeasureSpec.makeMeasureSpec(0, View.MeasureSpec.UNSPECIFIED);
//	        		final int heightSpec = View.MeasureSpec.makeMeasureSpec(0, View.MeasureSpec.UNSPECIFIED);
//	        		layoutFooter.measure(widthSpec, heightSpec);

	        		mAnimator = slideAnimator(0, layoutFooter.getMeasuredHeight());
	                return true;
	            }
	        });
			
			
		 layoutFooter.setOnClickListener(new View.OnClickListener() {
	 
	            @Override
	            public void onClick(View v) {
	                if (layoutFooter.getVisibility()==View.GONE){
	                	expand();
	                }else{
	                	collapse();
	                }
	            }
	        });
		 mWebview.setOnTouchListener(new OnTouchListener() {
			float initialY;
			
			@Override
			public boolean onTouch(View v, MotionEvent event) {
				// TODO Auto-generated method stub
				int action = event.getActionMasked();
				
			    switch (action) {
			 
			        case MotionEvent.ACTION_DOWN:
			            initialY = event.getY();
			 
			            Log.d(TAG, "Action was DOWN");
			            break;
			 
			        case MotionEvent.ACTION_MOVE:
			            Log.d(TAG, "Action was MOVE");
			            break;
			 
			        case MotionEvent.ACTION_UP:
					float finalY = event.getY();
			 
			            Log.d(TAG, "Action was UP");
			 
//			            if (initialX < finalX) {
//			                Log.d(TAG, "Left to Right swipe performed");
//			            }
//			 
//			            if (initialX > finalX) {
//			                Log.d(TAG, "Right to Left swipe performed");
//			            }
			 
			            if (initialY < finalY) {
			                Log.d(TAG, "Up to Down swipe performed");
//			                layoutFooter.setVisibility(View.GONE);
			                collapse();
			            }
			 
			            if (initialY > finalY) {
			                Log.d(TAG, "Down to Up swipe performed");
//			                layoutFooter.setVisibility(View.VISIBLE);
			                expand();
			            }
			 
			            break;
			 
			        case MotionEvent.ACTION_CANCEL:
			            Log.d(TAG,"Action was CANCEL");
			            break;
			 
			        case MotionEvent.ACTION_OUTSIDE:
			            Log.d(TAG, "Movement occurred outside bounds of current screen element");
			            break;
			    }
				return false;
			}
		});
		 
		 progressDialog.setMessage("Loading. Please wait...");
		 progressDialog.show();
		
		

//		// Custom actionbar
//		ActionBar mActionBar = getSupportActionBar();
//		mActionBar.setDisplayShowHomeEnabled(false);
//		mActionBar.setDisplayShowTitleEnabled(false);
//		mActionBar.setDisplayHomeAsUpEnabled(false);
//		mActionBar.setHomeButtonEnabled(false);
//		mActionBar.setDisplayOptions(ActionBar.DISPLAY_SHOW_CUSTOM,
//				ActionBar.DISPLAY_SHOW_CUSTOM | ActionBar.DISPLAY_SHOW_HOME
//						| ActionBar.DISPLAY_SHOW_TITLE);
//		LayoutInflater mInflater = LayoutInflater.from(this);
//
//		View mCustomView = mInflater.inflate(R.layout.actionbar_layout, null);
//		LayoutParams lp = new LayoutParams(LayoutParams.FILL_PARENT,
//				LayoutParams.FILL_PARENT);
//		mActionBar.setCustomView(mCustomView, lp);
//		mActionBar.setDisplayShowCustomEnabled(true);
//
//		ArrayList<String> arrtempstore = new ArrayList<String>();
//
//		for (int i = 0; i < imageIconObjects.length; i++) {
//			arrtempstore.add(imageIconObjects[i]);
//		}
//
//		ArrayList<ArrayList<String>> dataSources = new ArrayList<ArrayList<String>>();
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//
////		TextView tvEDirectory = (TextView) findViewById(R.id.tvEDirectory);
////
////		tvEDirectory.setTypeface(null, Typeface.BOLD_ITALIC);
//
//		CustomViewPagerWrapContent vpGridView = (CustomViewPagerWrapContent) findViewById(R.id.vpGridViewEDirectory);
//
//		MainSlideGridViewEDirectoryPagerAdapter slgvAdapter = new MainSlideGridViewEDirectoryPagerAdapter(
//				this, (ArrayList<ArrayList<String>>) dataSources);
//		vpGridView.setAdapter(slgvAdapter);
//		// displaying selected gridview first
//		vpGridView.setCurrentItem(0);
//		
//		final ScrollView mScrollViewMain = (ScrollView) findViewById(R.id.scrollView);
//		final RelativeLayout layoutFooter = (RelativeLayout) findViewById(R.id.layoutFooter);
//		
//		mScrollViewMain.setOnTouchListener(new OnTouchListener() {
//			float initialY;
//			
//			@Override
//			public boolean onTouch(View v, MotionEvent event) {
//				// TODO Auto-generated method stub
//				int childHeight = ((LinearLayout)findViewById(R.id.scrollContent)).getHeight();
//				if(!isScrollAble(mScrollViewMain, childHeight)) {
//					Log.d(TAG, "Can't scroll");
//					layoutFooter.setVisibility(View.VISIBLE);
//					return false;
//				}
//				
//				int action = event.getActionMasked();
//				
//			    switch (action) {
//			 
//			        case MotionEvent.ACTION_DOWN:
//			            initialY = event.getY();
//			 
//			            Log.d(TAG, "Action was DOWN");
//			            break;
//			 
//			        case MotionEvent.ACTION_MOVE:
//			            Log.d(TAG, "Action was MOVE");
//			            break;
//			 
//			        case MotionEvent.ACTION_UP:
//					float finalY = event.getY();
//			 
//			            Log.d(TAG, "Action was UP");
//			 
////			            if (initialX < finalX) {
////			                Log.d(TAG, "Left to Right swipe performed");
////			            }
////			 
////			            if (initialX > finalX) {
////			                Log.d(TAG, "Right to Left swipe performed");
////			            }
//			 
//			            if (initialY < finalY) {
//			                Log.d(TAG, "Up to Down swipe performed");
//			                layoutFooter.setVisibility(View.VISIBLE);
//			            }
//			 
//			            if (initialY > finalY) {
//			                Log.d(TAG, "Down to Up swipe performed");
//			                layoutFooter.setVisibility(View.GONE);
//			            }
//			 
//			            break;
//			 
//			        case MotionEvent.ACTION_CANCEL:
//			            Log.d(TAG,"Action was CANCEL");
//			            break;
//			 
//			        case MotionEvent.ACTION_OUTSIDE:
//			            Log.d(TAG, "Movement occurred outside bounds of current screen element");
//			            break;
//			    }
//			 
//			    return false;
//			}
//		});

	}

	private void expand() {
		//set Visible
		layoutFooter.setVisibility(View.VISIBLE);
		
		/* Remove and used in preDrawListener
		final int widthSpec = View.MeasureSpec.makeMeasureSpec(0, View.MeasureSpec.UNSPECIFIED);
		final int heightSpec = View.MeasureSpec.makeMeasureSpec(0, View.MeasureSpec.UNSPECIFIED);
		mLinearLayout.measure(widthSpec, heightSpec);

		mAnimator = slideAnimator(0, mLinearLayout.getMeasuredHeight());
		*/
		
		mAnimator.start();
	}
	
	private void collapse() {
		int finalHeight = layoutFooter.getHeight();

		ValueAnimator mAnimator = slideAnimator(finalHeight, 0);
		
		mAnimator.addListener(new Animator.AnimatorListener() {
			@Override
			public void onAnimationEnd(Animator animator) {
				//Height=0, but it set visibility to GONE
				layoutFooter.setVisibility(View.GONE);
			}
			
			@Override
			public void onAnimationStart(Animator animator) {
			}

			@Override
			public void onAnimationCancel(Animator animator) {
			}

			@Override
			public void onAnimationRepeat(Animator animator) {
			}
		});
		mAnimator.start();
	}
	
	
	private ValueAnimator slideAnimator(int start, int end) {
		
		ValueAnimator animator = ValueAnimator.ofInt(start, end);
		
		
		animator.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
			@Override
			public void onAnimationUpdate(ValueAnimator valueAnimator) {
				//Update Height
				int value = (Integer) valueAnimator.getAnimatedValue();

				ViewGroup.LayoutParams layoutParams = layoutFooter.getLayoutParams();
				layoutParams.height = value;
				layoutFooter.setLayoutParams(layoutParams);
			}
		});
		return animator;
	}
	
	private boolean isScrollAble(ScrollView scrollView, int childHeight){
		return scrollView.getHeight() < (childHeight + scrollView.getPaddingTop() + scrollView.getPaddingBottom());
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
