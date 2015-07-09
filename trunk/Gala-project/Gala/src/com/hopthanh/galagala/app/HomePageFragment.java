package com.hopthanh.galagala.app;

import java.util.ArrayList;
import java.util.concurrent.ExecutionException;

import org.javatuples.Quartet;
import org.javatuples.Quintet;
import org.javatuples.Triplet;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewProducts;
import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewBrand;
import com.hopthanh.gala.layout.HomePageLayoutNormalCategory;
import com.hopthanh.gala.layout.HomePageLayoutSlideGridViewStores;
import com.hopthanh.gala.layout.HomePageLayoutSlideImageMalls;
import com.hopthanh.gala.layout.LayoutNormalFooter;
import com.hopthanh.gala.objects.Article;
import com.hopthanh.gala.objects.ArticleType;
import com.hopthanh.gala.objects.Brand;
import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.FooterDataClass;
import com.hopthanh.gala.objects.HomePageDataClass;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.ProductInMedia;
import com.hopthanh.gala.objects.StoreInMedia;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.gala.web_api_util.ITaskLoadJsonDataListener;
import com.hopthanh.gala.web_api_util.JSONHttpClient;
import com.hopthanh.gala.web_api_util.LoadHomePageDataTask;
import com.hopthanh.gala.web_api_util.LoadJsonDataTask;
import com.hopthanh.galagala.app.R;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask.Status;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView.FindListener;
import android.widget.LinearLayout;
import android.widget.ListView;


public class HomePageFragment extends AbstractLayoutFragment<Object> implements ITaskLoadJsonDataListener<HomePageDataClass> {
	private static final String TAG = "HomePageFragment";

	private LinearLayout mLayoutContain = null;
	private Activity mActivity = null;
	private LayoutInflater mInflater = null;
	private ViewGroup mContainer = null;
	private ListView mLvLayoutContainer = null;
	
	public HomePageFragment() {
		super();
	}
	
	@Override
	public void onAttach(Activity activity) {
		// TODO Auto-generated method stub
		super.onAttach(activity);
		mActivity = activity;
	}
	
