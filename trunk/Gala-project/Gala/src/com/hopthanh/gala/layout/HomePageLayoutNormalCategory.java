package com.hopthanh.gala.layout;

import java.util.ArrayList;

import javax.sql.DataSource;

import org.javatuples.Quartet;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerSwipeAbleDisable;
import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.Store_fake;
import com.hopthanh.gala.utils.AppConstant;
import com.hopthanh.gala.utils.Utils;
import com.hopthanh.gala.web_api_util.AnimationExpandCollaspeLayout;
import com.squareup.picasso.MemoryPolicy;
import com.squareup.picasso.Picasso;

import android.app.Activity;
import android.content.Context;
import android.graphics.Typeface;
import android.opengl.Visibility;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public class HomePageLayoutNormalCategory extends AbstractLayout{

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
		final AnimationExpandCollaspeLayout animator = new AnimationExpandCollaspeLayout(lnExpandable);
		
		tvExpand.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if(lnExpandable.getVisibility() == View.GONE) {
					tvExpand.setText("Collapse");
					animator.expand();
				} else {
					tvExpand.setText("Expand");
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
		TextView tvItem0 = (TextView)vItem.findViewById(R.id.tvItem0);
		ImageView imgItem0 = (ImageView)vItem.findViewById(R.id.imgItem0);
		TextView tvItem1 = (TextView)vItem.findViewById(R.id.tvItem1);
		ImageView imgItem1 = (ImageView)vItem.findViewById(R.id.imgItem1);
		
		String itemNameLv0 = item1.getValue3() == null ? item1.getValue0().getCategoryName() : item1.getValue3().getCategoryName();
		String imgUrl = Utils.XONE_SERVER + item1.getValue1().getUrl() + item1.getValue1().getMediaName();
		tvItem0.setText(itemNameLv0);
		Picasso.with(mContext).load(imgUrl).resize(imgItem0.getLayoutParams().width, imgItem0.getLayoutParams().height)
		.into(imgItem0);
		
		itemNameLv0 = item2.getValue3() == null ? item2.getValue0().getCategoryName() : item2.getValue3().getCategoryName();
		imgUrl = Utils.XONE_SERVER + item2.getValue1().getUrl() + item2.getValue1().getMediaName();
		tvItem1.setText(itemNameLv0);
		Picasso.with(mContext).load(imgUrl).resize(imgItem1.getLayoutParams().width, imgItem1.getLayoutParams().height)
		.into(imgItem1);
		
		return vItem;
	}
	
	@SuppressWarnings("unchecked")
	private void loadContent(LayoutInflater inflater, ViewGroup container) {
		ArrayList<Quartet<Category, Media, Media, Category_MultiLang>> categories = (ArrayList<Quartet<Category, Media, Media, Category_MultiLang>>) mDataSource;
		for(int i = 0; i < 4; i+=2) {
			lnNotExpand.addView(loadItemContent(inflater, container, categories.get(i), categories.get(i+1)));
		}
		
		lnNotExpand.removeViewAt(0);
		for(int i = 4; i < categories.size(); i+=2) {
			lnExpandable.addView(loadItemContent(inflater, container, categories.get(i), categories.get(i+1)));
		}
		lnNotExpand.addView(lnExpandable);
	}
	
	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}