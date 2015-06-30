package com.hopthanh.gala.app.actionbar_custom;

public interface ActionBarFragmentListener {
	void notifyUpdateActionBarFragment(AbstractActionBarFragment fragment);
	void notifyOpenOrCloseLeftMenu();
	void notifyStartWebViewActivity(String url);
}
