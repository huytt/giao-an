package com.gala.app;

import java.util.ArrayList;

import com.gala.layout.AbstractLayout;
import com.gala.layout.LayoutHorizontalScrollViewProducts;
import com.gala.layout.LayoutHorizontalScrollViewSpecialStores;
import com.gala.layout.LayoutSlideGridViewStores;
import com.gala.layout.LayoutSlideImageMalls;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;


public class HomePageFragment extends Fragment {
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		View rootView = inflater.inflate(R.layout.fragment_main, container, false);
		
		ArrayList<String> arrtemp = new ArrayList<String>();
		arrtemp.add("http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg");
		arrtemp.add("http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif");
		arrtemp.add("http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg");
		arrtemp.add("http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg");
		
		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
		LayoutSlideImageMalls layoutMall = new LayoutSlideImageMalls();
		layoutMall.setDataSource(arrtemp);		
		arrLayouts.add(layoutMall);
		
		ArrayList<String> arrtempstore = new ArrayList<String>();
		
		for (int i = 0; i < imageStoreObjects.length; i++) {
			arrtempstore.add(imageStoreObjects[i]);
		}
		
		ArrayList<ArrayList<String>> dataSources = new ArrayList<ArrayList<String>>();
		dataSources.add(arrtempstore);
		dataSources.add(arrtempstore);
		dataSources.add(arrtempstore);
		dataSources.add(arrtempstore);
		dataSources.add(arrtempstore);
		dataSources.add(arrtempstore);

		LayoutSlideGridViewStores layoutStore = new LayoutSlideGridViewStores();
		layoutStore.setDataSource(dataSources);		
		arrLayouts.add(layoutStore);
		
		ArrayList<String> arrSpecStore = new ArrayList<String>();

		for (int i = 0; i < imageSpecialStoreObjects.length; i++) {
			arrSpecStore.add(imageSpecialStoreObjects[i]);
		}

		LayoutHorizontalScrollViewSpecialStores layoutSpecStore = new LayoutHorizontalScrollViewSpecialStores();
		layoutSpecStore.setDataSource(arrSpecStore);		
		arrLayouts.add(layoutSpecStore);
		
		ArrayList<String> arrProduct = new ArrayList<String>();
		for (int i = 0; i < imageProductObjects.length; i++) {
			arrProduct.add(imageProductObjects[i]);
		}
		
		LayoutHorizontalScrollViewProducts layoutProduct = new LayoutHorizontalScrollViewProducts();
		layoutProduct.setDataSource(arrProduct);		
		arrLayouts.add(layoutProduct);
		
		LayoutHorizontalScrollViewProducts layoutProduct1 = new LayoutHorizontalScrollViewProducts();
		layoutProduct1.setDataSource(arrProduct);		
		arrLayouts.add(layoutProduct1);
		
		LayoutHorizontalScrollViewProducts layoutProduct2 = new LayoutHorizontalScrollViewProducts();
		layoutProduct2.setDataSource(arrProduct);		
		arrLayouts.add(layoutProduct2);

		MainContentAdapter mainContentAdapter= new MainContentAdapter(arrLayouts, getActivity());
		
		ListView lsLayoutContainer = (ListView) rootView.findViewById(R.id.lsLayoutContain);
		lsLayoutContainer.setAdapter(mainContentAdapter);
		return rootView;
	}
	
	private static String[] imageSpecialStoreObjects = new String[] {
		"http://galagala.vn:8888//Media/Brand/B1/20150507_BRAND-1_f3262ed9-7e3c-44bc-8f57-ce42a9273a16.jpg",
		"http://galagala.vn:8888//Media/Brand/B2/20150507_BRAND-1_5593fd7d-7c6d-4613-bf8f-43321dd367f9.gif",
		"http://galagala.vn:8888//Media/Brand/B3/20150507_BRAND-1_2f7c5be7-2273-4b47-8f6c-a3f5cca1666e.png",
		"http://galagala.vn:8888//Media/Brand/B4/20150507_BRAND-1_7684b38a-61ea-4a09-81d7-c674cc1de033.jpg"
	};
	
	private static String[] imageStoreObjects = new String[] {
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg",
		"http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg",
		"http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif" ,
		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg"
	};
	
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
}
