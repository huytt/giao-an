package com.hopthanh.gala.web_api_util;

import java.util.ArrayList;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.util.Log;
import android.view.ViewGroup;

import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewBrand;
import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewProducts;
import com.hopthanh.gala.layout.HomePageLayoutSlideGridViewStores;
import com.hopthanh.gala.layout.HomePageLayoutSlideImageMalls;
import com.hopthanh.gala.objects.Brand;
import com.hopthanh.gala.objects.HomePageDataClass;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.ProductInMedia;
import com.hopthanh.gala.objects.StoreInMedia;
import com.hopthanh.galagala.app.HomePageFragment;

import org.javatuples.Triplet;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class LoadHomePageDataTask extends  AsyncTask<String, String, String> {

	private final int MAX_STORE_ON_GRID = 6;
	private static final String TAG = "LoadHomePageDataTask";
	private ProgressDialog progressDialog;
	private HomePageFragment mHomePageFragment;
	private ITaskLoadJsonDataListener<HomePageDataClass> taskListener;
	private Activity test;
	private HomePageDataClass mHomepageData;
	
	public LoadHomePageDataTask (HomePageFragment homePageFragment) {
		this.mHomePageFragment = homePageFragment;
	}
	
	public LoadHomePageDataTask (Activity test) {
		this.test = test;
		this.mHomepageData = new HomePageDataClass();
	}
	
	private void parserJson(String json) {
		try {
			JSONObject jObject = new JSONObject(json);
			

			// Load store's data.
			JSONArray jArray = jObject.getJSONArray("store");
//			HomePageDataClass mHomepageData = mHomePageFragment.getHomePageData();
			mHomepageData.getStore().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				StoreInMedia itemStore = StoreInMedia.parseJonData(temp);
				if(i % MAX_STORE_ON_GRID == 0) {
					ArrayList<StoreInMedia> item = new ArrayList<StoreInMedia>();
					mHomepageData.getStore().add(item);
				}
				mHomepageData.getStore().get(i / MAX_STORE_ON_GRID).add(itemStore);
			}
			
			// Load brand's data
			jArray = jObject.getJSONArray("brand");
			mHomepageData.getBrand().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				JSONObject oneObject = jArray.getJSONObject(i);
		        Triplet<Brand, Media, Media> item = new Triplet<Brand, Media, Media>(
		        		Brand.parseJonData(oneObject.getString("Item1")), 
		        		Media.parseJonData(oneObject.getString("Item2")), 
		        		Media.parseJonData(oneObject.getString("Item3"))
		        );
			        
				mHomepageData.getBrand().add(item);
			}
			
			// Load mall's data
			jArray = jObject.getJSONArray("mall");
			mHomepageData.getMall().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				Media item = Media.parseJonData(temp);
				mHomepageData.getMall().add(item);
			}
			
			// Load product_hot's data
			jArray = jObject.getJSONArray("product_hot");
			mHomepageData.getProductHot().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				ProductInMedia item = ProductInMedia.parseJonData(temp);
				mHomepageData.getProductHot().add(item);
			}
			
			// Load product_buy's data
			jArray = jObject.getJSONArray("product_buy");
			mHomepageData.getProductBuy().clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				ProductInMedia item = ProductInMedia.parseJonData(temp);
				mHomepageData.getProductBuy().add(item);
			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			Log.e(TAG, "parserJson error:", e.getCause());
		}
	}
	
	@Override
	protected String doInBackground(String... params) {
		// TODO Auto-generated method stub
		String url = "http://galagala.vn:88/home/home_app";
//		parserJson(JSONHttpClient.getJsonString(url));
		return null;
	}

	@Override
    protected void onPreExecute() {
        super.onPreExecute();    //To change body of overridden methods use File | Settings | File Templates.
        progressDialog = new ProgressDialog(test);
        progressDialog.setMessage("Loading. Please wait...");
        progressDialog.show();
    }

    @Override
    protected void onPostExecute(String s) {
        progressDialog.dismiss();
        taskListener.onTaskComplete(mHomepageData);
//        this.mHomePageFragment.getActivity().runOnUiThread(new Runnable() {
//            @Override
//            public void run() {
//                Log.e("=====huytt=======load", "Load done");
//                HomePageLayoutSlideImageMalls layoutMall = new HomePageLayoutSlideImageMalls();
//                layoutMall.setDataSource(mHomePageFragment.getHomePageData().getMall());
//                mHomePageFragment.getLayoutContain().addView(layoutMall.getView(
//                		mHomePageFragment.getActivity(), 
//                		mHomePageFragment.getActivity().getLayoutInflater(),
//                		(ViewGroup) mHomePageFragment.getView().getParent()));
//                
//        		HomePageLayoutHorizontalScrollViewProducts layoutProductBuy = new HomePageLayoutHorizontalScrollViewProducts();
//        		layoutProductBuy.setDataSource(mHomePageFragment.getHomePageData().getProductBuy());
//        		mHomePageFragment.getLayoutContain().addView(layoutProductBuy.getView(
//                		mHomePageFragment.getActivity(), 
//                		mHomePageFragment.getActivity().getLayoutInflater(),
//                		(ViewGroup) mHomePageFragment.getView().getParent()));
//        		
//        		HomePageLayoutHorizontalScrollViewBrand layoutBrand = new HomePageLayoutHorizontalScrollViewBrand();
//        		layoutBrand.setDataSource(mHomePageFragment.getHomePageData().getBrand());		
//        		mHomePageFragment.getLayoutContain().addView(layoutBrand.getView(
//                		mHomePageFragment.getActivity(), 
//                		mHomePageFragment.getActivity().getLayoutInflater(),
//                		(ViewGroup) mHomePageFragment.getView().getParent()));
//        		
//        		HomePageLayoutHorizontalScrollViewProducts layoutProductHot = new HomePageLayoutHorizontalScrollViewProducts();
//        		layoutProductHot.setDataSource(mHomePageFragment.getHomePageData().getProductHot());
//        		mHomePageFragment.getLayoutContain().addView(layoutProductHot.getView(
//                		mHomePageFragment.getActivity(), 
//                		mHomePageFragment.getActivity().getLayoutInflater(),
//                		(ViewGroup) mHomePageFragment.getView().getParent()));
//        		
//        		HomePageLayoutSlideGridViewStores layoutStore = new HomePageLayoutSlideGridViewStores();
//        		layoutStore.setDataSource(mHomePageFragment.getHomePageData().getStore());
//        		mHomePageFragment.getLayoutContain().addView(layoutStore.getView(
//                		mHomePageFragment.getActivity(), 
//                		mHomePageFragment.getActivity().getLayoutInflater(),
//                		(ViewGroup) mHomePageFragment.getView().getParent()));
//            }
//        });
    }
}
