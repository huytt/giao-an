package com.hopthanh.gala.objects;

public class ProductInMedia {
    private long ProductInMediaId;
    private long ProductId;
    private long MediaId;
    
    private Media Media;
    private Product Product;
    
	public long getProductInMediaId() {
		return ProductInMediaId;
	}
	public void setProductInMediaId(long productInMediaId) {
		ProductInMediaId = productInMediaId;
	}
	public long getProductId() {
		return ProductId;
	}
	public void setProductId(long productId) {
		ProductId = productId;
	}
	public long getMediaId() {
		return MediaId;
	}
	public void setMediaId(long mediaId) {
		MediaId = mediaId;
	}
	public Media getMedia() {
		return Media;
	}
	public void setMedia(Media media) {
		Media = media;
	}
	public Product getProduct() {
		return Product;
	}
	public void setProduct(Product product) {
		Product = product;
	}
}
