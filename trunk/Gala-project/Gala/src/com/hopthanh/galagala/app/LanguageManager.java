package com.hopthanh.galagala.app;

import java.util.Locale;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;

public class LanguageManager {
	private static LanguageManager instance;
	private Context mContext = null;
	private SharedPreferences mSharedPreferences = null;
	private Locale myLocale;
	
	public static final String LANG_PREF = "Language";
	public static final String LANG_DEFAULT = "vi";
	public static final String LANG_VIETNAM = "vi";
	public static final String LANG_ENGLAND = "en";
	public static final String LANG_CHINA = "ci";
	
	public static final LanguageDescription[] LANG_SUPPORTS = {
		new LanguageDescription(LANG_VIETNAM, R.string.langVietNam, R.drawable.icon_left_menu_lang_vietnam), 
		new LanguageDescription(LANG_ENGLAND, R.string.langEngland, R.drawable.icon_left_menu_lang_england),  
		new LanguageDescription(LANG_CHINA, R.string.langChina, R.drawable.icon_left_menu_lang_china)
	};
	
	private LanguageManager()
	{
//		mContext = context;
		myLocale = new Locale(LANG_DEFAULT);
	}

	public static synchronized LanguageManager getInstance()
	{
		if (instance == null) {
			instance = new LanguageManager();
		}
		return instance;
	}
	
	public void init(Context context) {
		mContext = context;
		mSharedPreferences = mContext.getSharedPreferences("CommonPrefs", Activity.MODE_PRIVATE);
	}
	
	public void loadLocaleDefault()
    {
    	changeLang(LANG_DEFAULT);
    }
    
    public void saveLocale(String lang)
    {
    	SharedPreferences.Editor editor = mSharedPreferences.edit();
		editor.putString(LANG_PREF, lang);
		editor.commit();
    }

	public void changeLang(String lang)
    {
    	if (lang.equalsIgnoreCase("")) {
    		loadLocaleDefault();
    		return;
    	}

    	myLocale = new Locale(lang);
    	saveLocale(lang);
        Locale.setDefault(myLocale);
        android.content.res.Configuration config = new android.content.res.Configuration();
        config.locale = myLocale;
        mContext.getResources().updateConfiguration(config, mContext.getResources().getDisplayMetrics());
    }

	public String getCurrentLanguage() {
		return mSharedPreferences.getString(LANG_PREF, LANG_DEFAULT);
	}

	public String getCurLangName() {
		return convertLangCodeToName(getCurrentLanguage());
	}
	
	public String convertLangCodeToName(String langCode) {
		if(langCode.equals(LANG_ENGLAND)) {
			return "English";
		} else if(langCode.equals(LANG_CHINA)) {
			return "Chinese";
		}
		return "VietNamese";
	}
}
