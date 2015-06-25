package com.hopthanh.gala.app;

import java.util.ArrayList;
import java.util.logging.Level;

import org.javatuples.Quintet;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.gala.app.R;
import com.hopthanh.gala.layout.AbstractLayout;
import com.hopthanh.gala.layout.LayoutLeftMenu;
import com.hopthanh.gala.layout.LayoutLeftMenuCategory;
import com.hopthanh.gala.objects.Category;
import com.hopthanh.gala.objects.Category_MultiLang;
import com.hopthanh.gala.objects.Media;
import com.hopthanh.gala.objects.MenuDataClass;
import com.hopthanh.gala.utils.Utils;

import android.graphics.Shader.TileMode;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.RelativeLayout;
import android.widget.TextView;


public class LeftMenuCategoryFragment extends AbstractLeftMenuFragment{
//	private static final String TAG = "HomePageFragment";
	private int mCategoryLevel = 0;
	private long mParentCateId = 0;
	private long mParentCateIdPrevious = 0;
	private LeftMenuCategoryTitle mTitle = null;

	public LeftMenuCategoryFragment(int categoryLevel, long parentCateId, LeftMenuCategoryTitle title) {
		super();
		mCategoryLevel = categoryLevel;
		mParentCateId = parentCateId;
		mTitle = title;
	}
	
	public LeftMenuCategoryFragment(int categoryLevel, long parentCateId, long parentCateIdPrevious) {
		super();
		mCategoryLevel = categoryLevel;
		mParentCateId = parentCateId;
		mParentCateIdPrevious = parentCateIdPrevious;
	}

	public LeftMenuCategoryFragment(int categoryLevel, long parentCateId, long parentCateIdPrevious, LeftMenuCategoryTitle title) {
		super();
		mCategoryLevel = categoryLevel;
		mParentCateId = parentCateId;
		mParentCateIdPrevious = parentCateIdPrevious;
		mTitle = title;
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.layout_menu_child, container, false);
		
		RelativeLayout rlPrevious = (RelativeLayout) mView.findViewById(R.id.rlPrevious);
		TextView tvLeftMenuTitle = (TextView) mView.findViewById(R.id.tvLeftMenuTitle);
		
		tvLeftMenuTitle.setText(mTitle.getTitle());
		
		rlPrevious.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				if(mCategoryLevel == 0) {
					mListener.notifyUpdateFragment(new LeftMenuMainFragment(), NavigationDrawerFragment.SLIDE_LEFT_RIGHT);
				} else if(mCategoryLevel > 0) {
					mListener.notifyUpdateFragment(new LeftMenuCategoryFragment(mCategoryLevel - 1, mParentCateIdPrevious, mTitle.getParent()), 
							NavigationDrawerFragment.SLIDE_LEFT_RIGHT);
				}
			}
		});
		
		mDrawerListView = (ListView) mView.findViewById(R.id.lsMenuChild);
		mDrawerListView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				selectItem(position);
			}
		});

		ArrayList<AbstractLayout> arrLayouts = new ArrayList<AbstractLayout>();
		for(Quintet<Category, Media, Media,Category_MultiLang, Integer> item : mDataSource.get(Integer.valueOf(mCategoryLevel)).get(Long.valueOf(mParentCateId))) {
			boolean hasChild = false;
			if(mDataSource.containsKey(mCategoryLevel + 1) &&
			   mDataSource.get(Integer.valueOf(mCategoryLevel + 1)).containsKey(item.getValue0().getCategoryId())
			) {
				hasChild = true;
			}
			String itemNameLv0 = item.getValue3() == null ? item.getValue0().getCategoryName() : item.getValue3().getCategoryName();
			String imgUrl = item.getValue1() != null ? Utils.XONE_SERVER + item.getValue1().getUrl() + item.getValue1().getMediaName():null;
			LayoutLeftMenuCategory temp = new LayoutLeftMenuCategory(
					getActivity().getApplicationContext(), 
					hasChild, 
					item.getValue0().getCategoryId(),
					mParentCateId, 
					new LeftMenuCategoryTitle(itemNameLv0, mTitle));
			temp.setDataSource(new MenuDataClass(itemNameLv0, imgUrl));
			arrLayouts.add(temp);
		}
		
		MultiLayoutContentListViewAdapter adapter = new MultiLayoutContentListViewAdapter(arrLayouts, getActivity().getApplicationContext());
		mDrawerListView.setAdapter(adapter);
		return mView;
	}
	
	private void selectItem(int position) {
		mCurrentSelectedPosition = position;
		if (mDrawerListView != null) {
			mDrawerListView.setItemChecked(position, true);
		}
		
		MultiLayoutContentListViewAdapter adapter = (MultiLayoutContentListViewAdapter) mDrawerListView.getAdapter();
		LayoutLeftMenuCategory layout = (LayoutLeftMenuCategory)adapter.getLayout(position);
		if (layout.isHasChild()) {
			LeftMenuCategoryFragment fragment = new LeftMenuCategoryFragment(
					mCategoryLevel + 1, 
					layout.getCategoryId(), 
					mParentCateId, 
					layout.getTitle());
			fragment.setDataSource(mDataSource);
			mListener.notifyUpdateFragment(fragment, NavigationDrawerFragment.SLIDE_RIGHT_LEFT);
		}
		
		if (!layout.isHasChild()) {
			mListener.notifyDrawerClose();
		}
		
//		mListener.notifyNavigationDrawerItemSelected(position);
	}
	
	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if (mView != null && mView.getParent() != null) {
//			ListView ls = (ListView) mView.findViewById(R.id.lsLayoutContain);
//			((MultiLayoutContentListViewAdapter) ls.getAdapter()).clearAllLayouts();
//			ls = null;
//			mLvLayoutContainer = null;
//			mInflater = null;
//			mContainer = null;
//			mLayoutContain.removeAllViews();
            ((ViewGroup) mView.getParent()).removeView(mView);
            mView = null;
            System.gc();
        }
		super.onDestroyView();
	}
	public long getParentCateId() {
		return mParentCateId;
	}
	public void setParentCateId(long mParentCateId) {
		this.mParentCateId = mParentCateId;
	}
	
	public static class LeftMenuCategoryTitle {
		private String mTitle;
		private LeftMenuCategoryTitle mParent = null;
		
		public LeftMenuCategoryTitle(String title) {
			mTitle = title;
			mParent = this;
		}

		public LeftMenuCategoryTitle(String title, String titleParent) {
			mTitle = title;
			mParent = new LeftMenuCategoryTitle(titleParent);
		}

		public LeftMenuCategoryTitle(String title, LeftMenuCategoryTitle titleParent) {
			mTitle = title;
			mParent = titleParent;
		}

		public String getTitle() {
			return mTitle;
		}
		public void setTitle(String mTitle) {
			this.mTitle = mTitle;
		}
		public LeftMenuCategoryTitle getParent() {
			return mParent;
		}
		public void setParent(LeftMenuCategoryTitle mParent) {
			this.mParent = mParent;
		}
	}
}
