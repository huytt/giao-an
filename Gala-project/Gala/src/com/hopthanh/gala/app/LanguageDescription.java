package com.hopthanh.gala.app;

public class LanguageDescription {
	private String langCode;
	private int langNameId;
	private String imgUrl = "";
	private int iconResId = -1;
	
	public LanguageDescription(String langCode, int langName) {
		this.langCode = langCode;
		this.langNameId = langName;
	}

	public LanguageDescription(String langCode, int langName, String imgUrl) {
		this.langCode = langCode;
		this.langNameId = langName;
		this.imgUrl = imgUrl;
	}

	public LanguageDescription(String langCode, int langName, int iconRes) {
		this.langCode = langCode;
		this.langNameId = langName;
		this.iconResId = iconRes;
	}

	
	public String getLangCode() {
		return langCode;
	}
	public void setLangCode(String langCode) {
		this.langCode = langCode;
	}
	public String getImgUrl() {
		return imgUrl;
	}
	public void setImgUrl(String imgUrl) {
		this.imgUrl = imgUrl;
	}
	public int getIconResId() {
		return iconResId;
	}
	public void setIconResId(int iconResId) {
		this.iconResId = iconResId;
	}

	public int getLangNameId() {
		return langNameId;
	}

	public void setLangNameId(int langNameId) {
		this.langNameId = langNameId;
	}

	
}
