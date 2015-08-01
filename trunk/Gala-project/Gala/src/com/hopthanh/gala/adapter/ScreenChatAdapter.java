package com.hopthanh.gala.adapter;

import java.util.Collections;
import java.util.Comparator;
import java.util.Date;
import java.util.List;
import java.util.Observable;
import java.util.Observer;

import org.doubango.ngn.media.NgnMediaType;
import org.doubango.ngn.model.NgnHistoryEvent;
import org.doubango.ngn.model.NgnHistoryEvent.StatusType;
import org.doubango.ngn.model.NgnHistorySMSEvent;
import org.doubango.ngn.services.INgnHistoryService;
import org.doubango.ngn.utils.NgnPredicate;
import org.doubango.ngn.utils.NgnStringUtils;

import com.hopthanh.gala.utils.DateTimeUtils;
import com.hopthanh.galagala.app.FontManager;
import com.hopthanh.galagala.app.R;

import android.app.ActionBar.LayoutParams;
import android.content.Context;
import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public class ScreenChatAdapter extends BaseAdapter implements Observer{

	private static final String TAG = "ScreenChatAdapter";
	private List<NgnHistoryEvent> mEvents;
	private final LayoutInflater mInflater;
	private final Handler mHandler;
	private final Context mContext;
	private final INgnHistoryService mHistorytService;
	private static String mRemoteParty;

	public ScreenChatAdapter(Context context, INgnHistoryService historytService, String remoteParty) {
		mContext = context;
		mHandler = new Handler();
		mInflater = LayoutInflater.from(mContext);
		mHistorytService = historytService;
		mRemoteParty = remoteParty;
		mEvents = mHistorytService.getObservableEvents().filter(new HistoryEventChatFilter());
		Collections.sort(mEvents, new DateComparator());
		mHistorytService.getObservableEvents().addObserver(this);
	}
	
	@Override
	protected void finalize() throws Throwable {
		mHistorytService.getObservableEvents().deleteObserver(this);
		super.finalize();
	}

	public void refresh(){
		mEvents = mHistorytService.getObservableEvents().filter(new HistoryEventChatFilter());
		Collections.sort(mEvents, new DateComparator());
		if (Thread.currentThread() == Looper.getMainLooper().getThread()) {
			notifyDataSetChanged();
		} else {
			mHandler.post(new Runnable() {
				@Override
				public void run() {
					notifyDataSetChanged();
				}
			});
		}
	}

	//
	// HistoryEventSMSFilter
	//
	static class HistoryEventChatFilter implements NgnPredicate<NgnHistoryEvent>{
		@Override
		public boolean apply(NgnHistoryEvent event) {
			if (event != null && (event.getMediaType() == NgnMediaType.SMS)){
				return NgnStringUtils.equals(mRemoteParty, event.getRemoteParty(), false);
			}
			return false;
		}
	}
	
	//
	// DateComparator
	//
	static class DateComparator implements Comparator<NgnHistoryEvent>{
	    @Override
	    public int compare(NgnHistoryEvent e1, NgnHistoryEvent e2) {
	    	return (int)(e1.getStartTime() - e2.getStartTime());
	    }
	}

	@Override
	public int getCount() {
		// TODO Auto-generated method stub
		return mEvents.size();
	}

	@Override
	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return mEvents.get(position);
	}

	@Override
	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	private class ViewHolder {
		int position;
		ImageView imgTopLeft;
		ImageView imgTopMid;
		ImageView imgTopRight;
		ImageView imgMidLeft;
		ImageView imgMidRight;
		ImageView imgBottomLeft;
		ImageView imgBottomMid;
		ImageView imgBottomRight;
		ImageView imgAvatarLeft;
		ImageView imgAvatarRight;
		TextView tvContent;
		TextView tvDate;
		LinearLayout lnContent;
		LinearLayout lnLeft;
		LinearLayout lnRight;
		LinearLayout lnLeftPadding;
		LinearLayout lnRightPadding;
		
		private void initView(View view) {
			imgTopLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_left);
			imgTopMid = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_center);
			imgTopRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_right);
			
			imgMidLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_middle_left);
			tvContent = (TextView) view.findViewById(R.id.screen_chat_item_textView);
			tvDate = (TextView) view.findViewById(R.id.screen_chat_item_textView_date);
			imgMidRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_middle_right);
			
			imgBottomLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_bottom_left);
			imgBottomMid = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_bottom_center);
			imgBottomRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_bottom_right);
			
			imgAvatarLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_left);
			lnContent = (LinearLayout) view.findViewById(R.id.ln_chat_item_content);
			imgAvatarRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_right);
			
			lnLeft = (LinearLayout) view.findViewById(R.id.screen_chat_item_linearLayout_left);
			lnRight = (LinearLayout) view.findViewById(R.id.screen_chat_item_linearLayout_right);
			
			lnLeftPadding = (LinearLayout) view.findViewById(R.id.screen_chat_item_linearLayout_left_padding);
			lnRightPadding = (LinearLayout) view.findViewById(R.id.screen_chat_item_linearLayout_right_padding);
		}
	};
	
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View view = convertView;
		
		final NgnHistoryEvent event = (NgnHistoryEvent)getItem(position);
		ViewHolder holder = null;
		
		if(event == null){
			return null;
		}
		
		if (view == null) {
			switch(event.getMediaType()){
				case Audio:
				case AudioVideo:
				case FileTransfer:
				default:
					Log.e(TAG, "Invalid media type");
					return null;
				case SMS:
					view = mInflater.inflate(R.layout.screen_chat_item_edit, null);
					holder = new ViewHolder();
					holder.position = position;
					holder.initView(view);
					
//					holder.imgTopLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_left);
//					holder.imgTopMid = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_center);
//					holder.imgTopRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_right);
//					
//					holder.imgMidLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_middle_left);
//					holder.tvContent = (TextView) view.findViewById(R.id.screen_chat_item_textView);
//					holder.tvDate = (TextView) view.findViewById(R.id.screen_chat_item_textView_date);
//					holder.imgMidRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_middle_right);
//					
//					holder.imgBottomLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_bottom_left);
//					holder.imgBottomMid = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_bottom_center);
//					holder.imgBottomRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_bottom_right);
//					
//					holder.imgAvatarLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_left);
//					holder.imgAvatarLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_right);

					view.setTag(holder);
					break;
			}
		} else if (((ViewHolder) view.getTag()).position != position) {
			view = mInflater.inflate(R.layout.screen_chat_item_edit, null);
			holder = new ViewHolder();
			holder.position = position;
			holder.initView(view);
			view.setTag(holder);
		} else {
			holder = (ViewHolder) view.getTag();
		}
		
		final NgnHistorySMSEvent SMSEvent = (NgnHistorySMSEvent)event;
		final String content = SMSEvent.getContent();
		final boolean bIncoming = SMSEvent.getStatus() == StatusType.Incoming;
		
		boolean bIncomingPre = false;
		if(position > 0) {
			bIncomingPre = ((NgnHistorySMSEvent)getItem(position - 1)).getStatus() == StatusType.Incoming;
		} else {
			bIncomingPre = !bIncoming;
		}

