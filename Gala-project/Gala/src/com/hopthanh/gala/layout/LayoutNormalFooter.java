package com.hopthanh.gala.layout;

import com.hopthanh.gala.app.R;
import com.hopthanh.gala.customview.CustomViewPagerSwipeAbleDisable;
import com.hopthanh.gala.objects.Store_fake;
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

public class LayoutNormalFooter extends AbstractLayout{

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
		
		final AnimationExpandCollaspeLayout animator = new AnimationExpandCollaspeLayout(lnInfo);
		
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

	@Override
	public int getObjectType() {
		// TODO Auto-generated method stub
		return 0;
	}

}
