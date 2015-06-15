package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class Media {
    public static Media parseJonData(String json) {
    	Media result = new Media();
    	try {
			JSONObject jObject = new JSONObject(json);
			String temp = jObject.getString("MediaId");
			if (!temp.equals("null")) {
				result.MediaId = Long.parseLong(temp);
			}
			
			temp = jObject.getString("MediaTypeId");
			if (!temp.equals("null")) {
				result.MediaTypeId = Long.parseLong(temp);
			}

		    result.MediaName = jObject.getString("MediaName");
		    result.Url = jObject.getString("MediaName");
		    result.mMediaType = MediaType.parseJonData(jObject.getString("MediaType"));
		    
//		    DateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss"); 
		    
//		    String tempDate = jObject.getString("DateCreated");
//		    if(!tempDate.equals("null")) {
//		    	result.DateCreated = df.parse(tempDate);
//		    }
//			
//		    tempDate = jObject.getString("DateModified");
//		    if(!tempDate.equals("null")) {
//		    	result.DateModified = df.parse(tempDate);
//		    }
		    
		} catch (JSONException e) {
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

	public MediaType getMediaType() {
		return mMediaType;
	}

	public void setMediaType(MediaType mediaType) {
		mMediaType = mediaType;
	}

	private long MediaId;
    private long MediaTypeId;
    private String MediaName;
    private String Url;
    private MediaType mMediaType;
};
