package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class Category_MultiLang {
	private long Category_MultiLangId;
    private long CategoryId;
    private String LanguageCode;
    private String CategoryName;
    private String Description;
    private String Alias;
    private String MetaTitle;
    private String MetaKeywords;
    private String MetaDescription;
    
    public static Category_MultiLang parseJonData(String json) {
		if(json.equals("null")) {
			return null;
		}

    	Category_MultiLang result = new Category_MultiLang();
    	try {
			JSONObject jObject = new JSONObject(json);

			String temp = jObject.getString("Category_MultiLangId");
			if (!temp.equals("null")) {
				result.Category_MultiLangId = Long.parseLong(temp);
			}
			
			temp = jObject.getString("CategoryId");
			if (!temp.equals("null")) {
				result.CategoryId = Long.parseLong(temp);
			}

			result.LanguageCode = jObject.getString("LanguageCode");
		    result.CategoryName = jObject.getString("CategoryName");
		    result.Description = jObject.getString("Description");
		    result.Alias = jObject.getString("Alias");
		    result.MetaTitle = jObject.getString("MetaTitle");
		    result.MetaKeywords = jObject.getString("MetaKeywords");
		    result.MetaDescription = jObject.getString("MetaDescription");
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
    	return result;
    }
    
	public long getCategory_MultiLangId() {
		return Category_MultiLangId;
	}
	public void setCategory_MultiLangId(long category_MultiLangId) {
		Category_MultiLangId = category_MultiLangId;
	}
	public long getCategoryId() {
		return CategoryId;
	}
	public void setCategoryId(long categoryId) {
		CategoryId = categoryId;
	}
	public String getLanguageCode() {
		return LanguageCode;
	}
	public void setLanguageCode(String languageCode) {
		LanguageCode = languageCode;
	}
	public String getCategoryName() {
		return CategoryName;
	}
	public void setCategoryName(String categoryName) {
		CategoryName = categoryName;
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
}
