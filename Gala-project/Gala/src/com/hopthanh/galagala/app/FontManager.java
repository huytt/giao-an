package com.hopthanh.galagala.app;

import android.content.res.AssetManager;
import android.graphics.Typeface;
import android.widget.TextView;

public class FontManager {
	private static FontManager instance;
	
	public static final String FONT_HELVETICANEUE = "fonts/helvaticaneue.ttf";

	private FontManager()
	{
	}

	public static synchronized FontManager getInstance()
	{
		if (instance == null) {
			instance = new FontManager();
		}
		return instance;
	}
	
	public Typeface getCustomFont(AssetManager assets, String fontName) {
		return Typeface.createFromAsset(assets, fontName);
	}
	
	public void customFontTextView(TextView tv, AssetManager assets, String fontName) {
		tv.setTypeface(Typeface.createFromAsset(assets, fontName));
	}
}
