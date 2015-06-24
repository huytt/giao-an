package com.hopthanh.gala.app;

public interface NavigationDrawerFragmentListener {
	void notifyUpdateFragment(AbstractMenuFragment fragment, int styleAnimate);
	void notifyDrawerClose();
	void notifyNavigationDrawerItemSelected(int position);
}
