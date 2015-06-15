package com.hopthanh.gala.objects;

public class ProductInCategory {
    private long ProductInCategoryId;
    private long ProductId;
    private long CategoryId;
    private boolean IsActive;
    private boolean IsDeleted;
    
    private Category Category;
    private Product Product;
    
	public long getProductInCategoryId() {
		return ProductInCategoryId;
	}
	public void setProductInCategoryId(long productInCategoryId) {
		ProductInCategoryId = productInCategoryId;
	}
	public long getProductId() {
		return ProductId;
	}
	public void setProductId(long productId) {
		ProductId = productId;
	}
	public long getCategoryId() {
		return CategoryId;
	}
	public void setCategoryId(long categoryId) {
		CategoryId = categoryId;
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
	public Category getCategory() {
		return Category;
	}
	public void setCategory(Category category) {
		Category = category;
	}
	public Product getProduct() {
		return Product;
	}
	public void setProduct(Product product) {
		Product = product;
	}
}
