package com.hopthanh.gala.layout;

import com.hopthanh.gala.objects.Article;
import com.hopthanh.gala.objects.ArticleType;
import com.hopthanh.gala.objects.FooterDataClass;
import com.hopthanh.gala.utils.AnimationExpandCollaspeLayout;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.galagala.app.LanguageManager;
import com.hopthanh.galagala.app.R;
import com.hopthanh.galagala.app.WebViewActivityListener;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;

public class LayoutNormalFooter extends AbstractLayout<FooterDataClass>{

	private LinearLayout lnInfo;
	private ImageButton ibtnExpand;
	
	public LayoutNormalFooter(Context context) {
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
		View v = inflater.inflate(R.layout.layout_footer, container, false);
		lnInfo = (LinearLayout) v.findViewById(R.id.lnInfo);
		ibtnExpand = (ImageButton) v.findViewById(R.id.ibtnExpand);
		
		loadContent(inflater, container);
		
		int height = lnInfo.getLayoutParams().height;
		final AnimationExpandCollaspeLayout animator = new AnimationExpandCollaspeLayout(lnInfo, height);
		lnInfo.setVisibility(View.GONE);
		
		ibtnExpand.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if(lnInfo.getVisibility() == View.GONE) {
					ibtnExpand.setImageResource(R.drawable.ic_up_white);
					animator.expand();
				} else {
					ibtnExpand.setImageResource(R.drawable.ic_down_white);
					animator.collapse();
				}
			}
		});
		return v;
	}

	private void loadContent(LayoutInflater inflater, ViewGroup container) {
		FooterDataClass footerData = mDataSource;
		for (ArticleType item : footerData.getArticleType()) {
			View vItem0 = inflater.inflate(R.layout.layout_footer_aritcle_item_detail_level0, container, false);
			RelativeLayout rlNotExpand = (RelativeLayout) vItem0.findViewById(R.id.rlNotExpand);
			TextView tvItem0 = (TextView) vItem0.findViewById(R.id.tvItem0);
			final LinearLayout lnExpandable = (LinearLayout) vItem0.findViewById(R.id.lnExpandable);
			
			final AnimationExpandCollaspeLayout animator = new AnimationExpandCollaspeLayout(lnExpandable);
			
			rlNotExpand.setOnClickListener(new OnClickListener() {
				
				@Override
				public void onClick(View v) {
					// TODO Auto-generated method stub
					if(lnExpandable.getVisibility() == View.GONE) {
						animator.expand();
					} else {
						animator.collapse();
					}
				}
			});
			
			tvItem0.setText(item.getArticleTypeName());
			
			for (Article article : item.getArticles()) {
				View vItem1 = inflater.inflate(R.layout.layout_footer_aritcle_item_detail_level1, container, false);
				TextView tvItem1 = (TextView) vItem1.findViewById(R.id.tvItem1);
				tvItem1.setText(article.getTitle());
				
				String xoneServer = Utils.XONE_SERVER_WEB + "/Home/setLanguage?lang="+ LanguageManager.getInstance().getCurLangName() + "&u=";
				final String url = String.format("%s/Article/Info/%d", xoneServer, article.getArticleId());
				
				vItem1.setOnClickListener(new OnClickListener() {
					
					@Override
					public void onClick(View v) {
						// TODO Auto-generated method stub
						if(mListener instanceof WebViewActivityListener) {
							((WebViewActivityListener) mListener).notifyStartWebViewActivity(url);
						}
					}
				});
				
				lnExpandable.addView(vItem1);
			}
			animator.setHeightLayout(lnExpandable.getLayoutParams().height);
			lnExpandable.setVisibility(View.GONE);
			lnInfo.addView(vItem0);
		}
	}
	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