//		TextView tvContent = (TextView) view.findViewById(R.id.screen_chat_item_textView);
//		tvContent.setText(content == null ? NgnStringUtils.emptyValue() : content);
//		LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
//		params.gravity = bIncoming ? Gravity.RIGHT : Gravity.LEFT;
//		tvContent.setLayoutParams(params);
//		
//		Typeface customFont = FontManager.getInstance().getCustomFont(mContext.getAssets(), FontManager.FONT_SFUFUTURABOOK);
//		tvContent.setTypeface(customFont);
//
//		//		tvContent.setBackgroundResource(bIncoming ? R.color.bg_chat_in_item : R.color.white);
//		
//		TextView tvDate = ((TextView)view.findViewById(R.id.screen_chat_item_textView_date));
//		tvDate.setText(DateTimeUtils.getFriendlyDateString(new Date(event.getStartTime())));
//		tvDate.setTypeface(customFont);
//		
////		tvDate.setBackgroundResource(bIncoming ? R.color.bg_chat_in_item : R.color.white);
//		
//		((LinearLayout) view.findViewById(R.id.ln_chat_item_content)).setBackgroundResource(bIncoming ? R.color.bg_chat_in_item: R.color.white);
//		
//		view.findViewById(R.id.screen_chat_item_imageView_top_left)
//		.setBackgroundResource(bIncoming? R.drawable.message_incoming_top_left : (bIncoming != bIncomingPre ? R.drawable.message_outcoming_top_left : R.drawable.message_outcoming_top_left_no_expand));
//		view.findViewById(R.id.screen_chat_item_imageView_top_center)
//		.setBackgroundResource(bIncoming ? R.drawable.message_incoming_top_mid : R.drawable.message_outcoming_top_mid);
//		view.findViewById(R.id.screen_chat_item_imageView_top_right)
//		.setBackgroundResource(bIncoming ? (bIncoming != bIncomingPre ? R.drawable.message_incoming_top_right : R.drawable.message_incoming_top_right_no_expand) : R.drawable.message_outcoming_top_right);
//		
//		view.findViewById(R.id.screen_chat_item_imageView_middle_left)
//		.setBackgroundResource(bIncoming ? R.drawable.message_incoming_mid_left : R.drawable.message_outcoming_mid_left);
//		view.findViewById(R.id.screen_chat_item_imageView_middle_right)
//		.setBackgroundResource(bIncoming ? R.drawable.message_incoming_mid_right : R.drawable.message_outcoming_mid_right);
//		
//		view.findViewById(R.id.screen_chat_item_imageView_bottom_left)
//		.setBackgroundResource(bIncoming ? R.drawable.message_incoming_bottom_left : R.drawable.message_outcoming_bottom_left);
//		view.findViewById(R.id.screen_chat_item_imageView_bottom_center)
//		.setBackgroundResource(bIncoming ? R.drawable.message_incoming_bottom_mid : R.drawable.message_outcoming_bottom_mid);
//		view.findViewById(R.id.screen_chat_item_imageView_bottom_right)
//		.setBackgroundResource(bIncoming ? R.drawable.message_incoming_bottom_right : R.drawable.message_outcoming_bottom_right);
//
//		((ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_left)).setVisibility(bIncoming != bIncomingPre ? View.VISIBLE : View.GONE);
//		((ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_right)).setVisibility(bIncoming != bIncomingPre ? View.VISIBLE : View.GONE);
//		
//		view.findViewById(R.id.screen_chat_item_linearLayout_left_padding)
//		.setVisibility(bIncoming ? View.GONE : View.VISIBLE);
//		view.findViewById(R.id.screen_chat_item_linearLayout_right_padding)
//		.setVisibility(bIncoming ? View.VISIBLE : View.GONE);

