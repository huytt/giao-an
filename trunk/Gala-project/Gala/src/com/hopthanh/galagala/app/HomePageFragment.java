package com.hopthanh.galagala.app;

import java.util.ArrayList;

import org.javatuples.Quartet;
import org.javatuples.Triplet;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

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
import com.hopthanh.gala.objects.HomePageDataClass;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.ProductInMedia;
import com.hopthanh.gala.objects.StoreInMedia;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.gala.web_api_util.ITaskLoadJsonDataListener;
import com.hopthanh.gala.web_api_util.JSONHttpClient;
import com.hopthanh.gala.web_api_util.LoadJsonDataTask;
import com.hopthanh.galagala.app.R;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.Toast;


public class HomePageFragment extends AbstractLayoutFragment<Object> implements ITaskLoadJsonDataListener<HomePageDataClass> {
	private static final String TAG = "HomePageFragment";

	private LinearLayout mLayoutContain = null;
	private FragmentActivity mActivity = null;
	private LayoutInflater mInflater = null;
	private ViewGroup mContainer = null;
	private HomePageLayoutSlideImageMalls mLayoutMall = null;
	private HomePageLayoutNormalCategory mLayoutCategory = null;
	private HomePageLayoutHorizontalScrollViewProducts mLayoutProductBuy = null;
	private HomePageLayoutHorizontalScrollViewBrand mLayoutBrand = null;
	private HomePageLayoutHorizontalScrollViewProducts mLayoutProductHot = null;
	private HomePageLayoutSlideGridViewStores mLayoutStore = null;
	private LayoutNormalFooter mLayoutFooter = null;
	
	public HomePageFragment() {
		super();
	}
	
	@Override
	public void onAttach(Activity activity) {
		// TODO Auto-generated method stub
		super.onAttach(activity);
		mActivity = (FragmentActivity) activity;
	}
	
	@Override
	public void onDetach() {
		// TODO Auto-generated method stub
		super.onDetach();
		mActivity = null;
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub

		Thread.dumpStack();
		Log.e("=====HomePageFragment========","=====onCreateView=====");
		mView = inflater.inflate(R.layout.home_page_fragment_main_edit, container, false);
		mLayoutContain = (LinearLayout) mView.findViewById(R.id.lnContain);
		mInflater = inflater;
		mContainer = container;

		return mView;
	}
	
	@Override
	public void onActivityCreated(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onActivityCreated(savedInstanceState);
		if(Utils.checkNetwork(mActivity.getApplicationContext())) {
			executeTask();
		} else {
			Toast.makeText(mActivity.getApplicationContext(), R.string.OSD_network_disconnect,Toast.LENGTH_LONG).show();
		}
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
			if(mLayoutMall != null) mLayoutMall.Destroy();
			if(mLayoutCategory != null) mLayoutCategory.Destroy();
			if(mLayoutProductBuy != null) mLayoutProductBuy.Destroy();
			if(mLayoutBrand != null) mLayoutBrand.Destroy();
			if(mLayoutProductHot != null) mLayoutProductHot.Destroy();
			if(mLayoutStore != null) mLayoutStore.Destroy();
			if(mLayoutFooter != null) mLayoutFooter.Destroy();
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

	private void LoadHomePageLayout(HomePageDataClass dataSource) {
		mLayoutMall = new HomePageLayoutSlideImageMalls(mActivity.getApplicationContext());
		mLayoutMall.setDataSource(dataSource.getMall());
		mLayoutContain.addView(mLayoutMall.getView(mInflater, mContainer));

		mLayoutCategory = new HomePageLayoutNormalCategory(mActivity.getApplicationContext());
		mLayoutCategory.addListener(mListener);
		mLayoutCategory.setDataSource(dataSource.getCategory());
		mLayoutContain.addView(mLayoutCategory.getView(mInflater, mContainer));

		mLayoutProductBuy = new HomePageLayoutHorizontalScrollViewProducts(mActivity.getApplicationContext());
		mLayoutProductBuy.addListener(mListener);
		mLayoutProductBuy.setTypeFilter(AbstractLayout.TYPE_FILTER_SELLING);
		mLayoutProductBuy.setDataSource(dataSource.getProductBuy());
		mLayoutContain.addView(mLayoutProductBuy.getView(mInflater, mContainer));

		mLayoutBrand = new HomePageLayoutHorizontalScrollViewBrand(mActivity.getApplicationContext());
		mLayoutBrand.addListener(mListener);
		mLayoutBrand.setDataSource(dataSource.getBrand());
		mLayoutContain.addView(mLayoutBrand.getView(mInflater, mContainer));

		mLayoutProductHot = new HomePageLayoutHorizontalScrollViewProducts(
				mActivity.getApplicationContext(),
				getString(R.string.homepageNewArrival));
		mLayoutProductHot.addListener(mListener);
		mLayoutProductBuy.setTypeFilter(AbstractLayout.TYPE_FILTER_NEW);
		mLayoutProductHot.setDataSource(dataSource.getProductHot());
		mLayoutContain.addView(mLayoutProductHot.getView(mInflater, mContainer));

		mLayoutStore = new HomePageLayoutSlideGridViewStores(mActivity.getApplicationContext());
		mLayoutStore.addListener(mListener);
		mLayoutStore.setDataSource(dataSource.getStore());
		mLayoutContain.addView(mLayoutStore.getView(mInflater, mContainer));

		mLayoutFooter = new LayoutNormalFooter(mActivity.getApplicationContext());
		mLayoutFooter.addListener(mListener);
		mLayoutFooter.setDataSource(dataSource.getFooterData());
		mLayoutContain.addView(mLayoutFooter.getView(mInflater, mContainer));
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
		String url = "http://galagala.vn:88/home/home_app?lang="+LanguageManager.getInstance().getCurrentLanguage();
//		String url = "http://galagala.vn:88/home/home_app";
		return parserJsonHomePage(JSONHttpClient.getJsonString(url));
	}
}
