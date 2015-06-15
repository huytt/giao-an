package com.hopthanh.gala.objects;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashSet;

import org.json.JSONException;
import org.json.JSONObject;

import com.google.gson.GsonBuilder;

public class Media {
    public Media()
    {
        this.setProductInMedia(new HashSet<ProductInMedia>());
        this.setStoreInMedia(new HashSet<StoreInMedia>());
    }

    public static Media parseJonData(String json) {
    	Media result = new Media();
    	try {
			JSONObject jObject = new JSONObject(json);
			result.MediaId = Long.parseLong(jObject.getString("MediaId"));
		    result.MediaTypeId = Long.parseLong(jObject.getString("MediaTypeId"));
		    result.MediaName = jObject.getString("MediaName");
		    result.Url = jObject.getString("MediaName");
		    DateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss"); 
		    
		    String tempDate = jObject.getString("DateCreated");
		    if(!tempDate.equals("null")) {
		    	result.DateCreated = df.parse(tempDate);
		    }
			
		    tempDate = jObject.getString("DateModified");
		    if(!tempDate.equals("null")) {
		    	result.DateModified = df.parse(tempDate);
		    }
		    
		    result.IsActive = Boolean.parseBoolean(jObject.getString("IsActive"));
		    result.IsDeleted = Boolean.parseBoolean(jObject.getString("IsDeleted"));
		    
		    String test1 = jObject.getString("ProductInMedia");
		    String test2 = jObject.getString("StoreInMedia");
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    	
    	return result;
    }
    
    public long getMediaId() {
		return MediaId;
	}
	public void setMediaId(long mediaId) {
		MediaId = mediaId;
	}

	public long getMediaTypeId() {
		return MediaTypeId;
	}

	public void setMediaTypeId(long mediaTypeId) {
		MediaTypeId = mediaTypeId;
	}

	public String getMediaName() {
		return MediaName;
	}

	public void setMediaName(String mediaName) {
		MediaName = mediaName;
	}

	public String getUrl() {
		return Url;
	}

	public void setUrl(String url) {
		Url = url;
	}

	public Date getDateCreated() {
		return DateCreated;
	}

	public void setDateCreated(Date dateCreated) {
		DateCreated = dateCreated;
	}

	public Date getDateModified() {
		return DateModified;
	}

	public void setDateModified(Date dateModified) {
		DateModified = dateModified;
	}

	public boolean isActive() {
		return IsActive;
	}

	public void setActive(boolean isActive) {
		IsActive = isActive;
	}

	public boolean isDeleted() {
		return IsDeleted;
	}

	public void setDeleted(boolean isDeleted) {
		IsDeleted = isDeleted;
	}

	public MediaType getMediaType() {
		return MediaType;
	}

	public void setMediaType(MediaType mediaType) {
		MediaType = mediaType;
	}

	public HashSet<ProductInMedia> getProductInMedia() {
		return ProductInMedia;
	}

	public void setProductInMedia(HashSet<ProductInMedia> productInMedia) {
		ProductInMedia = productInMedia;
	}

	public HashSet<StoreInMedia> getStoreInMedia() {
		return StoreInMedia;
	}

	public void setStoreInMedia(HashSet<StoreInMedia> storeInMedia) {
		StoreInMedia = storeInMedia;
	}

	private long MediaId;
    private long MediaTypeId;
    private String MediaName;
    private String Url;
    private Date DateCreated;
    private Date DateModified;
    private boolean IsActive;
    private boolean IsDeleted;
    
    private MediaType MediaType;
    private HashSet<ProductInMedia> ProductInMedia;
    private HashSet<StoreInMedia> StoreInMedia;
}
