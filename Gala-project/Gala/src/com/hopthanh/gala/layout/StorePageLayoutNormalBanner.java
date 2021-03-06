package com.hopthanh.gala.layout;

import com.hopthanh.gala.customview.CustomViewPagerSwipeAbleDisable;
import com.hopthanh.gala.objects.Store_fake;
import com.hopthanh.galagala.app.R;
import com.squareup.picasso.Picasso;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.view.ViewGroup;
import android.widget.ImageView;

public class StorePageLayoutNormalBanner extends AbstractLayout<Store_fake>{

	public StorePageLayoutNormalBanner(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_normal_banner, container, false);
//		TextView tvBanner = (TextView) v.findViewById(R.id.tvBanner);
		ImageView imgDisplay = (ImageView) v.findViewById(R.id.imgDisplay);
		
		Store_fake store = mDataSource;
//		if (store.hasMediaBanner()) {			
			// Load image from SD card.
			// BitmapFactory.Options options = new BitmapFactory.Options();
			// options.inPreferredConfig = Bitmap.Config.ARGB_8888;
			// Bitmap bitmap = BitmapFactory.decodeFile(mImagePaths.get(position),
			// options);
			// imgDisplay.setImageBitmap(bitmap);
	
			// Load image from URL.
			Picasso.with(mContext).load(store.getStrBanner()).resize(imgDisplay.getLayoutParams().width, imgDisplay.getLayoutParams().height)
					.into(imgDisplay);
//			tvBanner.setVisibility(v.GONE);
//			imgDisplay.setVisibility(v.VISIBLE);
//		} else {
//			tvBanner.setText(store.getStrBanner());
//			tvBanner.setTypeface(null, Typeface.BOLD);
//			tvBanner.setVisibility(v.VISIBLE);
//			imgDisplay.setVisibility(v.GONE);
//		}

			imgDisplay.setOnTouchListener(new OnTouchListener() {
				
				@Override
				public boolean onTouch(View v, MotionEvent event) {
					// TODO Auto-generated method stub
					// Enable swipe when touch on this.
					// viewpager/LinenearLayout/ListView/FrameLayout/this.
					CustomViewPagerSwipeAbleDisable vp = (CustomViewPagerSwipeAbleDisable) v.getParent().getParent().getParent().getParent();
					switch (event.getAction()){
					case MotionEvent.ACTION_DOWN:
						vp.setEnableSwipe(true);
						vp.onTouchEvent(event);
						vp.onInterceptTouchEvent(event);
						break;
					}
					return true;
				}
			});
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
