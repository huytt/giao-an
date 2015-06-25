package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class Brand {
	public static Brand parseJonData(String json) {
		if(json.equals("null")) {
			return null;
		}

		Brand result = new Brand();
    	try {
			JSONObject jObject = new JSONObject(json);

			String temp = jObject.getString("BrandId");
			if (!temp.equals("null")) {
				result.BrandId = Long.parseLong(temp);
			}

			temp = jObject.getString("LogoMediaId");
			if (!temp.equals("null")) {
				result.LogoMediaId = Long.parseLong(temp);
			}
			
			temp = jObject.getString("BannerMediaId");
			if (!temp.equals("null")) {
				result.BannerMediaId = Long.parseLong(temp);
			}
			
		    result.BrandName = jObject.getString("BrandName");
		    result.Alias = jObject.getString("Alias");
		    result.Description = jObject.getString("Description");
		    
		    temp = jObject.getString("VisitCount");
			if (!temp.equals("null")) {
				result.VisitCount = Long.parseLong(temp);
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
	
    public long getBrandId() {
		return BrandId;
	}
	public void setBrandId(long brandId) {
		BrandId = brandId;
	}

	public long getLogoMediaId() {
		return LogoMediaId;
	}

	public void setLogoMediaId(long logoMediaId) {
		LogoMediaId = logoMediaId;
	}

	public long getBannerMediaId() {
		return BannerMediaId;
	}

	public void setBannerMediaId(long bannerMediaId) {
		BannerMediaId = bannerMediaId;
	}

	public String getBrandName() {
		return BrandName;
	}

	public void setBrandName(String brandName) {
		BrandName = brandName;
	}

	public String getAlias() {
		return Alias;
	}

	public void setAlias(String alias) {
		Alias = alias;
	}

	public String getDescription() {
		return Description;
	}

	public void setDescription(String description) {
		Description = description;
	}

	public long getVisitCount() {
		return VisitCount;
	}

	public void setVisitCount(long visitCount) {
		VisitCount = visitCount;
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

	private long BrandId;
    private long LogoMediaId;
    private long BannerMediaId;
    private String BrandName;
    private String Alias;
    private String Description;
    private long VisitCount;
    private String MetaTitle;
    private String MetaKeywords;
    private String MetaDescription;
}
