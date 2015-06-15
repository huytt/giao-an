package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

public class Product {
	public Product()
    {
        this.setGift(new HashSet<Gift>());
        this.setProductInMedia(new HashSet<ProductInMedia>());
        this.setProductInCategory(new HashSet<ProductInCategory>());
    }

    public long getProductId() {
		return ProductId;
	}
	public void setProductId(long productId) {
		ProductId = productId;
	}

	public String getProductCode() {
		return ProductCode;
	}

	public void setProductCode(String productCode) {
		ProductCode = productCode;
	}

	public long getStoreId() {
		return StoreId;
	}

	public void setStoreId(long storeId) {
		StoreId = storeId;
	}

	public long getBrandId() {
		return BrandId;
	}

	public void setBrandId(long brandId) {
		BrandId = brandId;
	}

	public String getProductStatusCode() {
		return ProductStatusCode;
	}

	public void setProductStatusCode(String productStatusCode) {
		ProductStatusCode = productStatusCode;
	}

	public String getProductTypeCode() {
		return ProductTypeCode;
	}

	public void setProductTypeCode(String productTypeCode) {
		ProductTypeCode = productTypeCode;
	}

	public String getProductStockCode() {
		return ProductStockCode;
	}

	public void setProductStockCode(String productStockCode) {
		ProductStockCode = productStockCode;
	}

	public String getProductName() {
		return ProductName;
	}

	public void setProductName(String productName) {
		ProductName = productName;
	}

	public String getProductComplexName() {
		return ProductComplexName;
	}

	public void setProductComplexName(String productComplexName) {
		ProductComplexName = productComplexName;
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

	public double getRetailPrice() {
		return RetailPrice;
	}

	public void setRetailPrice(double retailPrice) {
		RetailPrice = retailPrice;
	}

	public double getPromotePrice() {
		return PromotePrice;
	}

	public void setPromotePrice(double promotePrice) {
		PromotePrice = promotePrice;
	}

	public double getMobileOnlinePrice() {
		return MobileOnlinePrice;
	}

	public void setMobileOnlinePrice(double mobileOnlinePrice) {
		MobileOnlinePrice = mobileOnlinePrice;
	}

	public String getProductOutLine() {
		return ProductOutLine;
	}

	public void setProductOutLine(String productOutLine) {
		ProductOutLine = productOutLine;
	}

	public String getProductSpecification() {
		return ProductSpecification;
	}

	public void setProductSpecification(String productSpecification) {
		ProductSpecification = productSpecification;
	}

	public String getProductDetail() {
		return ProductDetail;
	}

	public void setProductDetail(String productDetail) {
		ProductDetail = productDetail;
	}

	public String getProductTermService() {
		return ProductTermService;
	}

	public void setProductTermService(String productTermService) {
		ProductTermService = productTermService;
	}

	public long getVisitCount() {
		return VisitCount;
	}

	public void setVisitCount(long visitCount) {
		VisitCount = visitCount;
	}

	public boolean isShowInStorePage() {
		return ShowInStorePage;
	}

	public void setShowInStorePage(boolean showInStorePage) {
		ShowInStorePage = showInStorePage;
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

	public Date getDateVerified() {
		return DateVerified;
	}

	public void setDateVerified(Date dateVerified) {
		DateVerified = dateVerified;
	}

	public long getCreatedBy() {
		return CreatedBy;
	}

	public void setCreatedBy(long createdBy) {
		CreatedBy = createdBy;
	}

	public long getModifiedBy() {
		return ModifiedBy;
	}

	public void setModifiedBy(long modifiedBy) {
		ModifiedBy = modifiedBy;
	}

	public boolean isVerified() {
		return IsVerified;
	}

	public void setVerified(boolean isVerified) {
		IsVerified = isVerified;
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

	public Brand getBrand() {
		return Brand;
	}

	public void setBrand(Brand brand) {
		Brand = brand;
	}

	public HashSet<Gift> getGift() {
		return Gift;
	}

	public void setGift(HashSet<Gift> gift) {
		Gift = gift;
	}

	public Store_fake getStore() {
		return Store;
	}

	public void setStore(Store_fake store) {
		Store = store;
	}

	public HashSet<ProductInMedia> getProductInMedia() {
		return ProductInMedia;
	}

	public void setProductInMedia(HashSet<ProductInMedia> productInMedia) {
		ProductInMedia = productInMedia;
	}

	public HashSet<ProductInCategory> getProductInCategory() {
		return ProductInCategory;
	}

	public void setProductInCategory(HashSet<ProductInCategory> productInCategory) {
		ProductInCategory = productInCategory;
	}

	private long ProductId;
    private String ProductCode;
    private long StoreId;
    private long BrandId;
    private String ProductStatusCode;
    private String ProductTypeCode;
    private String ProductStockCode;
    private String ProductName;
    private String ProductComplexName;
    private String Alias;
    private String Keywords;
    private double RetailPrice;
    private double PromotePrice;
    private double MobileOnlinePrice;
    private String ProductOutLine;
    private String ProductSpecification;
    private String ProductDetail;
    private String ProductTermService;
    private long VisitCount;
    private boolean ShowInStorePage;
    private String MetaTitle;
    private String MetaKeywords;
    private String MetaDescription;
    private Date DateCreated;
    private Date DateModified;
    private Date DateVerified;
    private long CreatedBy;
    private long ModifiedBy;
    private boolean IsVerified;
    private boolean IsActive;
    private boolean IsDeleted;

    private  Brand Brand;
    private  HashSet<Gift> Gift;
    private  Store_fake Store;
    private  HashSet<ProductInMedia> ProductInMedia;
    private  HashSet<ProductInCategory> ProductInCategory;
}
