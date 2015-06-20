package com.hopthanh.gala.web_api_util;

import java.io.IOException;

import android.content.Context;

import com.squareup.okhttp.Cache;
import com.squareup.picasso.OkHttpDownloader;
import com.squareup.picasso.Picasso;

public class PicassoUtil {
	private static PicassoUtil mInstance;
	private Picasso mPicasso = null;
	private CustomeOkHttpDownloader sDownloader = null;

	public static synchronized PicassoUtil getInstance(Context context) {
		if (mInstance == null) {
			mInstance = new PicassoUtil(context);
		}
		return mInstance;
	}

	private PicassoUtil(Context context) {
		sDownloader = new CustomeOkHttpDownloader(context);
		Picasso.Builder builder = new Picasso.Builder(context);
		builder.downloader(sDownloader);
		mPicasso = builder.build();
	}

	// public static Picasso getPicasso(Context context){
	// if(sInstance == null) {
	// sDownloader = new CustomeOkHttpDownloader(context);
	// Picasso.Builder builder = new Picasso.Builder(context);
	// builder.downloader(sDownloader);
	// sInstance = builder.build();
	// }
	// return sInstance;
	// }

	public void clearCache() {
		if (sDownloader != null) {
			sDownloader.clearCache();
		}
	}

	public Picasso getPicasso() {
		return mPicasso;
	}

//	public void setPicasso(Picasso mPicasso) {
//		this.mPicasso = mPicasso;
//	}
}

class CustomeOkHttpDownloader extends OkHttpDownloader {
	public CustomeOkHttpDownloader(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	public void clearCache() {
		Cache cache = getClient().getCache();
		if (cache != null) {
			try {
				cache.evictAll();
			} catch (IOException ignored) {
			}
		}
	}
}
