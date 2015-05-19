package com.gala.app;

import java.util.ArrayList;

import com.gala.adapter.StorePageSlideListViewLayoutPagerAdapter;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;


public class CategoryFragment extends Fragment {
	private View mView;
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
//		View rootView = inflater.inflate(R.layout.fragment_main_category, container, false);
		View rootView = inflater.inflate(R.layout.store_page_fragment_main, container, false);
		ViewPager vpListLayout = (ViewPager) rootView.findViewById(R.id.vpListLayouts);
		int pos = getActivity().getIntent().getIntExtra("position", 0);

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

		StorePageSlideListViewLayoutPagerAdapter sliAdapter = new StorePageSlideListViewLayoutPagerAdapter(getActivity(),
				dataSources
				);
		vpListLayout.setAdapter(sliAdapter);
		// displaying selected image first
		vpListLayout.setCurrentItem(pos);
		
		mView = rootView;
		return rootView;
	}
	
	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if (mView != null && mView.getParent() != null) {
			ViewPager vp = (ViewPager) mView.findViewById(R.id.vpListLayouts);
			((StorePageSlideListViewLayoutPagerAdapter) vp.getAdapter()).clearDataSource();
            ((ViewGroup) mView.getParent()).removeView(mView);
            mView = null;
        }
		super.onDestroyView();
	}
	
	private static String[] imageStoreObjects = new String[] {
//		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
//		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg",
//		"http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg",
//		"http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif" ,
//		"http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg",
		"http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg"
	};

	private static String[] imageBannersObjects = new String[]{ 
		"http://galagala.vn:8888//Media/Store/S000011/20150514_STORE-1_cec691e0-ffa7-4c8a-834f-08194c968bbc.jpg",
		"http://galagala.vn:8888//Media/Store/S000021/20150515_STORE-1_9fead174-9ec1-43b6-92b6-02a57e3bdae8.jpg",
		"http://galagala.vn:8888//Media/Store/S000022/20150515_STORE-1_734f06ce-d540-417b-bfb3-98aabe8619dd.jpg",
		"http://galagala.vn:8888//Media/Store/S000023/20150515_STORE-1_80089154-3c31-4ecc-9a8d-a5420ba4a11c.png",
		"http://galagala.vn:8888//Media/Store/S000024/20150515_STORE-1_c706963b-ebe0-4587-bcd8-cb5e46e645ce.jpg",
		"http://galagala.vn:8888//Media/Store/S000026/20150515_STORE-1_35cad6c5-677a-497b-8eec-925fb2326c13.png"
	};

}