	@Override
	public void onDetach() {
		// TODO Auto-generated method stub
		super.onDetach();
		mActivity = null;
		mListener = null;
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub

		Log.e("=====HomePageFragment========","=====onCreateView=====");
		mView = inflater.inflate(R.layout.home_page_fragment_main_edit, container, false);
		mLayoutContain = (LinearLayout) mView.findViewById(R.id.lnContain);
		mInflater = inflater;
		mContainer = container;

//		mLvLayoutContainer = (ListView) mView.findViewById(R.id.lsLayoutContain);
//		executeTask();
		
//		HomePageLayoutHorizontalScrollViewProducts layoutProductBuy = new HomePageLayoutHorizontalScrollViewProducts(mActivity.getApplicationContext());
//        mLayoutContain.addView(layoutProductBuy.getView(
//    		mInflater,
//      		mContainer));

//		ArrayList<String> arrtemp = new ArrayList<String>();
//		arrtemp.add("http://galagala.vn:8888//Media/Store/S000004/20150508_MALL-2_f1eb7644-9a5f-4cc3-a63a-975155a38509.jpg");
//		arrtemp.add("http://galagala.vn:8888//Media/Store/S000001/KhanhToan-Store-SHB-D1-AdsBanner-1.gif");
//		arrtemp.add("http://galagala.vn:8888//Media/Store/S000002/Hana-Store-SHB-D1-AdsBanner-1.jpg");
//		arrtemp.add("http://galagala.vn:8888//Media/Store/S000008/20150508_MALL-2_00c3a40a-e38e-4f47-9cb9-3b47bfb7a33c.jpg");
//		
//		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
//
//		HomePageLayoutSlideImageMalls layoutMall = new HomePageLayoutSlideImageMalls();
//		layoutMall.setDataSource(arrtemp);		
//		arrLayouts.add(layoutMall);
//		
//		ArrayList<String> arrtempstore = new ArrayList<String>();
//		
//		for (int i = 0; i < imageStoreObjects.length; i++) {
//			arrtempstore.add(imageStoreObjects[i]);
//		}
//		
//		ArrayList<ArrayList<String>> dataSources = new ArrayList<ArrayList<String>>();
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//		dataSources.add(arrtempstore);
//
//		HomePageLayoutSlideGridViewStores layoutStore = new HomePageLayoutSlideGridViewStores();
//		layoutStore.setDataSource(dataSources);		
//		arrLayouts.add(layoutStore);
//		
//		ArrayList<String> arrSpecStore = new ArrayList<String>();
//
//		for (int i = 0; i < imageSpecialStoreObjects.length; i++) {
//			arrSpecStore.add(imageSpecialStoreObjects[i]);
//		}
//
//		HomePageLayoutHorizontalScrollViewBrand layoutSpecStore = new HomePageLayoutHorizontalScrollViewBrand();
//		layoutSpecStore.setDataSource(arrSpecStore);		
//		arrLayouts.add(layoutSpecStore);
//		
//		ArrayList<String> arrProduct = new ArrayList<String>();
//		for (int i = 0; i < imageProductObjects.length; i++) {
//			arrProduct.add(imageProductObjects[i]);
//		}
//		
//		HomePageLayoutHorizontalScrollViewProducts layoutProduct = new HomePageLayoutHorizontalScrollViewProducts();
//		layoutProduct.setDataSource(arrProduct);		
//		arrLayouts.add(layoutProduct);
//		
//		HomePageLayoutHorizontalScrollViewProducts layoutProduct1 = new HomePageLayoutHorizontalScrollViewProducts();
//		layoutProduct1.setDataSource(arrProduct);		
//		arrLayouts.add(layoutProduct1);
//		
//		HomePageLayoutHorizontalScrollViewProducts layoutProduct2 = new HomePageLayoutHorizontalScrollViewProducts();
//		layoutProduct2.setDataSource(arrProduct);		
//		arrLayouts.add(layoutProduct2);
//
//		MultiLayoutContentListViewAdapter mainContentAdapter= new MultiLayoutContentListViewAdapter(arrLayouts, getActivity());
//		
//		ListView lsLayoutContainer = (ListView) mView.findViewById(R.id.lsLayoutContain);
//		lsLayoutContainer.setAdapter(mainContentAdapter);
		
		return mView;
	}
	
