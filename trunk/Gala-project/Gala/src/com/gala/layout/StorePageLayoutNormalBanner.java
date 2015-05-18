package com.gala.layout;

import com.gala.app.R;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.res.Configuration;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;

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
		ImageView imgDisplay = (ImageView) v.findViewById(R.id.imgDisplay);
		// Load image from SD card.
		// BitmapFactory.Options options = new BitmapFactory.Options();
		// options.inPreferredConfig = Bitmap.Config.ARGB_8888;
		// Bitmap bitmap = BitmapFactory.decodeFile(mImagePaths.get(position),
		// options);
		// imgDisplay.setImageBitmap(bitmap);

		// Load image from URL.
		Picasso.with(context).load((String) mDataSource)
				.into(imgDisplay);

		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
