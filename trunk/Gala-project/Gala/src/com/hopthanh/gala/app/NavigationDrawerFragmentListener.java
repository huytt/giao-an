package com.hopthanh.gala.app;

public interface NavigationDrawerFragmentListener {
	void notifyUpdateFragment(AbstractLeftMenuFragment fragment, int styleAnimate);
	void notifyDrawerClose();
	void notifyNavigationDrawerItemSelected(int position);
}
