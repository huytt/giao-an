package com.hopthanh.gala.objects;

import java.sql.Date;
import java.util.HashSet;

public class MediaType {
    public MediaType()
    {
        this.setMedia(new HashSet<Media>());
    }

    public long getMediaTypeId() {
		return MediaTypeId;
	}

	public void setMediaTypeId(long mediaTypeId) {
		MediaTypeId = mediaTypeId;
	}

	public long getParentID() {
		return ParentID;
	}

	public void setParentID(long parentID) {
		ParentID = parentID;
	}

	public int getFrameNumb() {
		return FrameNumb;
	}

	public void setFrameNumb(int frameNumb) {
		FrameNumb = frameNumb;
	}

	public String getMediaTypeName() {
		return MediaTypeName;
	}

	public void setMediaTypeName(String mediaTypeName) {
		MediaTypeName = mediaTypeName;
	}

	public String getMediaTypeCode() {
		return MediaTypeCode;
	}

	public void setMediaTypeCode(String mediaTypeCode) {
		MediaTypeCode = mediaTypeCode;
	}

	public String getFolderPath() {
		return FolderPath;
	}

	public void setFolderPath(String folderPath) {
		FolderPath = folderPath;
	}

	public int getRWidth() {
		return RWidth;
	}

	public void setRWidth(int rWidth) {
		RWidth = rWidth;
	}

	public int getRHeight() {
		return RHeight;
	}

	public void setRHeight(int rHeight) {
		RHeight = rHeight;
	}

	public long getMinSize() {
		return MinSize;
	}

	public void setMinSize(long minSize) {
		MinSize = minSize;
	}

	public long getMaxSize() {
		return MaxSize;
	}

	public void setMaxSize(long maxSize) {
		MaxSize = maxSize;
	}

	public String getDescription() {
		return Description;
	}

	public void setDescription(String description) {
		Description = description;
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

	public HashSet<Media> getMedia() {
		return Media;
	}

	public void setMedia(HashSet<Media> media) {
		Media = media;
	}

	private long MediaTypeId;
    private long ParentID;
    private int FrameNumb;
    private String MediaTypeName;
    private String MediaTypeCode;
    private String FolderPath;
    private int RWidth;
    private int RHeight;
    private long MinSize;
    private long MaxSize;
    private String Description;
    private Date DateCreated;
    private Date DateModified;
    private boolean IsActive;
    private boolean IsDeleted;
    
    private HashSet<Media> Media;
}