//		view.findViewById(R.id.screen_chat_item_linearLayout_left)
//		.setVisibility(bIncoming ? View.VISIBLE : View.GONE);
//		view.findViewById(R.id.screen_chat_item_linearLayout_right)
//		.setVisibility(bIncoming ? View.GONE : View.VISIBLE);

		Typeface customFont = FontManager.getInstance().getCustomFont(mContext.getAssets(), FontManager.FONT_SFUFUTURABOOK);
		holder.tvContent.setText(content == null ? NgnStringUtils.emptyValue() : content);
		LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
		params.gravity = bIncoming ? Gravity.RIGHT : Gravity.LEFT;
		holder.tvContent.setLayoutParams(params);
		holder.tvContent.setTypeface(customFont);
		
		holder.tvDate.setText(DateTimeUtils.getFriendlyDateString(new Date(event.getStartTime())));
		holder.tvDate.setTypeface(customFont);

		if(bIncoming) {
			holder.imgTopLeft.setBackgroundResource(R.drawable.message_incoming_top_left);
			holder.imgTopMid.setBackgroundResource(R.drawable.message_incoming_top_mid);
			holder.imgTopRight.setBackgroundResource(bIncoming != bIncomingPre ? R.drawable.message_incoming_top_right : R.drawable.message_incoming_top_right_no_expand);

			holder.imgMidLeft.setBackgroundResource(R.drawable.message_incoming_mid_left);
			holder.lnContent.setBackgroundResource(R.color.bg_chat_in_item);
			holder.imgMidRight.setBackgroundResource(R.drawable.message_incoming_mid_right);
			
			holder.imgBottomLeft.setBackgroundResource(R.drawable.message_incoming_bottom_left);
			holder.imgBottomMid.setBackgroundResource(R.drawable.message_incoming_bottom_mid);
			holder.imgBottomRight.setBackgroundResource(R.drawable.message_incoming_bottom_right);

		} else {
			holder.imgTopLeft.setBackgroundResource(bIncoming != bIncomingPre ? R.drawable.message_outcoming_top_left : R.drawable.message_outcoming_top_left_no_expand);
			holder.imgTopMid.setBackgroundResource(R.drawable.message_outcoming_top_mid);
			holder.imgTopRight.setBackgroundResource(R.drawable.message_outcoming_top_right);

			holder.imgMidLeft.setBackgroundResource(R.drawable.message_outcoming_mid_left);
			holder.lnContent.setBackgroundResource(R.color.white);
			holder.imgMidRight.setBackgroundResource(R.drawable.message_outcoming_mid_right);
			
			holder.imgBottomLeft.setBackgroundResource(R.drawable.message_outcoming_bottom_left);
			holder.imgBottomMid.setBackgroundResource(R.drawable.message_outcoming_bottom_mid);
			holder.imgBottomRight.setBackgroundResource(R.drawable.message_outcoming_bottom_right);
		}

		holder.imgAvatarLeft.setVisibility(bIncoming != bIncomingPre ? View.VISIBLE : View.GONE);
		holder.imgAvatarRight.setVisibility(bIncoming != bIncomingPre ? View.VISIBLE : View.GONE);

		holder.lnLeftPadding.setVisibility(bIncoming ? View.GONE : View.VISIBLE);
		holder.lnRightPadding.setVisibility(bIncoming ? View.VISIBLE : View.GONE);

		holder.lnLeft.setVisibility(bIncoming ? View.VISIBLE : View.GONE);
		holder.lnRight.setVisibility(bIncoming ? View.GONE : View.VISIBLE);
		
		return view;
	}

	@Override
	public void update(Observable observable, Object data) {
		// TODO Auto-generated method stub
		refresh();
	}

}
