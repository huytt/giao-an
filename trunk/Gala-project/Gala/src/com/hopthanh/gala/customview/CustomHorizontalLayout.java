package com.hopthanh.gala.customview;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public abstract class CustomHorizontalLayout<T> extends LinearLayout {
	
	protected  Context mContext;
	protected ArrayList<String> itemList = new ArrayList<String>();
	protected Object mListener = null;

	public CustomHorizontalLayout(Context context) {
		super(context);
		mContext = context;
	}

	public CustomHorizontalLayout(Context context, AttributeSet attrs) {
		super(context, attrs);
		mContext = context;
	}

	public CustomHorizontalLayout(Context context, AttributeSet attrs,
			int defStyle) {
		super(context, attrs, defStyle);
		mContext = context;
	}
	
	public void setDataSource(ArrayList<T> dataSource) {
		if(dataSource == null) {
			return;
		}
		
		for (int i = 0; i < dataSource.size(); i++) {
			addItem(dataSource.get(i));
		}
	}
	
	public void addListener(Object listener) {
		mListener = listener;
	}
	
	public abstract void addItem(T objectItemData);
//	public void addItem(String path){
//		int newIdx = itemList.size();
//		itemList.add(path);
//		View view =  LayoutInflater.from(mContext).inflate(R.layout.item_horizontal_scroll_view_products, null);
//
//		//view.setLayoutParams(new LayoutParams(300, 300));
////		Bitmap bm = null;
////		if (newIdx < itemList.size()){
////			bm = decodeSampledBitmapFromUri(itemList.get(newIdx), 220, 220);
////		}
//		
//		ImageView imageView = (ImageView) view.findViewById(R.id.imgProduct);
////    	imageView.setLayoutParams(new LayoutParams(220, 220));
////    	imageView.setScaleType(ImageView.ScaleType.FIT_CENTER);
////    	imageView.setImageBitmap(bm);
//    	Picasso.with(mContext).load(path)
//		.into(imageView);
//    	
//    	TextView tvProductName = (TextView) view.findViewById(R.id.tvProductName);
//    	tvProductName.setText(path);
//    	
//    	TextView tvRealPrice = (TextView) view.findViewById(R.id.tvRealPrice);
//    	tvRealPrice.setPaintFlags(tvRealPrice.getPaintFlags() | Paint.STRIKE_THRU_TEXT_FLAG);
//		//getImageView(newIdx);
//		addView(view);
//	}
	
	public void add(String path){
		int newIdx = itemList.size();
		itemList.add(path);
		View view =  LayoutInflater.from(mContext).inflate(R.layout.home_page_layout_horizontal_scroll_view_products_item_details, null);

		Bitmap bm = null;
		if (newIdx < itemList.size()){
			bm = decodeSampledBitmapFromUri(itemList.get(newIdx), 220, 220);
		}
		
		ImageView imageView = (ImageView) view.findViewById(R.id.image);
    	imageView.setLayoutParams(new LayoutParams(220, 220));
    	imageView.setScaleType(ImageView.ScaleType.CENTER_CROP);
    	imageView.setImageBitmap(bm);
    	
    	TextView txtTitle = (TextView) view.findViewById(R.id.title);
    	txtTitle.setText(path);
		//getImageView(newIdx);
		addView(view);
	}
	
//	private ImageView getImageView(int i){
//		Bitmap bm = null;
//		if (i < itemList.size()){
//			bm = decodeSampledBitmapFromUri(itemList.get(i), 220, 220);
//		}
//		
//		ImageView imageView = new ImageView(mContext);
//    	imageView.setLayoutParams(new LayoutParams(220, 220));
//    	imageView.setScaleType(ImageView.ScaleType.CENTER_CROP);
//    	imageView.setImageBitmap(bm);
//
//		return imageView;
//	}
	
	public Bitmap decodeSampledBitmapFromUri(String path, int reqWidth, int reqHeight) {
    	Bitmap bm = null;
    	
    	// First decode with inJustDecodeBounds=true to check dimensions
    	final BitmapFactory.Options options = new BitmapFactory.Options();
    	options.inJustDecodeBounds = true;
    	BitmapFactory.decodeFile(path, options);
    	
    	// Calculate inSampleSize
    	options.inSampleSize = calculateInSampleSize(options, reqWidth, reqHeight);
    	
    	// Decode bitmap with inSampleSize set
    	options.inJustDecodeBounds = false;
    	bm = BitmapFactory.decodeFile(path, options); 
    	
    	return bm; 	
    }
    
    public int calculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight) {
    	// Raw height and width of image
    	final int height = options.outHeight;
    	final int width = options.outWidth;
    	int inSampleSize = 1;
        
    	if (height > reqHeight || width > reqWidth) {
    		if (width > height) {
    			inSampleSize = Math.round((float)height / (float)reqHeight);  	
    		} else {
    			inSampleSize = Math.round((float)width / (float)reqWidth);  	
    		}  	
    	}
    	
    	return inSampleSize;  	
    }
	
}
