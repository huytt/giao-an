package com.hopthanh.gala.utils;

import android.animation.Animator;
import android.animation.ValueAnimator;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver;
import android.view.animation.Animation;

public class AnimationSlideRightLeftLayout {
	private ViewGroup mLayout = null;
	private ValueAnimator mAnimator = null;
	private int mWidthLayout = 0;
	
	public AnimationSlideRightLeftLayout(ViewGroup layout) {
		this.mLayout = layout;
		init();
	}

	public AnimationSlideRightLeftLayout(ViewGroup layout, int widthLayout) {
		this.mLayout = layout;
		mWidthLayout = widthLayout;
		init();
	}

	private void init() {
		mLayout.getViewTreeObserver().addOnPreDrawListener(
				new ViewTreeObserver.OnPreDrawListener() {
					@Override
					public boolean onPreDraw() {
						mLayout.getViewTreeObserver().removeOnPreDrawListener(this);
						// mLayoutFooter.setVisibility(View.GONE);

						// final int widthSpec =
						// View.MeasureSpec.makeMeasureSpec(0,
						// View.MeasureSpec.UNSPECIFIED);
						// final int heightSpec =
						// View.MeasureSpec.makeMeasureSpec(0,
						// View.MeasureSpec.UNSPECIFIED);
						// mLayoutFooter.measure(widthSpec, heightSpec);

						mAnimator = slideAnimator(0, mWidthLayout == 0 ? mLayout.getMeasuredWidth():mWidthLayout);
						return true;
					}
				});

//		mLayout.setOnClickListener(new View.OnClickListener() {
//
//			@Override
//			public void onClick(View v) {
//				if (mLayout.getVisibility() == View.GONE) {
//					expand();
//				} else {
//					collapse();
//				}
//			}
//		});
	}
	
	public void expand() {
		//set Visible
		mLayout.setVisibility(View.VISIBLE);
		
		/* Remove and used in preDrawListener
		final int widthSpec = View.MeasureSpec.makeMeasureSpec(0, View.MeasureSpec.UNSPECIFIED);
		final int heightSpec = View.MeasureSpec.makeMeasureSpec(0, View.MeasureSpec.UNSPECIFIED);
		mLinearLayout.measure(widthSpec, heightSpec);

		mAnimator = slideAnimator(0, mLinearLayout.getMeasuredHeight());
		*/
		mAnimator.start();
	}
	
	public void collapse() {
		int finalWidth = mLayout.getWidth();

		ValueAnimator mAnimator = slideAnimator(finalWidth, 0);
		
		mAnimator.addListener(new Animator.AnimatorListener() {
			@Override
			public void onAnimationEnd(Animator animator) {
				//Height=0, but it set visibility to GONE
				mLayout.setVisibility(View.GONE);
			}
			
			@Override
			public void onAnimationStart(Animator animator) {
			}

			@Override
			public void onAnimationCancel(Animator animator) {
			}

			@Override
			public void onAnimationRepeat(Animator animator) {
			}
		});
		mAnimator.start();
	}
	
	private ValueAnimator slideAnimator(int start, int end) {
		
		ValueAnimator animator = ValueAnimator.ofInt(start, end);
		
		
		animator.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
			@Override
			public void onAnimationUpdate(ValueAnimator valueAnimator) {
				//Update Height
				int value = (Integer) valueAnimator.getAnimatedValue();

				ViewGroup.LayoutParams layoutParams = mLayout.getLayoutParams();
				layoutParams.width = value;
				mLayout.setLayoutParams(layoutParams);
			}
		});
		return animator;
	}

	public int getWidthLayout() {
		return mWidthLayout;
	}

	public void setWidthLayout(int mWidthLayout) {
		this.mWidthLayout = mWidthLayout;
	}
}
