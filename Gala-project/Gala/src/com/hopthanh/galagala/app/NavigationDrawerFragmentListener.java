package com.hopthanh.galagala.app;

import com.hopthanh.galagala.app.left_menu.AbstractLeftMenuFragment;

public interface NavigationDrawerFragmentListener {
	void notifyUpdateFragment(AbstractLeftMenuFragment fragment, int styleAnimate);
	void nofityChangedLanguage();
	void notifyDrawerClose();
	void notifyNavigationDrawerItemSelected(int titleId);
	void notifyStartWebViewActivity(String url);
}
