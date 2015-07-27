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
import com.hopthanh.galagala.app.R;

import android.app.ActionBar.LayoutParams;
import android.content.Context;
import android.graphics.Color;
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

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		View view = convertView;
		
		final NgnHistoryEvent event = (NgnHistoryEvent)getItem(position);
		
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
					break;
			}
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

		TextView tvContent = (TextView) view.findViewById(R.id.screen_chat_item_textView);
		tvContent.setText(content == null ? NgnStringUtils.emptyValue() : content);
		LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
		params.gravity = bIncoming ? Gravity.LEFT : Gravity.RIGHT;
		tvContent.setLayoutParams(params);
		
//		tvContent.setBackgroundResource(bIncoming ? R.color.bg_chat_in_item : R.color.white);
		
		TextView tvDate = ((TextView)view.findViewById(R.id.screen_chat_item_textView_date));
		tvDate.setText(DateTimeUtils.getFriendlyDateString(new Date(event.getStartTime())));
//		tvDate.setBackgroundResource(bIncoming ? R.color.bg_chat_in_item : R.color.white);
		
		((LinearLayout) view.findViewById(R.id.ln_chat_item_content)).setBackgroundResource(bIncoming ? R.color.white : R.color.bg_chat_in_item);
		
//		view.findViewById(R.id.screen_chat_item_imageView_top_left)
//		.setBackgroundResource(bIncoming? R.drawable.baloon_in_top_left : R.drawable.baloon_out_top_left);
//		view.findViewById(R.id.screen_chat_item_imageView_top_center)
//		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_top_center : R.drawable.baloon_out_top_center);
//		view.findViewById(R.id.screen_chat_item_imageView_top_right)
//		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_top_right : R.drawable.baloon_out_top_right);
//		
//		view.findViewById(R.id.screen_chat_item_imageView_middle_left)
//		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_middle_left : R.drawable.baloon_out_middle_left);
//		view.findViewById(R.id.screen_chat_item_imageView_middle_right)
//		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_middle_right : R.drawable.baloon_out_middle_right);
//		
//		view.findViewById(R.id.screen_chat_item_imageView_bottom_left)
//		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_bottom_left : R.drawable.baloon_out_bottom_left);
//		view.findViewById(R.id.screen_chat_item_imageView_bottom_center)
//		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_bottom_center : R.drawable.baloon_out_bottom_center);
//		view.findViewById(R.id.screen_chat_item_imageView_bottom_right)
//		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_bottom_right : R.drawable.baloon_out_bottom_right);

		ImageView topLeft = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_left);
		topLeft.setBackgroundResource((bIncoming && bIncoming != bIncomingPre) ? R.drawable.message_outcoming_top_left:R.color.trans);
		view.findViewById(R.id.screen_chat_item_textView).setBackgroundResource(bIncoming ? R.color.white : R.color.bg_chat_in_item);
		ImageView topRight = (ImageView) view.findViewById(R.id.screen_chat_item_imageView_top_right);
		topRight.setBackgroundResource((!bIncoming && bIncoming != bIncomingPre) ? R.drawable.message_incoming_top_right : R.color.trans);

		((ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_left)).setVisibility(bIncoming != bIncomingPre ? View.VISIBLE : View.GONE);
		((ImageView) view.findViewById(R.id.screen_chat_item_imageView_avatar_right)).setVisibility(bIncoming != bIncomingPre ? View.VISIBLE : View.GONE);
		
		if(bIncoming) {
			topLeft.setVisibility(View.VISIBLE);
			topRight.setVisibility(View.GONE);
		} else {
			topRight.setVisibility(View.VISIBLE);
			topLeft.setVisibility(View.GONE);			
		}

		view.findViewById(R.id.screen_chat_item_linearLayout_left)
		.setVisibility(bIncoming ? View.VISIBLE : View.GONE);
		view.findViewById(R.id.screen_chat_item_linearLayout_right)
		.setVisibility(bIncoming ? View.GONE : View.VISIBLE);
		
		return view;
	}

	@Override
	public void update(Observable observable, Object data) {
		// TODO Auto-generated method stub
		refresh();
	}

}
