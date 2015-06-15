package com.hopthanh.gala.customview;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.app.R.id;
import com.hopthanh.gala.app.R.layout;
import com.squareup.picasso.Picasso;

import android.content.Context;
import android.graphics.Paint;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

public class CustomHorizontalLayoutSpecialStores extends CustomHorizontalLayout {

	public CustomHorizontalLayoutSpecialStores(Context context) {
		super(context);
	}

	public CustomHorizontalLayoutSpecialStores(Context context, AttributeSet attrs) {
		super(context, attrs);
	}

	public CustomHorizontalLayoutSpecialStores(Context context, AttributeSet attrs,
			int defStyle) {
		super(context, attrs, defStyle);
	}

	@Override
	public void addItem(Object objectItemData) {
		// TODO Auto-generated method stub
		String path = (String) objectItemData;
		View view = LayoutInflater.from(mContext).inflate(
				R.layout.home_page_layout_horizontal_scroll_view_special_stores_item_details, null);

		// Load from SD card
		// Bitmap bm = null;
		// if (newIdx < itemList.size()){
		// bm = decodeSampledBitmapFromUri(itemList.get(newIdx), 220, 220);
		// }
		// imageView.setLayoutParams(new LayoutParams(220, 220));
		// imageView.setScaleType(ImageView.ScaleType.FIT_CENTER);
		// imageView.setImageBitmap(bm);

		// Load from URL
		ImageView imageView = (ImageView) view.findViewById(R.id.imgSpecialStores);
		Picasso.with(mContext).load(path).fit().into(imageView);

//		TextView tvProductName = (TextView) view
//				.findViewById(R.id.tvProductName);
//		tvProductName.setText(path);
//
//		TextView tvRealPrice = (TextView) view.findViewById(R.id.tvRealPrice);
//		tvRealPrice.setPaintFlags(tvRealPrice.getPaintFlags()
//				| Paint.STRIKE_THRU_TEXT_FLAG);

		addView(view);
	}
}
