package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class Article {
	private long ArticleId;
    private long ArticleTypeId;
    private String Title;
    private String Introduction;
    private String ArticleContent;
    private long Code;
    private String LanguageCode;
    private String MetaTitle;
    private String MetaKeywords;
    private String MetaDescription;
    
    public static Article parseJonData(String json) {
    	Article result = new Article();
    	try {
			JSONObject jObject = new JSONObject(json);

			String temp = jObject.getString("ArticleId");
			if (!temp.equals("null")) {
				result.ArticleId = Long.parseLong(temp);
			}
			
			temp = jObject.getString("ArticleTypeId");
			if (!temp.equals("null")) {
				result.ArticleTypeId = Long.parseLong(temp);
			}
			
		    result.Title = jObject.getString("Title");
		    result.Introduction = jObject.getString("Introduction");
		    result.ArticleContent = jObject.getString("ArticleContent");
		    
			temp = jObject.getString("Code");
			if (!temp.equals("null")) {
				result.Code = Long.parseLong(temp);
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
    
	public long getArticleId() {
		return ArticleId;
	}
	public void setArticleId(long articleId) {
		ArticleId = articleId;
	}
	public long getArticleTypeId() {
		return ArticleTypeId;
	}
	public void setArticleTypeId(long articleTypeId) {
		ArticleTypeId = articleTypeId;
	}
	public String getTitle() {
		return Title;
	}
	public void setTitle(String title) {
		Title = title;
	}
	public String getIntroduction() {
		return Introduction;
	}
	public void setIntroduction(String introduction) {
		Introduction = introduction;
	}
	public String getArticleContent() {
		return ArticleContent;
	}
	public void setArticleContent(String articleContent) {
		ArticleContent = articleContent;
	}
	public long getCode() {
		return Code;
	}
	public void setCode(long code) {
		Code = code;
	}
	public String getLanguageCode() {
		return LanguageCode;
	}
	public void setLanguageCode(String languageCode) {
		LanguageCode = languageCode;
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
