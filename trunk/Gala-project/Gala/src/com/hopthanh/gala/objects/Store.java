package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

public class Store {
	public Store()
    {
        this.setProduct(new HashSet<Product>());
        this.setStoreInMedia(new HashSet<StoreInMedia>());
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

	public Date getOnlineDate() {
		return OnlineDate;
	}

	public void setOnlineDate(Date onlineDate) {
		OnlineDate = onlineDate;
	}

	public Date getOfflineDate() {
		return OfflineDate;
	}

	public void setOfflineDate(Date offlineDate) {
		OfflineDate = offlineDate;
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

	public HashSet<Product> getProduct() {
		return Product;
	}

	public void setProduct(HashSet<Product> product) {
		Product = product;
	}

	public HashSet<StoreInMedia> getStoreInMedia() {
		return StoreInMedia;
	}

	public void setStoreInMedia(HashSet<StoreInMedia> storeInMedia) {
		StoreInMedia = storeInMedia;
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
    private Date OnlineDate;
    private Date OfflineDate;
    private boolean ShowInMallPage;
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
    
    private HashSet<Product> Product;
    private HashSet<StoreInMedia> StoreInMedia;
}
