package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

public class Media {
    public Media()
    {
        this.setProductInMedia(new HashSet<ProductInMedia>());
        this.setStoreInMedia(new HashSet<StoreInMedia>());
    }

    public long getMediaId() {
		return MediaId;
	}
	public void setMediaId(long mediaId) {
		MediaId = mediaId;
	}

	public long getMediaTypeId() {
		return MediaTypeId;
	}

	public void setMediaTypeId(long mediaTypeId) {
		MediaTypeId = mediaTypeId;
	}

	public String getMediaName() {
		return MediaName;
	}

	public void setMediaName(String mediaName) {
		MediaName = mediaName;
	}

	public String getUrl() {
		return Url;
	}

	public void setUrl(String url) {
		Url = url;
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

	public MediaType getMediaType() {
		return MediaType;
	}

	public void setMediaType(MediaType mediaType) {
		MediaType = mediaType;
	}

	public HashSet<ProductInMedia> getProductInMedia() {
		return ProductInMedia;
	}

	public void setProductInMedia(HashSet<ProductInMedia> productInMedia) {
		ProductInMedia = productInMedia;
	}

	public HashSet<StoreInMedia> getStoreInMedia() {
		return StoreInMedia;
	}

	public void setStoreInMedia(HashSet<StoreInMedia> storeInMedia) {
		StoreInMedia = storeInMedia;
	}

	private long MediaId;
    private long MediaTypeId;
    private String MediaName;
    private String Url;
    private Date DateCreated;
    private Date DateModified;
    private boolean IsActive;
    private boolean IsDeleted;
    
    private MediaType MediaType;
    private HashSet<ProductInMedia> ProductInMedia;
    private HashSet<StoreInMedia> StoreInMedia;
}
