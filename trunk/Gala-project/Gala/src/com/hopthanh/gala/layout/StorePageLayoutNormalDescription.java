package com.hopthanh.gala.layout;

import com.hopthanh.gala.app.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;

public class StorePageLayoutNormalDescription extends AbstractLayout{

	public StorePageLayoutNormalDescription(Context context) {
		super(context);
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getLayoutType() {
		// TODO Auto-generated method stub
		return LAYOUT_TYPE_NORMAL;
	}

	@Override
	public View getView(LayoutInflater inflater, ViewGroup container) {
		// TODO Auto-generated method stub
		View v = inflater.inflate(R.layout.store_page_layout_normal_description, container, false);
		WebView wvDescription = (WebView) v.findViewById(R.id.wvDescription);
		String data = "<div> "
				+ "<p></p><p>1908年，米爾斯·匡�?侯爵以自己的姓�?Converse美國麻州</p>"
				+ "<p>創立了匡�?（Converse Rubber Shoe Company）</p>"
				+ "<p>�?�到高中和大學�?�動員喜愛匡�?的�?�動鞋變得相當的�?行</p>"
				+ "<p>匡�?的鞋�?變�?是當時必備鞋。</p>"
				+ "<p>�?�在開始進入Converse潮�?世界�?�!</p><p></p>" 
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
