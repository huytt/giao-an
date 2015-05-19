package com.gala.layout;

import com.gala.app.R;
import com.gala.objects.Store;
import com.squareup.picasso.MemoryPolicy;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.graphics.Typeface;
import android.opengl.Visibility;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

public class StorePageLayoutNormalBanner extends AbstractLayout{

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(Activity context, LayoutInflater inflater,
			ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_normal_banner, container, false);
//		TextView tvBanner = (TextView) v.findViewById(R.id.tvBanner);
		ImageView imgDisplay = (ImageView) v.findViewById(R.id.imgDisplay);
		
		Store store = (Store) mDataSource;
//		if (store.hasMediaBanner()) {			
			// Load image from SD card.
			// BitmapFactory.Options options = new BitmapFactory.Options();
			// options.inPreferredConfig = Bitmap.Config.ARGB_8888;
			// Bitmap bitmap = BitmapFactory.decodeFile(mImagePaths.get(position),
			// options);
			// imgDisplay.setImageBitmap(bitmap);
	
			// Load image from URL.
			Picasso.with(context).load(store.getStrBanner()).fit().memoryPolicy(MemoryPolicy.NO_CACHE)
					.into(imgDisplay);
//			tvBanner.setVisibility(v.GONE);
//			imgDisplay.setVisibility(v.VISIBLE);
//		} else {
//			tvBanner.setText(store.getStrBanner());
//			tvBanner.setTypeface(null, Typeface.BOLD);
//			tvBanner.setVisibility(v.VISIBLE);
//			imgDisplay.setVisibility(v.GONE);
//		}

		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
