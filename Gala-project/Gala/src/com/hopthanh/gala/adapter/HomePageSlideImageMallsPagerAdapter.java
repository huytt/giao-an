package com.hopthanh.gala.adapter;

import android.content.Context;
import android.support.v4.view.PagerAdapter;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;

import com.hopthanh.gala.customview.CustomViewPagerWrapContent;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.galagala.app.R;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;

public class HomePageSlideImageMallsPagerAdapter extends PagerAdapter {

	private static final String TAG = "HomePageSlideImageMallsPagerAdapter";
	private Context mContext = null;
	private ArrayList<Media> mMediaData = null;
	private ImageView imgDisplay = null;

	// constructor
	public HomePageSlideImageMallsPagerAdapter(Context context,
			ArrayList<Media> mediaData) {
		this.mContext = context;
		this.mMediaData = mediaData;
	}

	@Override
	public int getCount() {
		if (this.mMediaData == null) {
			return 0;
		}
		return this.mMediaData.size();
	}

	@Override
	public boolean isViewFromObject(View view, Object object) {
		return view == ((LinearLayout) object);
	}

	@Override
	public Object instantiateItem(ViewGroup container, int position) {
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View viewLayout = inflater.inflate(R.layout.home_page_layout_slide_image_malls_view,
				container, false);

		if(this.getCount() == 0) {
			return viewLayout;
		}

		imgDisplay = (ImageView) viewLayout
				.findViewById(R.id.imgDisplay);
		
		ScaleType scaleType = ScaleType.FIT_XY;
		
//		if(mActivity.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE) {
//			scaleType = ScaleType.FIT_CENTER;
//		}
		Utils utils = new Utils(mContext);
		int imageWidth = utils.getScreenWidth();
		int imageHigh = imageWidth * 9 / 16;
		
		imgDisplay.setLayoutParams(new LinearLayout.LayoutParams(imageWidth, imageHigh));
		imgDisplay.setScaleType(scaleType);
		// Load image from SD card.
		// BitmapFactory.Options options = new BitmapFactory.Options();
		// options.inPreferredConfig = Bitmap.Config.ARGB_8888;
		// Bitmap bitmap = BitmapFactory.decodeFile(mMediaData.get(position),
		// options);
		// imgDisplay.setImageBitmap(bitmap);

		// Load image from URL.
		String urlMedia = Utils.XONE_SERVER + mMediaData.get(position).getUrl() + mMediaData.get(position).getMediaName();
		Picasso.with(mContext).load(urlMedia).resize(imageWidth, imageHigh)
				.into(imgDisplay);
		((CustomViewPagerWrapContent) container).addView(viewLayout);

		return viewLayout;
	}

	@Override
	public void destroyItem(ViewGroup container, int position, Object object) {
		LinearLayout ln = (LinearLayout) object;
		((CustomViewPagerWrapContent) container).removeView(ln);
		ln = null;
	}
	
	public void Destroy() {
		mMediaData.clear();
		mMediaData = null;
		mContext = null;		
	}
	
	@Override
	protected void finalize() throws Throwable {
		// TODO Auto-generated method stub
		Log.e(TAG, "finalize is called");
		super.finalize();
	}

}
