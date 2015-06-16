package com.hopthanh.gala.objects;

import org.json.JSONException;
import org.json.JSONObject;

public class ProductInMedia {
    private long ProductInMediaId;
    private long ProductId;
    private long MediaId;
    
    private Media mMedia;
    private Product mProduct;
    
    public static ProductInMedia parseJonData(String json) {
    	ProductInMedia result = new ProductInMedia();
    	try {
			JSONObject jObject = new JSONObject(json);
			String temp = jObject.getString("ProductInMediaId");
			if (!temp.equals("null")) {
				result.ProductInMediaId = Long.parseLong(temp);
			}
			
			temp = jObject.getString("ProductId");
			if (!temp.equals("null")) {
				result.ProductId = Long.parseLong(temp);
			}

			temp = jObject.getString("MediaId");
			if (!temp.equals("null")) {
				result.MediaId = Long.parseLong(temp);
			}
			
		    result.mMedia = Media.parseJonData(jObject.getString("Media"));
		    result.mProduct = Product.parseJonData(jObject.getString("Product"));
		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    	
    	return result;
    }
    
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
		return mMedia;
	}
	public void setMedia(Media media) {
		mMedia = media;
	}
	public Product getProduct() {
		return mProduct;
	}
	public void setProduct(Product product) {
		mProduct = product;
	}
}
