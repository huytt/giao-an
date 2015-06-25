package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class Store {
	public static Store parseJonData(String json) {
		if(json.equals("null")) {
			return null;
		}

		Store result = new Store();
    	try {
			JSONObject jObject = new JSONObject(json);

			String temp = jObject.getString("StoreId");
			if (!temp.equals("null")) {
				result.StoreId = Long.parseLong(temp);
			}

			temp = jObject.getString("VendorId");
			if (!temp.equals("null")) {
				result.VendorId = Long.parseLong(temp);
			}
			
		    result.StoreCode = jObject.getString("StoreCode");
		    result.StoreName = jObject.getString("StoreName");
		    result.StoreComplexName = jObject.getString("StoreComplexName");
		    result.Description = jObject.getString("Description");
		    result.Alias = jObject.getString("Alias");
		    result.Keywords = jObject.getString("Keywords");
		    
		    temp = jObject.getString("VisitCount");
			if (!temp.equals("null")) {
				result.VisitCount = Long.parseLong(temp);
			}
			
			temp = jObject.getString("ShowInMallPage");
			if (!temp.equals("null")) {
				result.ShowInMallPage = Boolean.parseBoolean(temp);
			}

		    result.MetaTitle = jObject.getString("MetaTitle");
		    result.MetaKeywords = jObject.getString("MetaKeywords");
		    result.MetaDescription = jObject.getString("MetaDescription");
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
    	return result;
    }
	
    public long getStoreId() {
		return StoreId;
	}
	public void setStoreId(long storeId) {
		StoreId = storeId;
	}

	public long getVendorId() {
		return VendorId;
	}

	public void setVendorId(long vendorId) {
		VendorId = vendorId;
	}

	public String getStoreCode() {
		return StoreCode;
	}

	public void setStoreCode(String storeCode) {
		StoreCode = storeCode;
	}

	public String getStoreName() {
		return StoreName;
	}

	public void setStoreName(String storeName) {
		StoreName = storeName;
	}

	public String getStoreComplexName() {
		return StoreComplexName;
	}

	public void setStoreComplexName(String storeComplexName) {
		StoreComplexName = storeComplexName;
	}

	public String getDescription() {
		return Description;
	}

	public void setDescription(String description) {
		Description = description;
	}

	public String getAlias() {
		return Alias;
	}

	public void setAlias(String alias) {
		Alias = alias;
	}

	public String getKeywords() {
		return Keywords;
	}

	public void setKeywords(String keywords) {
		Keywords = keywords;
	}

	public long getVisitCount() {
		return VisitCount;
	}

	public void setVisitCount(long visitCount) {
		VisitCount = visitCount;
	}

	public boolean isShowInMallPage() {
		return ShowInMallPage;
	}

	public void setShowInMallPage(boolean showInMallPage) {
		ShowInMallPage = showInMallPage;
	}

	public String getMetaTitle() {
		return MetaTitle;
	}

	public void setMetaTitle(String metaTitle) {
		MetaTitle = metaTitle;
	}

	public String getMetaKeywords() {
		return MetaKeywords;
	}

	public void setMetaKeywords(String metaKeywords) {
		MetaKeywords = metaKeywords;
	}

	public String getMetaDescription() {
		return MetaDescription;
	}

	public void setMetaDescription(String metaDescription) {
		MetaDescription = metaDescription;
	}

	private long StoreId;
    private long VendorId;
    private String StoreCode;
    private String StoreName;
    private String StoreComplexName;
    private String Description;
    private String Alias;
    private String Keywords;
    private long VisitCount;
    private boolean ShowInMallPage;
    private String MetaTitle;
    private String MetaKeywords;
    private String MetaDescription;
}
