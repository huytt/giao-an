package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

public class Brand {
	public Brand()
    {
        this.setProduct(new HashSet<Product>());
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

	public HashSet<Product> getProduct() {
		return Product;
	}

	public void setProduct(HashSet<Product> product) {
		Product = product;
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
    private Date DateCreated;
    private Date DateModified;
    private long CreatedBy;
    private boolean IsActive;
    private boolean IsDeleted;
    private HashSet<Product> Product;
}
