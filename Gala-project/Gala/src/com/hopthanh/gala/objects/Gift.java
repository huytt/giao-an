package com.hopthanh.gala.objects;

import java.sql.Date;

public class Gift {
	
	private long GiftId;
    private long ProductId;
    private long BannerMediaId;
    private String GiftName;
    private String Description;
    private double GiftPrice;
    private Date DateCreated;
    private Date DateModified;
    private boolean IsDeleted;
    private Product Product;
    
	public long getGiftId() {
		return GiftId;
	}
	public void setGiftId(long giftId) {
		GiftId = giftId;
	}
	public long getProductId() {
		return ProductId;
	}
	public void setProductId(long productId) {
		ProductId = productId;
	}
	public long getBannerMediaId() {
		return BannerMediaId;
	}
	public void setBannerMediaId(long bannerMediaId) {
		BannerMediaId = bannerMediaId;
	}
	public String getGiftName() {
		return GiftName;
	}
	public void setGiftName(String giftName) {
		GiftName = giftName;
	}
	public String getDescription() {
		return Description;
	}
	public void setDescription(String description) {
		Description = description;
	}
	public double getGiftPrice() {
		return GiftPrice;
	}
	public void setGiftPrice(double giftPrice) {
		GiftPrice = giftPrice;
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
	public boolean isDeleted() {
		return IsDeleted;
	}
	public void setDeleted(boolean isDeleted) {
		IsDeleted = isDeleted;
	}
	public Product getProduct() {
		return Product;
	}
	public void setProduct(Product product) {
		Product = product;
	}
}
