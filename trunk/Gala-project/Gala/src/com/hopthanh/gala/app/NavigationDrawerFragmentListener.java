package com.hopthanh.gala.app;

import com.hopthanh.gala.app.left_menu.AbstractLeftMenuFragment;

public interface NavigationDrawerFragmentListener {
	void notifyUpdateFragment(AbstractLeftMenuFragment fragment, int styleAnimate);
	void nofityChangedLanguage();
	void notifyDrawerClose();
	void notifyNavigationDrawerItemSelected(int position);
	void notifyStartWebViewActivity(String url);
}
