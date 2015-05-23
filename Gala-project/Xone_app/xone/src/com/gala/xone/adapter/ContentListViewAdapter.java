package com.gala.xone.adapter;

import java.util.ArrayList;

import com.gala.xone.app.R;
import com.gala.xone.utils.AppConstant;
import com.gala.xone.utils.Utils;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;
import android.widget.TextView;

public class ContentListViewAdapter extends BaseAdapter {

	private ArrayList<String> mDataSource = null;
	private Activity mContext = null;

	public ContentListViewAdapter(Activity context, ArrayList<String> dataSource) {
		this.setDataSource(dataSource);
		this.mContext = context;
	}

	public void clearAllLayouts() {
		getDataSource().clear();
		setDataSource(null);
		System.gc();
	}
	
	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		if (getDataSource() == null)
			return 0;
		return this.getDataSource().size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return this.getDataSource().get(position);
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View v = convertView;
		Log.d("========huytt=========","=========position======"+position+"==========convertView====="+convertView);
		ViewHolder holder = null;
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		if (v == null) {
			v = inflater.inflate(R.layout.row_layout_details, parent, false);
			holder = new ViewHolder();
			holder.tvDecs = (TextView) v.findViewById(R.id.tvAdShortDesc);
			holder.tvAdName = (TextView) v.findViewById(R.id.tvAdName);
			holder.tvNumiew = (TextView) v.findViewById(R.id.tvView);
			holder.tvNumLike = (TextView) v.findViewById(R.id.tvLike);
			holder.tvNumShare = (TextView) v.findViewById(R.id.tvShare);
			holder.imgDisplay = (ImageView) v.findViewById(R.id.imgDisplay);
			v.setTag(holder);
		} 
		else {
			holder = (ViewHolder) v.getTag();
		}
		
		Utils utils = new Utils(mContext.getApplicationContext());
		
		float padding = TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP,
				AppConstant.GRID_PADDING, mContext.getResources().getDisplayMetrics());
		
		int imageWidth = (int) (utils.getScreenWidth() - 2 * padding);
		
		holder.imgDisplay.setLayoutParams(new LinearLayout.LayoutParams(imageWidth,
				imageWidth * 9 / 16));
		
		holder.imgDisplay.setScaleType(ScaleType.FIT_XY);
//		mDataSource = utils.getFilePaths();
//		
//		// Load image from SD card.
//		 BitmapFactory.Options options = new BitmapFactory.Options();
//		 options.inPreferredConfig = Bitmap.Config.ARGB_8888;
//		 Bitmap bitmap = BitmapFactory.decodeFile(mDataSource.get(position),
//		 options);
//		 holder.imgDisplay.setImageBitmap(bitmap);

		Picasso.with(mContext)
        .load(getDataSource().get(position)).resize(imageWidth, imageWidth * 9 / 16).centerInside()
        .into(holder.imgDisplay);
		return v;
	}
	
	public ArrayList<String> getDataSource() {
		return mDataSource;
	}

	public void setDataSource(ArrayList<String> mDataSource) {
		this.mDataSource = mDataSource;
	}

	private class ViewHolder {
		TextView tvDecs;
		TextView tvAdName;
		TextView tvNumiew;
		TextView tvNumLike;
		TextView tvNumShare;
		ImageView imgDisplay;
	}
}
