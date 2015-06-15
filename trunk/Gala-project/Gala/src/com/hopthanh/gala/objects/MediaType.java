package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class MediaType {

	public static MediaType parseJonData(String json) {
		MediaType result = new MediaType();
		try {
			JSONObject jObject = new JSONObject(json);
			String temp = jObject.getString("MediaTypeId");
			if (!temp.equals("null")) {
				result.MediaTypeId = Long.parseLong(temp);
			}
			
			temp = jObject.getString("ParentID");
			if (!temp.equals("null")) {
				result.ParentID = Long.parseLong(temp);
			}
			
			result.MediaTypeName = jObject.getString("MediaTypeName");
			result.MediaTypeCode = jObject.getString("MediaTypeCode");
			result.FolderPath = jObject.getString("FolderPath");
			
			temp = jObject.getString("RWidth");
			if (!temp.equals("null")) {
				result.RWidth = Integer.parseInt(temp);
			}

			temp = jObject.getString("RHeight");
			if (!temp.equals("null")) {
				result.RHeight = Integer.parseInt(temp);
			}

			temp = jObject.getString("MinSize");
			if (!temp.equals("null")) {
				result.MinSize = Long.parseLong(temp);
			}
			
			temp = jObject.getString("MaxSize");
			if (!temp.equals("null")) {
				result.MaxSize = Long.parseLong(temp);
			}

			result.Description = jObject.getString("Description");
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return result;
	}

	public long getMediaTypeId() {
		return MediaTypeId;
	}

	public void setMediaTypeId(long mediaTypeId) {
		MediaTypeId = mediaTypeId;
	}

	public long getParentID() {
		return ParentID;
	}

	public void setParentID(long parentID) {
		ParentID = parentID;
	}

	public int getFrameNumb() {
		return FrameNumb;
	}

	public void setFrameNumb(int frameNumb) {
		FrameNumb = frameNumb;
	}

	public String getMediaTypeName() {
		return MediaTypeName;
	}

	public void setMediaTypeName(String mediaTypeName) {
		MediaTypeName = mediaTypeName;
	}

	public String getMediaTypeCode() {
		return MediaTypeCode;
	}

	public void setMediaTypeCode(String mediaTypeCode) {
		MediaTypeCode = mediaTypeCode;
	}

	public String getFolderPath() {
		return FolderPath;
	}

	public void setFolderPath(String folderPath) {
		FolderPath = folderPath;
	}

	public int getRWidth() {
		return RWidth;
	}

	public void setRWidth(int rWidth) {
		RWidth = rWidth;
	}

	public int getRHeight() {
		return RHeight;
	}

	public void setRHeight(int rHeight) {
		RHeight = rHeight;
	}

	public long getMinSize() {
		return MinSize;
	}

	public void setMinSize(long minSize) {
		MinSize = minSize;
	}

	public long getMaxSize() {
		return MaxSize;
	}

	public void setMaxSize(long maxSize) {
		MaxSize = maxSize;
	}

	public String getDescription() {
		return Description;
	}

	public void setDescription(String description) {
		Description = description;
	}

	private long MediaTypeId;
	private long ParentID;
	private int FrameNumb;
	private String MediaTypeName;
	private String MediaTypeCode;
	private String FolderPath;
	private int RWidth;
	private int RHeight;
	private long MinSize;
	private long MaxSize;
	private String Description;
}
