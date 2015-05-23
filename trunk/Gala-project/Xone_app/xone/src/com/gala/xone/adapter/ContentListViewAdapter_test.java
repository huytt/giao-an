package com.gala.xone.adapter;

import java.util.ArrayList;
import java.util.List;

import com.gala.xone.app.LoadMoreDataListener;
import com.gala.xone.app.R;
import com.gala.xone.utils.AppConstant;
import com.gala.xone.utils.Utils;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.ImageView.ScaleType;

public class ContentListViewAdapter_test extends GenericAdapter<String>{

	private ArrayList<String> mDataSource = null;
	private Activity mContext = null;
	private LoadMoreDataListener mCallBack = null;

	public ContentListViewAdapter_test(Activity activity,
			ArrayList<String> list) {
		super(activity, list);
		mContext = activity;
		mDataSource = list;
		mCallBack = (LoadMoreDataListener) activity;
		// TODO Auto-generated constructor stub
	}

	@Override
	public View getDataRow(int position, View convertView, ViewGroup parent) {
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
        .load(mDataSource.get(position)).resize(imageWidth, imageWidth * 9 / 16).centerInside()
        .into(holder.imgDisplay);
		return v;
	}
	
	private class ViewHolder {
		TextView tvDecs;
		TextView tvAdName;
		TextView tvNumiew;
		TextView tvNumLike;
		TextView tvNumShare;
		ImageView imgDisplay;
	}

	public ArrayList<String> getDataSource() {
		return mDataSource;
	}

	public void setDataSource(ArrayList<String> mDataSource) {
		this.mDataSource = mDataSource;
	}

	@Override
	public void loadMoreData(ProgressBar bar) {
		// TODO Auto-generated method stub
		mCallBack.loadMoreData(bar);
	}
}
