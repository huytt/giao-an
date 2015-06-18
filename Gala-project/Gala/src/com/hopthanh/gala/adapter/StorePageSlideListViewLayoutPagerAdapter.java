package com.hopthanh.gala.adapter;

import java.util.ArrayList;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerSwipeAbleDisable;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.HomePageLayoutSlideGridViewStores;
import com.hopthanh.gala.layout.StorePageLayoutNormalBanner;
import com.hopthanh.gala.layout.StorePageLayoutNormalContact;
import com.hopthanh.gala.layout.StorePageLayoutNormalDescription;
import com.hopthanh.gala.layout.StorePageLayoutSlideGridViewProducts;
import com.hopthanh.gala.layout.StorePageLayoutSlideImageProducts;
import com.hopthanh.gala.objects.Store_fake;

import android.app.Activity;
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
		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();

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

	private static String[] imageProductObjects = new String[]{ 
		"http://galagala.vn:8888//Media/Store/S000008/Product/P00000013/20150509_STORE-3_b3a54d79-1720-44d8-8fbc-30732a3e5a2f.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000008/Hana-Store-PHB-D1-VayVOK001-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/Product/P00000013/20150509_STORE-3_b3a54d79-1720-44d8-8fbc-30732a3e5a2f.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000008/Hana-Store-PHB-D1-VayVOK001-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000004/KhanhToan-Store-PHB-D1-SonyHandyCam-1.gif",
		"http://galagala.vn:8888//Media/Store/S000001/Product/P00000003/KhanhToan-Store-PHB-D1-Fujifilm-1.gif",
		"http://galagala.vn:8888//Media/Store/S000002/Product/P00000005/Hana-Store-PHB-D1-DamBMaka-1.jpg"
	};
	
	private static String[] imageSlideProductObjects = new String[]{ 
		"http://galagala.vn:8888//Media/Store/S000026/Product/P00000025/20150515_STORE-3_201b431f-a08d-4328-8037-da962f46df6f.jpg",
		"http://galagala.vn:8888//Media/Store/S000021/Product/P00000018/20150515_STORE-3_32d11bd4-f791-4e12-b239-78aaede0f267.jpg",
		"http://galagala.vn:8888//Media/Store/S000021/Product/P00000019/20150515_STORE-3_465757bb-be52-43bb-a3e8-c63ba3145d67.jpg",
		"http://galagala.vn:8888//Media/Store/S000021/Product/P00000020/20150515_STORE-3_8071f037-139e-4a20-911d-30d02e8b2cc7.jpg"
	};
}