	@Override
	public void onActivityCreated(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onActivityCreated(savedInstanceState);
		executeTask();
	}
	
	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if (mView != null && mView.getParent() != null) {
//			ListView ls = (ListView) mView.findViewById(R.id.lsLayoutContain);
//			((MultiLayoutContentListViewAdapter) ls.getAdapter()).clearAllLayouts();
//			ls = null;
//			mLvLayoutContainer = null;
//			mInflater = null;
//			mContainer = null;
			mLayoutContain.removeAllViews();
            ((ViewGroup) mView.getParent()).removeView(mView);
            mView = null;
            System.gc();
        }
		super.onDestroyView();
	}
	
	public LinearLayout getLayoutContain() {
		return mLayoutContain;
	}

	public void setLayoutContain(LinearLayout mLayoutContain) {
		this.mLayoutContain = mLayoutContain;
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

	private void LoadHomePageLayout(HomePageDataClass dataSource) {
		
		HomePageLayoutSlideImageMalls layoutMall = new HomePageLayoutSlideImageMalls(mActivity.getApplicationContext());
		layoutMall.setDataSource(dataSource.getMall());
		mLayoutContain.addView(layoutMall.getView(mInflater, mContainer));

		HomePageLayoutNormalCategory layoutCategory = new HomePageLayoutNormalCategory(mActivity.getApplicationContext());
		layoutCategory.addListener(mListener);
		layoutCategory.setDataSource(dataSource.getCategory());
		mLayoutContain.addView(layoutCategory.getView(mInflater, mContainer));

		HomePageLayoutHorizontalScrollViewProducts layoutProductBuy = new HomePageLayoutHorizontalScrollViewProducts(mActivity.getApplicationContext());
		layoutProductBuy.addListener(mListener);
		layoutProductBuy.setDataSource(dataSource.getProductBuy());
		mLayoutContain.addView(layoutProductBuy.getView(mInflater, mContainer));

		HomePageLayoutHorizontalScrollViewBrand layoutBrand = new HomePageLayoutHorizontalScrollViewBrand(mActivity.getApplicationContext());
		layoutBrand.addListener(mListener);
		layoutBrand.setDataSource(dataSource.getBrand());
		mLayoutContain.addView(layoutBrand.getView(mInflater, mContainer));

		HomePageLayoutHorizontalScrollViewProducts layoutProductHot = new HomePageLayoutHorizontalScrollViewProducts(
				mActivity.getApplicationContext(),
				getString(R.string.homepageNewArrival));
		layoutProductHot.addListener(mListener);
		layoutProductHot.setDataSource(dataSource.getProductHot());
		mLayoutContain.addView(layoutProductHot.getView(mInflater, mContainer));

		HomePageLayoutSlideGridViewStores layoutStore = new HomePageLayoutSlideGridViewStores(mActivity.getApplicationContext());
		layoutStore.addListener(mListener);
		layoutStore.setDataSource(dataSource.getStore());
		mLayoutContain.addView(layoutStore.getView(mInflater, mContainer));

		LayoutNormalFooter layoutFooter = new LayoutNormalFooter(mActivity.getApplicationContext());
		layoutFooter.addListener(mListener);
		layoutFooter.setDataSource(dataSource.getFooterData());
		mLayoutContain.addView(layoutFooter.getView(mInflater, mContainer));
//		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
//
//		HomePageLayoutSlideImageMalls layoutMall = new HomePageLayoutSlideImageMalls(mActivity.getApplicationContext());
//		layoutMall.setDataSource(dataSource.getMall());		
//		arrLayouts.add(layoutMall);
//
//		HomePageLayoutHorizontalScrollViewProducts layoutProductBuy = new HomePageLayoutHorizontalScrollViewProducts(mActivity.getApplicationContext());
//		layoutProductBuy.setDataSource(dataSource.getProductBuy());		
//		arrLayouts.add(layoutProductBuy);
//		
//		HomePageLayoutHorizontalScrollViewBrand layoutBrand = new HomePageLayoutHorizontalScrollViewBrand(mActivity.getApplicationContext());
//		layoutBrand.setDataSource(dataSource.getBrand());		
//		arrLayouts.add(layoutBrand);
//		
//		HomePageLayoutHorizontalScrollViewProducts layoutProductHot = new HomePageLayoutHorizontalScrollViewProducts(mActivity.getApplicationContext());
//		layoutProductHot.setDataSource(dataSource.getProductHot());		
//		arrLayouts.add(layoutProductHot);
//
//		HomePageLayoutSlideGridViewStores layoutStore = new HomePageLayoutSlideGridViewStores(mActivity.getApplicationContext());
//		layoutStore.setDataSource(dataSource.getStore());		
//		arrLayouts.add(layoutStore);
//		
//		MultiLayoutContentListViewAdapter mainContentAdapter= new MultiLayoutContentListViewAdapter(arrLayouts, mActivity.getApplicationContext());
//		
//		mLvLayoutContainer.setAdapter(mainContentAdapter);
	}
	
	private HomePageDataClass parserJsonHomePage(String json) {
		HomePageDataClass homePageData = new HomePageDataClass();
		try {
			JSONObject jObject = new JSONObject(json);

			// Load store's data.
			JSONArray jArray = jObject.getJSONArray("store");
			homePageData.getStore().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				StoreInMedia itemStore = StoreInMedia.parseJonData(temp);
				if(i % Utils.MAX_STORE_ON_GRID == 0) {
					ArrayList<StoreInMedia> item = new ArrayList<StoreInMedia>();
					homePageData.getStore().add(item);
				}
				homePageData.getStore().get(i / Utils.MAX_STORE_ON_GRID).add(itemStore);
			}
			
			// Load brand's data
			jArray = jObject.getJSONArray("brand");
			homePageData.getBrand().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				JSONObject oneObject = jArray.getJSONObject(i);
		        Triplet<Brand, Media, Media> item = new Triplet<Brand, Media, Media>(
		        		Brand.parseJonData(oneObject.getString("Item1")), 
		        		Media.parseJonData(oneObject.getString("Item2")), 
		        		Media.parseJonData(oneObject.getString("Item3"))
		        );
			        
				homePageData.getBrand().add(item);
			}
			
			// Load mall's data
			jArray = jObject.getJSONArray("mall");
			homePageData.getMall().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				Media item = Media.parseJonData(temp);
				homePageData.getMall().add(item);
			}
			
			// Load product_hot's data
			jArray = jObject.getJSONArray("product_hot");
			homePageData.getProductHot().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				ProductInMedia item = ProductInMedia.parseJonData(temp);
				homePageData.getProductHot().add(item);
			}
			
			// Load product_buy's data
			jArray = jObject.getJSONArray("product_buy");
			homePageData.getProductBuy().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				ProductInMedia item = ProductInMedia.parseJonData(temp);
				homePageData.getProductBuy().add(item);
			}
			
			// Load category's data
			jArray = jObject.getJSONArray("category");
			homePageData.getCategory().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				JSONObject oneObject = jArray.getJSONObject(i);
		        Quartet<Category, Media, Media,Category_MultiLang> item = new Quartet<Category, Media, Media,Category_MultiLang>(
		        		Category.parseJonData(oneObject.getString("Item1")), 
		        		Media.parseJonData(oneObject.getString("Item2")), 
		        		Media.parseJonData(oneObject.getString("Item3")),
		        		Category_MultiLang.parseJonData(oneObject.getString("Item4"))
		        );
			        
				homePageData.getCategory().add(item);
			}

			// Load articletype's data
			jArray = jObject.getJSONArray("articletype");
			homePageData.getFooterData().getArticleType().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
			    ArticleType item = ArticleType.parseJonData(temp);
				homePageData.getFooterData().getArticleType().add(item);
			}

			// Load article's data
			jArray = jObject.getJSONArray("article");
			homePageData.getFooterData().getArticle().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
			    Article item = Article.parseJonData(temp);
			    for(ArticleType articleType : homePageData.getFooterData().getArticleType()) {
			    	if(item.getArticleTypeId() == articleType.getCode()) {
			    		articleType.getArticles().add(item);
			    		break;
			    	}
			    }
				homePageData.getFooterData().getArticle().add(item);
			}
			
