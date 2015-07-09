package com.hopthanh.gala.adapter;

import java.util.ArrayList;

import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.galagala.app.R;
import com.squareup.picasso.Picasso;

import android.content.Context;
import android.content.res.Configuration;
import android.support.v4.view.PagerAdapter;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;

public class StorePageSlideImageProductsPagerAdapter extends PagerAdapter {

	private Context mContext;
	private ArrayList<String> mImagePaths;

	// constructor
	public StorePageSlideImageProductsPagerAdapter(Context context,
			ArrayList<String> imagePaths) {
		this.mContext = context;
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
		LayoutInflater inflater = (LayoutInflater) mContext
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View viewLayout = inflater.inflate(R.layout.store_page_layout_slide_image_products_view,
				container, false);

		ImageView imgDisplay = (ImageView) viewLayout
				.findViewById(R.id.imgDisplay);
		
		ScaleType scaleType = ScaleType.FIT_XY;
		
		if(mContext.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
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
		Picasso.with(mContext).load(mImagePaths.get(position)).fit()
				.into(imgDisplay);
		((CustomViewPagerWrapContent) container).addView(viewLayout);

		return viewLayout;
	}

	@Override
	public void destroyItem(ViewGroup container, int position, Object object) {
//		((CustomViewPagerWrapContent) container).removeView((LinearLayout) object);
		LinearLayout ln = (LinearLayout) object;
		((CustomViewPagerWrapContent) container).removeView(ln);
		ln = null;
	}

}
