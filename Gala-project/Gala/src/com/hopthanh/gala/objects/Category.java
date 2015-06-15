package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

public class Category {
    public Category()
    {
        this.setProductInCategory(new HashSet<ProductInCategory>());
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

	public long getCreatedBy() {
		return CreatedBy;
	}

	public void setCreatedBy(long createdBy) {
		CreatedBy = createdBy;
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

	public HashSet<ProductInCategory> getProductInCategory() {
		return ProductInCategory;
	}

	public void setProductInCategory(HashSet<ProductInCategory> productInCategory) {
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
    private Date DateCreated;
    private Date DateModified;
    private long CreatedBy;
    private boolean IsActive;
    private boolean IsDeleted;
    
    private HashSet<ProductInCategory> ProductInCategory;
}
