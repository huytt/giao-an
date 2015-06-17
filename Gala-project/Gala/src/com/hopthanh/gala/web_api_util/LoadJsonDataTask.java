package com.hopthanh.gala.web_api_util;

import java.util.ArrayList;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.ConditionVariable;
import android.util.Log;
import android.view.ViewGroup;

import com.hopthanh.gala.app.HomePageFragment;
import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewBrand;
import com.hopthanh.gala.layout.HomePageLayoutHorizontalScrollViewProducts;
import com.hopthanh.gala.layout.HomePageLayoutSlideGridViewStores;
import com.hopthanh.gala.layout.HomePageLayoutSlideImageMalls;
import com.hopthanh.gala.objects.Brand;
import com.hopthanh.gala.objects.HomePageDataClass;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.ProductInMedia;
import com.hopthanh.gala.objects.StoreInMedia;
import com.hopthanh.gala.utils.Utils;

import org.javatuples.Triplet;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class LoadJsonDataTask<T> extends  AsyncTask<String, T, T> {
	private static final String TAG = "LoadHomePageDataTask.java";
	private ProgressDialog progressDialog;
	private ITaskLoadJsonDataListener<T> taskListener;
	private Activity mActivity;
	
	public LoadJsonDataTask (Activity activity) {
		this.mActivity = activity;
	}
	
//	private void parserJson(String json) {
//		try {
//			JSONObject jObject = new JSONObject(json);
//
//			// Load store's data.
//			JSONArray jArray = jObject.getJSONArray("store");
////			HomePageDataClass mHomepageData = mHomePageFragment.getHomePageData();
//			mHomepageData.getStore().clear();
//			for (int i=0; i < jArray.length(); i++)
//			{
//				String temp = jArray.getString(i);
//				StoreInMedia itemStore = StoreInMedia.parseJonData(temp);
//				if(i % Utils.MAX_STORE_ON_GRID == 0) {
//					ArrayList<StoreInMedia> item = new ArrayList<StoreInMedia>();
//					mHomepageData.getStore().add(item);
//				}
//				mHomepageData.getStore().get(i / Utils.MAX_STORE_ON_GRID).add(itemStore);
//			}
//			
//			// Load brand's data
//			jArray = jObject.getJSONArray("brand");
//			mHomepageData.getBrand().clear();
//			for (int i=0; i < jArray.length(); i++)
//			{
//				JSONObject oneObject = jArray.getJSONObject(i);
//		        Triplet<Brand, Media, Media> item = new Triplet<Brand, Media, Media>(
//		        		Brand.parseJonData(oneObject.getString("Item1")), 
//		        		Media.parseJonData(oneObject.getString("Item2")), 
//		        		Media.parseJonData(oneObject.getString("Item3"))
//		        );
//			        
//				mHomepageData.getBrand().add(item);
//			}
//			
//			// Load mall's data
//			jArray = jObject.getJSONArray("mall");
//			mHomepageData.getMall().clear();
//			for (int i=0; i < jArray.length(); i++)
//			{
//				String temp = jArray.getString(i);
//				Media item = Media.parseJonData(temp);
//				mHomepageData.getMall().add(item);
//			}
//			
//			// Load product_hot's data
//			jArray = jObject.getJSONArray("product_hot");
//			mHomepageData.getProductHot().clear();
//			for (int i=0; i < jArray.length(); i++)
//			{
//				String temp = jArray.getString(i);
//				ProductInMedia item = ProductInMedia.parseJonData(temp);
//				mHomepageData.getProductHot().add(item);
//			}
//			
//			// Load product_buy's data
//			jArray = jObject.getJSONArray("product_buy");
//			mHomepageData.getProductBuy().clear();
//			for (int i=0; i < jArray.length(); i++)
//			{
//				String temp = jArray.getString(i);
//				ProductInMedia item = ProductInMedia.parseJonData(temp);
//				mHomepageData.getProductBuy().add(item);
//			}
//		} catch (JSONException e) {
//			// TODO Auto-generated catch block
//			Log.e(TAG, "parserJson error:", e.getCause());
//		}
//	}
	
	@Override
	protected T doInBackground(String... params) {
		// TODO Auto-generated method stub
		return taskListener.parserJson();
	}

	@Override
    protected void onPreExecute() {
        super.onPreExecute();    //To change body of overridden methods use File | Settings | File Templates.
        progressDialog = new ProgressDialog(mActivity);
        progressDialog.setMessage("Loading. Please wait...");
        progressDialog.show();
    }

    @Override
    protected void onPostExecute(T result) {
        progressDialog.dismiss();
        taskListener.onTaskComplete(result);
    }

	public ITaskLoadJsonDataListener<T> getTaskListener() {
		return taskListener;
	}

	public void setTaskListener(ITaskLoadJsonDataListener<T> taskListener) {
		this.taskListener = taskListener;
	}
}
