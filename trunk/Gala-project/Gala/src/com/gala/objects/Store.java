package com.gala.objects;

import java.util.ArrayList;
import java.util.Date;

public class Store {
	private String strBanner;
	private boolean hasMediaBanner;
	private ArrayList<String> productsImgPaths;
	private Date timeOpen;
	
	public Store () {
		
	}
	
	public Store (String banner, boolean hasBanner, ArrayList<String> imgPaths, Date time) {
		strBanner = banner;
		hasMediaBanner = hasBanner;
		productsImgPaths = imgPaths;
		timeOpen = time;
	}
	
	public String getStrBanner() {
		return strBanner;
	}
	public void setStrBanner(String strBanner) {
		this.strBanner = strBanner;
	}
	public boolean hasMediaBanner() {
		return hasMediaBanner;
	}
	public void setHasMediaBanner(boolean hasMediaBanner) {
		this.hasMediaBanner = hasMediaBanner;
	}
	public ArrayList<String> getProductsImgPaths() {
		return productsImgPaths;
	}
	public void setProductsImgPaths(ArrayList<String> productsImgPaths) {
		this.productsImgPaths = productsImgPaths;
	}
	public Date getTimeOpen() {
		return timeOpen;
	}
	public void setTimeOpen(Date timeOpen) {
		this.timeOpen = timeOpen;
	}

}
