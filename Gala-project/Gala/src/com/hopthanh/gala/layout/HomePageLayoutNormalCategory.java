package com.hopthanh.gala.layout;

import java.util.ArrayList;

import org.javatuples.Quartet;

import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.gala.utils.AnimationExpandCollaspeLayout;
import com.hopthanh.galagala.app.LanguageManager;
import com.hopthanh.galagala.app.R;
import com.hopthanh.galagala.app.WebViewActivityListener;
import com.squareup.picasso.Picasso;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public class HomePageLayoutNormalCategory extends AbstractLayout<ArrayList<Quartet<Category, Media, Media, Category_MultiLang>>>{

	private LinearLayout lnExpandable;
	private LinearLayout lnNotExpand;
	private TextView tvExpand;
	
	public HomePageLayoutNormalCategory(Context context) {
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
		View v = inflater.inflate(R.layout.home_page_layout_category, container, false);
		lnNotExpand = (LinearLayout) v.findViewById(R.id.lnNotExpand);
		lnExpandable = (LinearLayout) v.findViewById(R.id.lnExpandable);
		tvExpand = (TextView) v.findViewById(R.id.tvExpand);
		
		loadContent(inflater, container);
		int heightLayout = lnExpandable.getLayoutParams().height;
		final AnimationExpandCollaspeLayout animator = new AnimationExpandCollaspeLayout(lnExpandable, heightLayout);
		lnExpandable.setVisibility(View.GONE);
		
		tvExpand.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if(lnExpandable.getVisibility() == View.GONE) {
					tvExpand.setText(R.string.homepageCollapse);
					animator.expand();
				} else {
					tvExpand.setText(R.string.homepageViewMore);
					animator.collapse();
				}
			}
		});
		return v;
	}

	private View loadItemContent(LayoutInflater inflater, ViewGroup container, 
			Quartet<Category, Media, Media, Category_MultiLang> item1, 
			Quartet<Category, Media, Media, Category_MultiLang> item2) {
		
		View vItem = inflater.inflate(R.layout.home_page_layout_category_detail_item, container, false);
		LinearLayout lnItem0 = (LinearLayout) vItem.findViewById(R.id.lnItem0);
		TextView tvItem0 = (TextView)vItem.findViewById(R.id.tvItem0);
		ImageView imgItem0 = (ImageView)vItem.findViewById(R.id.imgItem0);
		
		LinearLayout lnItem1 = (LinearLayout) vItem.findViewById(R.id.lnItem1);
		TextView tvItem1 = (TextView)vItem.findViewById(R.id.tvItem1);
		ImageView imgItem1 = (ImageView)vItem.findViewById(R.id.imgItem1);
		
		String itemNameLv0 = item1.getValue3() == null ? item1.getValue0().getCategoryName() : item1.getValue3().getCategoryName();
		String imgUrl = item1.getValue1() != null ? Utils.XONE_SERVER + item1.getValue1().getUrl() + item1.getValue1().getMediaName():null;
		tvItem0.setText(itemNameLv0);
		if(imgUrl != null) {
			Picasso.with(mContext).load(imgUrl).resize(imgItem0.getLayoutParams().width, imgItem0.getLayoutParams().height)
			.into(imgItem0);
		}

		final String urlItem1 = generateUrl(item1.getValue0());
		
		lnItem0.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				// format: "Category/{urlName}-{id}.html"
				((WebViewActivityListener) mListener).notifyStartWebViewActivity(urlItem1);

			}
		});
		
		if (item2 != null) {
			itemNameLv0 = item2.getValue3() == null ? item2.getValue0().getCategoryName() : item2.getValue3().getCategoryName();
			imgUrl = item2.getValue1() != null ? Utils.XONE_SERVER + item2.getValue1().getUrl() + item2.getValue1().getMediaName():null;
			tvItem1.setText(itemNameLv0);
			if(imgUrl != null) {
				Picasso.with(mContext).load(imgUrl).resize(imgItem1.getLayoutParams().width, imgItem1.getLayoutParams().height)
				.into(imgItem1);
			}
			
			final String urlItem2 = generateUrl(item2.getValue0());

			lnItem1.setOnClickListener(new OnClickListener() {
				
				@Override
				public void onClick(View v) {
					// TODO Auto-generated method stub
					// format: "Category/{urlName}-{id}.html"
					((WebViewActivityListener) mListener).notifyStartWebViewActivity(urlItem2);
				}
			});
		}
		
		return vItem;
	}
	
	private String generateUrl(Category category) {
		String strFormat = "%s/Category/%s";
		String strSrc = "";
		if(!category.getAlias().isEmpty()) {
			strSrc = String.format("%s-%d.html",category.getAlias(),category.getCategoryId());
		} else {
			strSrc = String.format("Info/%d", category.getCategoryId());
		}
		String xoneServer = Utils.XONE_SERVER_WEB + "/Home/setLanguage?lang="+ LanguageManager.getInstance().getCurLangName() + "&u=";
		return String.format(strFormat, xoneServer, strSrc);
	}
	
	private void loadContent(LayoutInflater inflater, ViewGroup container) {
		ArrayList<Quartet<Category, Media, Media, Category_MultiLang>> categories = mDataSource;
		
		// if category item less than 4 not
		if(categories.size() < 4) {
			return;
		}
		
		for(int i = 0; i < 4; i+=2) {
			lnNotExpand.addView(loadItemContent(inflater, container, categories.get(i), categories.get(i+1)));
		}
		
		lnNotExpand.removeViewAt(0);
		for(int i = 4; i < categories.size(); i+=2) {
			lnExpandable.addView(loadItemContent(inflater, container, categories.get(i), 
					(i + 1) < categories.size()? categories.get(i+1):null));
		}
		lnNotExpand.addView(lnExpandable);
	}
	
	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
