package com.hopthanh.gala.adapter;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerSwipeAbleDisable;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.StorePageLayoutNormalBanner;
import com.hopthanh.gala.layout.StorePageLayoutNormalContact;
import com.hopthanh.gala.layout.StorePageLayoutNormalDescription;
import com.hopthanh.gala.layout.StorePageLayoutSlideGridViewProducts;
import com.hopthanh.gala.layout.StorePageLayoutSlideImageProducts;
import com.hopthanh.gala.objects.Store_fake;

import android.content.Context;
import android.support.v4.view.PagerAdapter;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.ListView;

public class StorePageSlideListViewLayoutPagerAdapter extends PagerAdapter {

	private Context mContext;
	private ArrayList<Store_fake> mDataSource = null;

	// constructor
	public StorePageSlideListViewLayoutPagerAdapter(Context context,
			ArrayList<Store_fake> imagePaths) {
		this.mContext = context;
		this.mDataSource = imagePaths;
	}

	public void clearDataSource() {
		mDataSource.clear();
		mDataSource = null;
	}
	@Override
	public int getCount() {
		if (mDataSource == null) {
			return 0;
		}
		return this.mDataSource.size();
	}

	@Override
	public boolean isViewFromObject(View view, Object object) {
		return view == ((LinearLayout) object);
	}

	@Override
	public Object instantiateItem(ViewGroup container, int position) {
		LayoutInflater inflater = (LayoutInflater) mContext.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		View viewLayout = inflater.inflate(
				R.layout.store_page_layout_slide_listview_layout_contents_view,
				container, false);

		Store_fake item = mDataSource.get(position);
		ArrayList<AbstractLayout<?>> arrLayouts = new ArrayList<AbstractLayout<?>>();

		StorePageLayoutNormalBanner layoutBanner = new StorePageLayoutNormalBanner(mContext);
		layoutBanner.setDataSource(item);
		arrLayouts.add(layoutBanner);

//		ArrayList<String> arrSlideProduct = new ArrayList<String>();
//		for (int i = 0; i < imageSlideProductObjects.length; i++) {
//			arrSlideProduct.add(imageSlideProductObjects[i]);
//		}
		
		StorePageLayoutSlideImageProducts layoutSlideProducts = new StorePageLayoutSlideImageProducts(mContext);
		layoutSlideProducts.setDataSource(item.getProductsImgPaths());
		arrLayouts.add(layoutSlideProducts);
		
		StorePageLayoutNormalDescription layoutDesc = new StorePageLayoutNormalDescription(mContext);
		arrLayouts.add(layoutDesc);
		
		StorePageLayoutSlideGridViewProducts layoutGvProduct = new StorePageLayoutSlideGridViewProducts(mContext);
		layoutGvProduct.setDataSource(item.getProductsImgPaths());		
		arrLayouts.add(layoutGvProduct);
		
		StorePageLayoutNormalContact layoutContact = new StorePageLayoutNormalContact(mContext);
		arrLayouts.add(layoutContact);

//		HomePageLayoutHorizontalScrollViewProducts layoutProduct = new HomePageLayoutHorizontalScrollViewProducts();
//		layoutProduct.setDataSource(arrProduct);		
//		arrLayouts.add(layoutProduct);

		MultiLayoutContentListViewAdapter mainContentAdapter= new MultiLayoutContentListViewAdapter(arrLayouts, mContext);
		
		ListView lsLayoutContainer = (ListView) viewLayout.findViewById(R.id.lsLayoutContain);
		lsLayoutContainer.setAdapter(mainContentAdapter);
		
		((CustomViewPagerSwipeAbleDisable) container).addView(viewLayout);
		return viewLayout;
	}

	
	@Override
	public void destroyItem(ViewGroup container, int position, Object object) {
		LinearLayout ln = (LinearLayout) object;
		ListView ls = (ListView) ln.findViewById(R.id.lsLayoutContain);
		((MultiLayoutContentListViewAdapter) ls.getAdapter()).clearAllLayouts();
		((CustomViewPagerSwipeAbleDisable) container).removeView(ln);
		ln = null;
	}
}
