package com.hopthanh.gala.adapter;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;

import com.hopthanh.galagala.app.R;
import com.squareup.picasso.Picasso;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.FrameLayout;
import android.widget.ImageView;

public class StorePageGridViewImageProductsAdapter extends BaseAdapter {

	private Context mContext;
	private ArrayList<String> _imagePaths = new ArrayList<String>();
	private int imageWidth;
	private int imageHigh;

	public StorePageGridViewImageProductsAdapter(Context context, ArrayList<String> filePaths,
			int imageWidth) {
		this.mContext = context;
		this._imagePaths = filePaths;
		this.imageWidth = imageWidth;
		this.imageHigh = imageWidth;
	}

	public StorePageGridViewImageProductsAdapter(Context context, ArrayList<String> filePaths,
			int imageWidth, int imageHigh) {
		this.mContext = context;
		this._imagePaths = filePaths;
		this.imageWidth = imageWidth;
		this.imageHigh = imageHigh;
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
		ViewHolder viewHolder;
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (convertView == null) {
			viewLayout = inflater.inflate(R.layout.store_page_layout_slide_non_scrollable_gridview_products_item_details, parent, false);

//			ImageView imageView;
//			imageView = (ImageView) viewLayout.findViewById(R.id.imgStore);
//			imageView.setScaleType(ImageView.ScaleType.FIT_XY);
//			imageView.setLayoutParams(new LinearLayout.LayoutParams(imageWidth,
//					imageWidth * 3 / 4));
//			Picasso.with(_activity)
//	        .load(_imagePaths.get(position))
//	        .into(imageView);
			
			viewHolder = new ViewHolder();
			viewHolder.imgProduct = (ImageView) viewLayout.findViewById(R.id.imgProduct);
			viewHolder.imgProduct.setScaleType(ImageView.ScaleType.FIT_XY);
			viewHolder.imgProduct.setLayoutParams(new FrameLayout.LayoutParams(imageWidth, imageHigh));

			viewLayout.setTag(viewHolder);
		} else {
		    // assign values if the object is not null
			viewHolder = (ViewHolder) viewLayout.getTag();
		}
		
		Picasso.with(mContext)
        .load(_imagePaths.get(position)).resize(imageWidth, imageHigh)
        .into(viewHolder.imgProduct);

		return viewLayout;
	}

	private class ViewHolder {
		ImageView imgProduct;
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
