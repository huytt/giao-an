package com.hopthanh.gala.app;

public interface NavigationDrawerFragmentListener {
	void notifyUpdateFragment(AbstractLeftMenuFragment fragment, int styleAnimate);
	void nofityChangedLanguage();
	void notifyDrawerClose();
	void notifyNavigationDrawerItemSelected(int position);
}
