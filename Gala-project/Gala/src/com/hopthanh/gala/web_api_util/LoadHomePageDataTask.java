package com.hopthanh.gala.web_api_util;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.util.Log;

import com.hopthanh.gala.objects.Brand;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.ProductInMedia;
import com.hopthanh.gala.objects.StoreInMedia;

import org.javatuples.Triplet;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;


public class LoadHomePageDataTask extends  AsyncTask<String, String, String> {

	private ProgressDialog progressDialog;
	private Activity mActivity;
	private HomePageDataClass mHomepageData;
	
	public LoadHomePageDataTask (Activity activity) {
		this.mActivity = activity;
		mHomepageData = new HomePageDataClass();
	}
	
	private void parserJson(String json) {
		try {
			JSONObject jObject = new JSONObject(json);
			

			// Load store's data.
			JSONArray jArray = jObject.getJSONArray("store");
			mHomepageData.store.clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				StoreInMedia item = StoreInMedia.parseJonData(temp);
				mHomepageData.store.add(item);
			}
			
			// Load brand's data
			jArray = jObject.getJSONArray("brand");
			mHomepageData.brand.clear();
			for (int i=0; i < jArray.length(); i++)
			{
				JSONObject oneObject = jArray.getJSONObject(i);
		        Triplet<Brand, Media, Media> item = new Triplet<Brand, Media, Media>(
		        		Brand.parseJonData(oneObject.getString("Item1")), 
		        		Media.parseJonData(oneObject.getString("Item2")), 
		        		Media.parseJonData(oneObject.getString("Item3"))
		        );
			        
				mHomepageData.brand.add(item);
			}
			
			// Load mall's data
			jArray = jObject.getJSONArray("mall");
			mHomepageData.mall.clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				Media item = Media.parseJonData(temp);
				mHomepageData.mall.add(item);
			}
			
			// Load product_hot's data
			jArray = jObject.getJSONArray("product_hot");
			mHomepageData.product_hot.clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				ProductInMedia item = ProductInMedia.parseJonData(temp);
				mHomepageData.product_hot.add(item);
			}
			
			// Load product_buy's data
			jArray = jObject.getJSONArray("product_buy");
			mHomepageData.product_buy.clear();
			for (int i=0; i < jArray.length(); i++)
			{
				String temp = jArray.getString(i);
				ProductInMedia item = ProductInMedia.parseJonData(temp);
				mHomepageData.product_buy.add(item);
			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	@Override
	protected String doInBackground(String... params) {
		// TODO Auto-generated method stub
		String url = "http://galagala.vn:88/home/home_app";
		parserJson(JSONHttpClient.getJsonString(url));
		return null;
	}

	@Override
    protected void onPreExecute() {
        super.onPreExecute();    //To change body of overridden methods use File | Settings | File Templates.
        progressDialog = new ProgressDialog(mActivity);
        progressDialog.setMessage("Loading. Please wait...");
        progressDialog.show();
    }

    @Override
    protected void onPostExecute(String s) {
        progressDialog.dismiss();
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.e("=====huytt=======load", "Load done");
            }
        });
    }
    
    private class HomePageDataClass {
    	
    	public HomePageDataClass() {
    		store = new ArrayList<StoreInMedia>();
    		brand = new ArrayList<Triplet<Brand, Media, Media>>();
    		mall = new ArrayList<Media>();
    		product_hot = new ArrayList<ProductInMedia>();
    		product_buy = new ArrayList<ProductInMedia>();
    	}
    	
    	public ArrayList<StoreInMedia> store;
    	public ArrayList<Triplet<Brand, Media, Media>> brand;
    	public ArrayList<Media> mall;
    	public ArrayList<ProductInMedia> product_hot;
    	public ArrayList<ProductInMedia> product_buy;
    };
}
