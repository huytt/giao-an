package com.hopthanh.gala.objects;

import java.util.ArrayList;

public class FooterDataClass {
	private ArrayList<Article> mArticle;
	private ArrayList<ArticleType> mArticleType;
	
	public FooterDataClass () {
		this.mArticle = new ArrayList<Article>();
		this.mArticleType = new ArrayList<ArticleType>();
	}
	
	public ArrayList<Article> getArticle() {
		return mArticle;
	}
	public void setArticle(ArrayList<Article> mArticle) {
		this.mArticle = mArticle;
	}
	public ArrayList<ArticleType> getArticleType() {
		return mArticleType;
	}
	public void setArticleType(ArrayList<ArticleType> mArticleType) {
		this.mArticleType = mArticleType;
	}
}
