package com.hopthanh.gala.objects;

public class StoreInMedia {
    private long StoreInMediaId;
    private long StoreId;
    private long MediaId;
    
    private Media Media;
    private Store Store;
    
	public long getStoreInMediaId() {
		return StoreInMediaId;
	}
	public void setStoreInMediaId(long storeInMediaId) {
		StoreInMediaId = storeInMediaId;
	}
	public long getStoreId() {
		return StoreId;
	}
	public void setStoreId(long storeId) {
		StoreId = storeId;
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
	public Store getStore() {
		return Store;
	}
	public void setStore(Store store) {
		Store = store;
	}
}
