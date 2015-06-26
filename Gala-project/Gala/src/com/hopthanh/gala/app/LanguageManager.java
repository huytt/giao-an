package com.hopthanh.gala.app;

import java.util.Locale;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;

public class LanguageManager {
	private static LanguageManager instance;
//	private Context mContext;
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
	
	public void loadLocaleDefault(Context context)
    {
    	changeLang(context, LANG_DEFAULT);
    }
    
    public void saveLocale(Context context, String lang)
    {
    	String langPref = "Language";
    	SharedPreferences prefs = context.getSharedPreferences("CommonPrefs", Activity.MODE_PRIVATE);
    	SharedPreferences.Editor editor = prefs.edit();
		editor.putString(langPref, lang);
		editor.commit();
    }

	public void changeLang(Context context, String lang)
    {
    	if (lang.equalsIgnoreCase(""))
    		return;
    	myLocale = new Locale(lang);
    	saveLocale(context, lang);
        Locale.setDefault(myLocale);
        android.content.res.Configuration config = new android.content.res.Configuration();
        config.locale = myLocale;
        context.getResources().updateConfiguration(config, context.getResources().getDisplayMetrics());
        mCurrentLanguage = lang;
    }

	public String getCurrentLanguage() {
		return mCurrentLanguage;
	}

	public void setCurrentLanguage(String mCurrentLanguage) {
		this.mCurrentLanguage = mCurrentLanguage;
	}
}
