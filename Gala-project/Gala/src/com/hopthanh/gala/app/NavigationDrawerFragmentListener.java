package com.hopthanh.gala.app;

public interface NavigationDrawerFragmentListener {
	void notifyUpdateFragment(AbstractLeftMenuFragment fragment, int styleAnimate);
	void nofityChangedLanguage(String newLang);
	void notifyDrawerClose();
	void notifyNavigationDrawerItemSelected(int position);
}
