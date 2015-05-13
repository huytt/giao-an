package com.gala.adapter;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;

import com.gala.app.R;
import com.gala.app.R.id;
import com.gala.app.R.layout;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.drawable.GradientDrawable.Orientation;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

public class GridViewImageAdapter extends BaseAdapter {

	private Activity _activity;
	private ArrayList<String> _imagePaths = new ArrayList<String>();
	private int imageWidth;

	public GridViewImageAdapter(Activity activity, ArrayList<String> filePaths,
			int imageWidth) {
		this._activity = activity;
		this._imagePaths = filePaths;
		this.imageWidth = imageWidth;
	}

	@Override
	public int getCount() {
		return this._imagePaths.size();
	}

	@Override
	public Object getItem(int position) {
		return this._imagePaths.get(position);
	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		View viewLayout = convertView;
		LayoutInflater inflater = (LayoutInflater) _activity.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (convertView == null) {
			viewLayout = inflater.inflate(R.layout.layout_item_details_non_scrollable_gridview_stores, parent, false);

			ImageView imageView;
			imageView = (ImageView) viewLayout.findViewById(R.id.imgStore);
			imageView.setScaleType(ImageView.ScaleType.FIT_XY);
			imageView.setLayoutParams(new LinearLayout.LayoutParams(imageWidth,
					imageWidth * 3 / 4));
			Picasso.with(_activity)
	        .load(_imagePaths.get(position))
	        .into(imageView);
		}
		return viewLayout;
	}

//	class OnImageClickListener implements OnClickListener {
//
//		int _postion;
//
//		// constructor
//		public OnImageClickListener(int position) {
//			this._postion = position;
//		}
//
//		@Override
//		public void onClick(View v) {
//			// on selecting grid view image
//			// launch full screen activity
//			Intent i = new Intent(_activity, FullScreenViewActivity.class);
//			i.putExtra("position", _postion);
//			_activity.startActivity(i);
//		}
//
//	}

	/*
	 * Resizing image size
	 */
	public static Bitmap decodeFile(String filePath, int WIDTH, int HIGHT) {
		try {

			File f = new File(filePath);

			BitmapFactory.Options o = new BitmapFactory.Options();
			o.inJustDecodeBounds = true;
			BitmapFactory.decodeStream(new FileInputStream(f), null, o);

			final int REQUIRED_WIDTH = WIDTH;
			final int REQUIRED_HIGHT = HIGHT;
			int scale = 1;
			while (o.outWidth / scale / 2 >= REQUIRED_WIDTH
					&& o.outHeight / scale / 2 >= REQUIRED_HIGHT)
				scale *= 2;

			BitmapFactory.Options o2 = new BitmapFactory.Options();
			o2.inSampleSize = scale;
			return BitmapFactory.decodeStream(new FileInputStream(f), null, o2);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		return null;
	}

}
