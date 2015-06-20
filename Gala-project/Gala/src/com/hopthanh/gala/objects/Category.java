package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

import org.json.JSONException;
import org.json.JSONObject;

public class Category {
	public Category() {
		this.setProductInCategory(new HashSet<ProductInCategory>());
	}

	public static Category parseJonData(String json) {
		Category result = new Category();
		try {
			JSONObject jObject = new JSONObject(json);

			String temp = jObject.getString("CategoryId");
			if (!temp.equals("null")) {
				result.CategoryId = Long.parseLong(temp);
			}

			temp = jObject.getString("LogoMediaId");
			if (!temp.equals("null")) {
				result.LogoMediaId = Long.parseLong(temp);
			}

			temp = jObject.getString("BannerMediaId");
			if (!temp.equals("null")) {
				result.BannerMediaId = Long.parseLong(temp);
			}

			temp = jObject.getString("CateLevel");
			if (!temp.equals("null")) {
				result.CateLevel = Integer.parseInt(temp);
			}

			temp = jObject.getString("OrderNumber");
			if (!temp.equals("null")) {
				result.OrderNumber = Integer.parseInt(temp);
			}
			result.CategoryName = jObject.getString("CategoryName");
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

	public long getCategoryId() {
		return CategoryId;
	}

	public void setCategoryId(long categoryId) {
		CategoryId = categoryId;
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

	public long getParentCateId() {
		return ParentCateId;
	}

	public void setParentCateId(long parentCateId) {
		ParentCateId = parentCateId;
	}

	public int getCateLevel() {
		return CateLevel;
	}

	public void setCateLevel(int cateLevel) {
		CateLevel = cateLevel;
	}

	public int getOrderNumber() {
		return OrderNumber;
	}

	public void setOrderNumber(int orderNumber) {
		OrderNumber = orderNumber;
	}

	public String getCategoryName() {
		return CategoryName;
	}

	public void setCategoryName(String categoryName) {
		CategoryName = categoryName;
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

	public HashSet<ProductInCategory> getProductInCategory() {
		return ProductInCategory;
	}

	public void setProductInCategory(
			HashSet<ProductInCategory> productInCategory) {
		ProductInCategory = productInCategory;
	}

	private long CategoryId;
	private long LogoMediaId;
	private long BannerMediaId;
	private long ParentCateId;
	private int CateLevel;
	private int OrderNumber;
	private String CategoryName;
	private String Alias;
	private String Description;
	private long VisitCount;
	private String MetaTitle;
	private String MetaKeywords;
	private String MetaDescription;

	private HashSet<ProductInCategory> ProductInCategory;
}
