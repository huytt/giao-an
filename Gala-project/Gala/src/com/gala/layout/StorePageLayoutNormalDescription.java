package com.gala.layout;

import com.gala.app.R;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;

public class StorePageLayoutNormalDescription extends AbstractLayout{

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(Activity context, LayoutInflater inflater,
			ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_normal_description, container, false);
		WebView wvDescription = (WebView) v.findViewById(R.id.wvDescription);
		String data = "<div> "
				+ "<p></p><p>1908年，米爾斯·匡威侯爵以自己的姓氏Converse美國麻州</p>"
				+ "<p>創立了匡威（Converse Rubber Shoe Company）</p>"
				+ "<p>受到高中和大學運動員喜愛匡威的運動鞋變得相當的流行</p>"
				+ "<p>匡威的鞋子變成是當時必備鞋。</p>"
				+ "<p>現在開始進入Converse潮流世界吧!</p><p></p>" 
				+ "</div>";
//		wvDescription.loadData(data, "text/html", "UTF-8");
		wvDescription.loadDataWithBaseURL(null, data, "text/html", "UTF-8", null);
		return v;
	}

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
