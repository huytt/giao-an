package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class StoreInMedia {
    private long StoreInMediaId;
    private long StoreId;
    private long MediaId;
    
    private Media mMedia;
    private Store mStore;
    
    public static StoreInMedia parseJonData(String json) {
		if(json.equals("null")) {
			return null;
		}

    	StoreInMedia result = new StoreInMedia();
    	try {
			JSONObject jObject = new JSONObject(json);
		    result.StoreInMediaId = Long.parseLong(jObject.getString("StoreInMediaId"));
		    result.StoreId = Long.parseLong(jObject.getString("StoreId"));
		    result.MediaId = Long.parseLong(jObject.getString("MediaId"));
		    result.mMedia = Media.parseJonData(jObject.getString("Media"));
		    result.mStore = Store.parseJonData(jObject.getString("Store"));
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
    	return result;
    }
    
	public long getStoreInMediaId() {
		return StoreInMediaId;
	}
	public void setStoreInMediaId(long storeInMediaId) {
		StoreInMediaId = storeInMediaId;
	}
	public long getStoreId() {
		return StoreId;
	}
	public void setStoreId(long storeId) {
		StoreId = storeId;
	}
	public long getMediaId() {
		return MediaId;
	}
	public void setMediaId(long mediaId) {
		MediaId = mediaId;
	}
	public Media getMedia() {
		return mMedia;
	}
	public void setMedia(Media media) {
		mMedia = media;
	}
	public Store getStore() {
		return mStore;
	}
	public void setStore(Store store) {
		mStore = store;
	}
}
