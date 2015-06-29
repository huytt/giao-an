package com.hopthanh.gala.customview;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.app.WebViewActivityListener;
import com.hopthanh.gala.objects.ProductInMedia;
import com.hopthanh.gala.utils.Utils;
import com.squareup.picasso.Picasso;

import android.content.Context;
import android.graphics.Paint;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

public class CustomHorizontalLayoutProducts extends CustomHorizontalLayout<ProductInMedia> {

	public CustomHorizontalLayoutProducts(Context context) {
		super(context);
	}

	public CustomHorizontalLayoutProducts(Context context, AttributeSet attrs) {
		super(context, attrs);
	}

	public CustomHorizontalLayoutProducts(Context context, AttributeSet attrs,
			int defStyle) {
		super(context, attrs, defStyle);
	}

	@Override
	public void addItem(ProductInMedia objectItemData) {
		// TODO Auto-generated method stub
		final ProductInMedia itemProduct = objectItemData;
		String path = Utils.XONE_SERVER + itemProduct.getMedia().getUrl() + itemProduct.getMedia().getMediaName();
		
		View view = LayoutInflater.from(mContext).inflate(
				R.layout.home_page_layout_horizontal_scroll_view_products_item_details, null);

		// Load from SD card
		// Bitmap bm = null;
		// if (newIdx < itemList.size()){
		// bm = decodeSampledBitmapFromUri(itemList.get(newIdx), 220, 220);
		// }
		// imageView.setLayoutParams(new LayoutParams(220, 220));
		// imageView.setScaleType(ImageView.ScaleType.FIT_CENTER);
		// imageView.setImageBitmap(bm);

		// Load from URL
		ImageView imageView = (ImageView) view.findViewById(R.id.imgProduct);
		
		Picasso.with(mContext).load(path).resize(imageView.getLayoutParams().width, imageView.getLayoutParams().height)
		.into(imageView);

		TextView tvProductName = (TextView) view.findViewById(R.id.tvProductName);
		TextView tvSalePrice = (TextView) view.findViewById(R.id.tvSalePrice);
		TextView tvRealPrice = (TextView) view.findViewById(R.id.tvRealPrice);
		
		tvProductName.setText(itemProduct.getProduct().getProductName());

		tvSalePrice.setText(String.format("%1$,.0f đ", itemProduct.getProduct().getPromotePrice()));
		tvRealPrice.setText("");
		
		if(itemProduct.getProduct().getRetailPrice() > itemProduct.getProduct().getPromotePrice()) {
			tvRealPrice.setPaintFlags(tvRealPrice.getPaintFlags()
					| Paint.STRIKE_THRU_TEXT_FLAG);

			tvRealPrice.setVisibility(View.VISIBLE);
			tvRealPrice.setText(String.format("%1$,.0f đ", itemProduct.getProduct().getRetailPrice()));
		}
		
		view.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				String strFormat = "";
				if(itemProduct.getProduct().getProductTypeCode().equals("PT1")) {
					// format: {server}/{urlNameStore}-1/{urlName}-{id}.html
					strFormat = "%s/%s-1/%s-%d.html";
				} else if(itemProduct.getProduct().getProductTypeCode().equals("PT2")) {
					// format: {server}/{urlNameStore}-2/{urlName}-{id}.html
					strFormat = "%s/%s-2/%s-%d.html";
				}
				
				String url = String.format(strFormat,
						Utils.XONE_SERVER_WEB,
						itemProduct.getProduct().getStore().getAlias(),
						itemProduct.getProduct().getAlias(),
						itemProduct.getProduct().getProductId()
						);

				((WebViewActivityListener) mListener).notifyStartWebViewActivity(url);
			}
		});
		addView(view);
	}
}
