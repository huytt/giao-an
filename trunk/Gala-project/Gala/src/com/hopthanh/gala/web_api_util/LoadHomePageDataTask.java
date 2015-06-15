package com.hopthanh.gala.web_api_util;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.hopthanh.gala.objects.Brand;
import com.hopthanh.gala.objects.Product;
import com.hopthanh.gala.objects.Store;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.StoreInMedia;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.util.Log;


public class LoadHomePageDataTask extends  AsyncTask<String, String, String> {

	private ProgressDialog progressDialog;
	private Activity mActivity;
	
	public LoadHomePageDataTask (Activity activity) {
		this.mActivity = activity;
	}
	
	@SuppressWarnings("deprecation")
	private String getJson(String url) {
		DefaultHttpClient httpclient = new DefaultHttpClient(
				new BasicHttpParams());
		HttpPost httppost = new HttpPost(url);
		// Depends on your web service
		httppost.setHeader("Content-type", "application/json");

		InputStream inputStream = null;
		String result = null;
		try {
			HttpResponse response = httpclient.execute(httppost);
			HttpEntity entity = response.getEntity();

			inputStream = entity.getContent();
			// json is UTF-8 by default
			BufferedReader reader = new BufferedReader(new InputStreamReader(
					inputStream, "UTF-8"), 8);
			StringBuilder sb = new StringBuilder();

			String line = null;
			while ((line = reader.readLine()) != null) {
				sb.append(line + "\n");
			}
			result = sb.toString();
		} catch (Exception e) {
			// Oops
		} finally {
			try {
				if (inputStream != null)
					inputStream.close();
			} catch (Exception squish) {
			}
		}
		
		return result;
	}
	
	private void parserJson(String json) {
		try {
			JSONObject jObject = new JSONObject(json);
			
			testClass test = new testClass();
			JSONArray jArray = jObject.getJSONArray("store");
			test.store.clear();
			for (int i=0; i < jArray.length(); i++)
			{
			    try {
			        String temp = jArray.getString(i);
			        StoreInMedia item = StoreInMedia.parseJonData(temp);
			        test.store.add(item);
			    } catch (JSONException e) {
			        // Oops
			    	e.printStackTrace();
			    }
			}
			
			jArray = jObject.getJSONArray("brand");
			
			jArray = jObject.getJSONArray("mall");
			test.mall.clear();
			for (int i=0; i < jArray.length(); i++)
			{
			    try {
			        String temp = jArray.getString(i);
			        Media item = Media.parseJonData(temp);
			        test.mall.add(item);
			    } catch (JSONException e) {
			        // Oops
			    	e.printStackTrace();
			    }
			}
			
			jArray = jObject.getJSONArray("product_hot");
			test.product_hot.clear();
			for (int i=0; i < jArray.length(); i++)
			{
			    try {
			        String temp = jArray.getString(i);
			        Product item = Product.parseJonData(temp);
			        test.product_hot.add(item);
			    } catch (JSONException e) {
			        // Oops
			    	e.printStackTrace();
			    }
			}
			
			jArray = jObject.getJSONArray("product_buy");
			test.product_buy.clear();
			for (int i=0; i < jArray.length(); i++)
			{
			    try {
			        String temp = jArray.getString(i);
			        Product item = Product.parseJonData(temp);
			        test.product_buy.add(item);
			    } catch (JSONException e) {
			        // Oops
			    	e.printStackTrace();
			    }
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
		parserJson(getJson(url));
		
//        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
//        JSONHttpClient jsonHttpClient = new JSONHttpClient();
//        testClass test = jsonHttpClient.Get(url, nameValuePairs, testClass.class);

		
		return null;
	}

	@Override
    protected void onPreExecute() {
        super.onPreExecute();    //To change body of overridden methods use File | Settings | File Templates.
        progressDialog = new ProgressDialog(mActivity);
        progressDialog.setMessage("Loading products. Please wait...");
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
    
    private class testClass {
    	
    	public testClass() {
    		store = new ArrayList<StoreInMedia>();
    		brand = new ArrayList<Brand>();
    		mall = new ArrayList<Media>();
    		product_hot = new ArrayList<Product>();
    		product_buy = new ArrayList<Product>();
    	}
    	
    	public ArrayList<StoreInMedia> store;
    	public ArrayList<Brand> brand;
    	public ArrayList<Media> mall;
    	public ArrayList<Product> product_hot;
    	public ArrayList<Product> product_buy;
    };
}
