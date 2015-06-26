package com.hopthanh.gala.app;

import java.util.Locale;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;

public class LanguageManager {
	private static LanguageManager instance;
	private Context mContext;
	private Locale myLocale;
	private String mCurrentLanguage = LANG_DEFAULT;
	public static final String LANG_DEFAULT = "vi";
	public static final String LANG_VIETNAM = "vi";
	public static final String LANG_ENGLAND = "en";
	public static final String LANG_CHINA = "ci";
	
	public static final LanguageDescription[] LANG_SUPPORTS = {
		new LanguageDescription(LANG_VIETNAM, R.string.langVietNam, R.drawable.icon_left_menu_lang_vietnam), 
		new LanguageDescription(LANG_ENGLAND, R.string.langEngland, R.drawable.icon_left_menu_lang_england),  
		new LanguageDescription(LANG_CHINA, R.string.langChina)
	};
	
	private LanguageManager(Context context)
	{
		mContext = context;
	}

	public static synchronized LanguageManager getInstance(Context context)
	{
		if (instance == null) {
			instance = new LanguageManager(context);
		}
		return instance;
	}
	
	public void loadLocaleDefault()
    {
    	changeLang(LANG_DEFAULT);
    }
    
    public void saveLocale(String lang)
    {
    	String langPref = "Language";
    	SharedPreferences prefs = mContext.getSharedPreferences("CommonPrefs", Activity.MODE_PRIVATE);
    	SharedPreferences.Editor editor = prefs.edit();
		editor.putString(langPref, lang);
		editor.commit();
    }

	public void changeLang(String lang)
    {
    	if (lang.equalsIgnoreCase(""))
    		return;
    	myLocale = new Locale(lang);
    	saveLocale(lang);
        Locale.setDefault(myLocale);
        android.content.res.Configuration config = new android.content.res.Configuration();
        config.locale = myLocale;
        mContext.getResources().updateConfiguration(config, mContext.getResources().getDisplayMetrics());
        mCurrentLanguage = lang;
    }

	public String getCurrentLanguage() {
		return mCurrentLanguage;
	}

	public void setCurrentLanguage(String mCurrentLanguage) {
		this.mCurrentLanguage = mCurrentLanguage;
	}
}