//			// Load CategoryInMenu's data.
//			String url = "http://galagala.vn:88/home/category_app?lang=vi";
//			jArray = new JSONArray(JSONHttpClient.getJsonString(url));
//			homePageData.getCategoryInMenu().clear();
//			for (int i=0; i < jArray.length(); i++)
//			{
//				JSONObject oneObject = jArray.getJSONObject(i);
//				
//				String temp = jObject.getString("Item5");
//				int tempCount = 0;
//				if (!temp.equals("null")) {
//					tempCount = Integer.parseInt(temp);
//				}
//				
//		        Quintet<Category, Media, Media,Category_MultiLang, Integer> item = new Quintet<Category, Media, Media,Category_MultiLang, Integer>(
//		        		Category.parseJonData(oneObject.getString("Item1")), 
//		        		Media.parseJonData(oneObject.getString("Item2")), 
//		        		Media.parseJonData(oneObject.getString("Item3")),
//		        		Category_MultiLang.parseJonData(oneObject.getString("Item4")),
//		        		tempCount
//		        );
//				homePageData.getCategoryInMenu().add(item);
//			}
			
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			Log.e(TAG, "parserJson error:", e.getCause());
		}
		return homePageData;
	}
	
	@Override
	public void executeTask() {
		// TODO Auto-generated method stub
		LoadJsonDataTask<HomePageDataClass> task = new LoadJsonDataTask<HomePageDataClass>(getActivity());
		task.setTaskListener(this);
		task.execute();
	}

	@Override
	public void onTaskComplete(HomePageDataClass result) {
		// TODO Auto-generated method stub
		LoadHomePageLayout(result);
	}

	@Override
	public HomePageDataClass parserJson() {
		// TODO Auto-generated method stub
		String url = "http://galagala.vn:88/home/home_app";
		return parserJsonHomePage(JSONHttpClient.getJsonString(url));
	}
}
