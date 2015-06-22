package com.hopthanh.gala.objects;

import java.util.ArrayList;

import org.javatuples.Quartet;
import org.javatuples.Triplet;

public class HomePageDataClass {
	public HomePageDataClass() {
		mStore = new ArrayList<ArrayList<StoreInMedia>>();
		mBrand = new ArrayList<Triplet<Brand, Media, Media>>();
		mMall = new ArrayList<Media>();
		mProductHot = new ArrayList<ProductInMedia>();
		mProductBuy = new ArrayList<ProductInMedia>();
		mCategory = new ArrayList<Quartet<Category,Media,Media,Category_MultiLang>>();
		mFooterData = new FooterDataClass();
	}
	
	public ArrayList<ArrayList<StoreInMedia>> getStore() {
		return mStore;
	}
	public void setStore(ArrayList<ArrayList<StoreInMedia>> mStore) {
		this.mStore = mStore;
	}

	public ArrayList<Triplet<Brand, Media, Media>> getBrand() {
		return mBrand;
	}

	public void setBrand(ArrayList<Triplet<Brand, Media, Media>> mBrand) {
		this.mBrand = mBrand;
	}

	public ArrayList<Media> getMall() {
		return mMall;
	}

	public void setMall(ArrayList<Media> mMall) {
		this.mMall = mMall;
	}

	public ArrayList<ProductInMedia> getProductHot() {
		return mProductHot;
	}

	public void setProductHot(ArrayList<ProductInMedia> mProductHot) {
		this.mProductHot = mProductHot;
	}

	public ArrayList<ProductInMedia> getProductBuy() {
		return mProductBuy;
	}

	public void setProductBuy(ArrayList<ProductInMedia> mProductBuy) {
		this.mProductBuy = mProductBuy;
	}

	public ArrayList<Quartet<Category, Media, Media, Category_MultiLang>> getCategory() {
		return mCategory;
	}

	public void setCategory(ArrayList<Quartet<Category, Media, Media, Category_MultiLang>> mCategory) {
		this.mCategory = mCategory;
	}

	public FooterDataClass getFooterData() {
		return mFooterData;
	}

	public void setFooterData(FooterDataClass mFooterData) {
		this.mFooterData = mFooterData;
	}

	private ArrayList<ArrayList<StoreInMedia>> mStore;
	private ArrayList<Triplet<Brand, Media, Media>> mBrand;
	private ArrayList<Media> mMall;
	private ArrayList<ProductInMedia> mProductHot;
	private ArrayList<ProductInMedia> mProductBuy;
	private ArrayList<Quartet<Category, Media, Media, Category_MultiLang>> mCategory;
	private FooterDataClass mFooterData;
}
