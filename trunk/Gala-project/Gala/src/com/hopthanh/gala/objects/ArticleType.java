package com.hopthanh.gala.objects;

import java.util.HashSet;

import org.json.JSONException;
import org.json.JSONObject;

public class ArticleType {
	public ArticleType()
    {
		mArticles = new HashSet<Article>();
    }

	public static ArticleType parseJonData(String json) {
		if(json.equals("null")) {
			return null;
		}

		ArticleType result = new ArticleType();
    	try {
			JSONObject jObject = new JSONObject(json);

			String temp = jObject.getString("ArticleTypeId");
			if (!temp.equals("null")) {
				result.ArticleTypeId = Long.parseLong(temp);
			}

			result.ArticleTypeName = jObject.getString("ArticleTypeName");
			result.LanguageCode = jObject.getString("LanguageCode");
			
			temp = jObject.getString("Code");
			if (!temp.equals("null")) {
				result.Code = Long.parseLong(temp);
			}
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
    	return result;
    }
	
    public long getArticleTypeId() {
		return ArticleTypeId;
	}
	public void setArticleTypeId(long articleTypeId) {
		ArticleTypeId = articleTypeId;
	}

	public String getArticleTypeName() {
		return ArticleTypeName;
	}

	public void setArticleTypeName(String articleTypeName) {
		ArticleTypeName = articleTypeName;
	}

	public String getLanguageCode() {
		return LanguageCode;
	}

	public void setLanguageCode(String languageCode) {
		LanguageCode = languageCode;
	}

	public long getCode() {
		return Code;
	}

	public void setCode(long code) {
		Code = code;
	}

	public HashSet<Article> getArticles() {
		return mArticles;
	}

	public void setArticle(HashSet<Article> mArticle) {
		this.mArticles = mArticle;
	}

	private long ArticleTypeId;
    private String ArticleTypeName;
    private String LanguageCode;
    private long Code;
    private HashSet<Article> mArticles;
}
