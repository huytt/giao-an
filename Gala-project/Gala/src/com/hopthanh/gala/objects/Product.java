package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

import org.json.JSONException;
import org.json.JSONObject;

public class Product {
	public Product()
    {
        this.setGift(new HashSet<Gift>());
        this.setProductInMedia(new HashSet<ProductInMedia>());
        this.setProductInCategory(new HashSet<ProductInCategory>());
    }

	public static Product parseJonData(String json) {
		Product result = new Product();
    	try {
			JSONObject jObject = new JSONObject(json);

			String temp = jObject.getString("ProductId");
			if (!temp.equals("null")) {
				result.ProductId = Long.parseLong(temp);
			}

			result.ProductCode = jObject.getString("ProductCode");
			
			temp = jObject.getString("StoreId");
			if (!temp.equals("null")) {
				result.StoreId = Long.parseLong(temp);
			}
			
			temp = jObject.getString("BrandId");
			if (!temp.equals("null")) {
				result.BrandId = Long.parseLong(temp);
			}
			
		    result.ProductStatusCode = jObject.getString("ProductStatusCode");
		    result.ProductTypeCode = jObject.getString("ProductTypeCode");
		    result.ProductStockCode = jObject.getString("ProductStockCode");
		    result.ProductName = jObject.getString("ProductName");
		    result.ProductComplexName = jObject.getString("ProductComplexName");
		    result.Alias = jObject.getString("Alias");
		    result.Keywords = jObject.getString("Keywords");

		    temp = jObject.getString("RetailPrice");
			if (!temp.equals("null")) {
				result.RetailPrice = Double.parseDouble(temp);
			}
			
			temp = jObject.getString("PromotePrice");
			if (!temp.equals("null")) {
				result.PromotePrice = Double.parseDouble(temp);
			}
			
			temp = jObject.getString("MobileOnlinePrice");
			if (!temp.equals("null")) {
				result.MobileOnlinePrice = Double.parseDouble(temp);
			}
			
		    result.ProductOutLine = jObject.getString("ProductOutLine");
		    result.ProductSpecification = jObject.getString("ProductSpecification");
		    result.ProductDetail = jObject.getString("ProductDetail");
		    result.ProductTermService = jObject.getString("ProductTermService");

		    temp = jObject.getString("VisitCount");
			if (!temp.equals("null")) {
				result.VisitCount = Long.parseLong(temp);
			}
			
			temp = jObject.getString("ShowInStorePage");
			if (!temp.equals("null")) {
				result.ShowInStorePage = Boolean.parseBoolean(temp);
			}

		    result.MetaTitle = jObject.getString("MetaTitle");
		    result.MetaKeywords = jObject.getString("MetaKeywords");
		    result.MetaDescription = jObject.getString("MetaDescription");
		    
		    result.mStore = Store.parseJonData(jObject.getString("Store"));
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
    	return result;
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

	public Brand getBrand() {
		return mBrand;
	}

	public void setBrand(Brand brand) {
		mBrand = brand;
	}

	public HashSet<Gift> getGift() {
		return mGift;
	}

	public void setGift(HashSet<Gift> gift) {
		mGift = gift;
	}

	public Store getStore() {
		return mStore;
	}

	public void setStore(Store store) {
		mStore = store;
	}

	public HashSet<ProductInMedia> getProductInMedia() {
		return mProductInMedia;
	}

	public void setProductInMedia(HashSet<ProductInMedia> productInMedia) {
		mProductInMedia = productInMedia;
	}

	public HashSet<ProductInCategory> getProductInCategory() {
		return mProductInCategory;
	}

	public void setProductInCategory(HashSet<ProductInCategory> productInCategory) {
		mProductInCategory = productInCategory;
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

    private  Brand mBrand;
    private  HashSet<Gift> mGift;
    private  Store mStore;
    private  HashSet<ProductInMedia> mProductInMedia;
    private  HashSet<ProductInCategory> mProductInCategory;
}
