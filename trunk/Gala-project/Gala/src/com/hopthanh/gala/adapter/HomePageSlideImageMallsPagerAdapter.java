package com.hopthanh.gala.adapter;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.Context;
import android.content.res.Configuration;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;

public class HomePageSlideImageMallsPagerAdapter extends PagerAdapter {

	private Activity mActivity;
	private ArrayList<String> mImagePaths;

	// constructor
	public HomePageSlideImageMallsPagerAdapter(Activity activity,
			ArrayList<String> imagePaths) {
		this.mActivity = activity;
		this.mImagePaths = imagePaths;
	}

	@Override
	public int getCount() {
		return this.mImagePaths.size();
	}

	@Override
	public boolean isViewFromObject(View view, Object object) {
		return view == ((LinearLayout) object);
	}

	@Override
	public Object instantiateItem(ViewGroup container, int position) {
		LayoutInflater inflater = (LayoutInflater) mActivity
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View viewLayout = inflater.inflate(R.layout.home_page_layout_slide_image_malls_view,
				container, false);

		ImageView imgDisplay = (ImageView) viewLayout
				.findViewById(R.id.imgDisplay);
		
		ScaleType scaleType = ScaleType.FIT_XY;
		
		if(mActivity.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
			scaleType = ScaleType.FIT_CENTER;
		}
		imgDisplay.setScaleType(scaleType);
		// Load image from SD card.
		// BitmapFactory.Options options = new BitmapFactory.Options();
		// options.inPreferredConfig = Bitmap.Config.ARGB_8888;
		// Bitmap bitmap = BitmapFactory.decodeFile(mImagePaths.get(position),
		// options);
		// imgDisplay.setImageBitmap(bitmap);

		// Load image from URL.
		Picasso.with(mActivity).load(mImagePaths.get(position)).fit()
				.into(imgDisplay);
		((ViewPager) container).addView(viewLayout);

		return viewLayout;
	}

	@Override
	public void destroyItem(ViewGroup container, int position, Object object) {
		LinearLayout ln = (LinearLayout) object;
		((ViewPager) container).removeView(ln);
		ln = null;
	}

}
