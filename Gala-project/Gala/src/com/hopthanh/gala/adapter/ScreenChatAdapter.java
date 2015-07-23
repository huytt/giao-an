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

import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
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
					view = mInflater.inflate(R.layout.screen_chat_item, null);
					break;
			}
		}
		
		final NgnHistorySMSEvent SMSEvent = (NgnHistorySMSEvent)event;
		final String content = SMSEvent.getContent();
		final boolean bIncoming = SMSEvent.getStatus() == StatusType.Incoming;
		
		TextView textView = (TextView) view.findViewById(R.id.screen_chat_item_textView);
		textView.setText(content == null ? NgnStringUtils.emptyValue() : content);
		textView.setBackgroundResource(bIncoming ? R.drawable.baloon_in_middle_center : R.drawable.baloon_out_middle_center);
		
		((TextView)view.findViewById(R.id.screen_chat_item_textView_date))
			.setText(DateTimeUtils.getFriendlyDateString(new Date(event.getStartTime())));
		
		view.findViewById(R.id.screen_chat_item_imageView_top_left)
		.setBackgroundResource(bIncoming? R.drawable.baloon_in_top_left : R.drawable.baloon_out_top_left);
		view.findViewById(R.id.screen_chat_item_imageView_top_center)
		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_top_center : R.drawable.baloon_out_top_center);
		view.findViewById(R.id.screen_chat_item_imageView_top_right)
		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_top_right : R.drawable.baloon_out_top_right);
		
		view.findViewById(R.id.screen_chat_item_imageView_middle_left)
		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_middle_left : R.drawable.baloon_out_middle_left);
		view.findViewById(R.id.screen_chat_item_imageView_middle_right)
		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_middle_right : R.drawable.baloon_out_middle_right);
		
		view.findViewById(R.id.screen_chat_item_imageView_bottom_left)
		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_bottom_left : R.drawable.baloon_out_bottom_left);
		view.findViewById(R.id.screen_chat_item_imageView_bottom_center)
		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_bottom_center : R.drawable.baloon_out_bottom_center);
		view.findViewById(R.id.screen_chat_item_imageView_bottom_right)
		.setBackgroundResource(bIncoming ? R.drawable.baloon_in_bottom_right : R.drawable.baloon_out_bottom_right);
		
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
