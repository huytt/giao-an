package com.hopthanh.gala.adapter;

import java.util.Date;
import java.util.List;
import java.util.Observable;
import java.util.Observer;

import org.doubango.ngn.model.NgnContact;
import org.doubango.ngn.model.NgnHistoryEvent;
import org.doubango.ngn.model.NgnHistorySMSEvent;
import org.doubango.ngn.model.NgnHistorySMSEvent.HistoryEventSMSIntelligentFilter;
import org.doubango.ngn.services.INgnContactService;
import org.doubango.ngn.services.INgnHistoryService;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;

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

public class ScreenTabMessagesAdapter extends BaseAdapter implements Observer{
	private static final String TAG = "ScreenTabMessagesAdapter";
	private List<NgnHistoryEvent> mEvents;
	private final LayoutInflater mInflater;
	private final Handler mHandler;
//	private final ScreenTabMessages mBaseScreen;
	private final MyHistoryEventSMSIntelligentFilter mFilter;
	private final INgnHistoryService mHistorytService;
	private final INgnContactService mContactService;
	
	public ScreenTabMessagesAdapter(Context context, INgnHistoryService historytService, INgnContactService contactService) {
		mHandler = new Handler();
		mInflater = LayoutInflater.from(context);
		mFilter = new MyHistoryEventSMSIntelligentFilter();
		mHistorytService = historytService;
		mEvents = mHistorytService.getObservableEvents().filter(mFilter);
		mHistorytService.getObservableEvents().addObserver(this);
		mContactService = contactService;
	}
	
	@Override
	protected void finalize() throws Throwable {
		mHistorytService.getObservableEvents().deleteObserver(this);
		super.finalize();
	}

	void refresh(){
		mFilter.reset();
		mEvents = mHistorytService.getObservableEvents().filter(mFilter);
		if(Thread.currentThread() == Looper.getMainLooper().getThread()){
			notifyDataSetChanged();
		}
		else{
			mHandler.post(new Runnable(){
				@Override
				public void run() {
					notifyDataSetChanged();
				}
			});
		}
	}
	
	@Override
	public int getCount() {
		return mEvents.size();
	}

	@Override
	public Object getItem(int position) {
		return mEvents.get(position);
	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
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
					view = mInflater.inflate(R.layout.screen_tab_messages_item_edit, null);
					break;
			}
		}
		
		String remoteParty = event.getRemoteParty();
		NgnContact contact = mContactService.getContactByPhoneNumber(remoteParty);
		if(contact == null){
			remoteParty = NgnUriUtils.getDisplayName(remoteParty);
		}
		else{
			remoteParty = contact.getDisplayName();
		}
		
		final TextView tvRemote = (TextView)view.findViewById(R.id.screen_tab_messages_item_textView_remote);
		final TextView tvDate = (TextView)view.findViewById(R.id.screen_tab_messages_item_textView_date);
		final TextView tvContent = (TextView)view.findViewById(R.id.screen_tab_messages_item_textView_content);
//		final TextView tvUnSeen = (TextView)view.findViewById(R.id.screen_tab_messages_item_textView_unseen);
		
		final NgnHistorySMSEvent SMSEvent = (NgnHistorySMSEvent)event;
		tvRemote.setText(remoteParty);
		tvDate.setText(DateTimeUtils.getFriendlyDateString(new Date(event.getStartTime())));
		final String SMSContent = SMSEvent.getContent();
		tvContent.setText(NgnStringUtils.isNullOrEmpty(SMSContent) 
				? NgnStringUtils.emptyValue() : SMSContent);
//		tvUnSeen.setText(Integer.toString(mFilter.getUnSeen()));
		
		return view;
	}

	@Override
	public void update(Observable observable, Object data) {
		Log.e(TAG, "update()");
		refresh();
	}
	
	//
	// MyHistoryEventSMSIntelligentFilter
	//
	static class MyHistoryEventSMSIntelligentFilter extends HistoryEventSMSIntelligentFilter{
		private int mUnSeen;
		
		int getUnSeen(){
			return mUnSeen;
		}

		@Override
		protected void reset(){
			super.reset();
			mUnSeen = 0;
		}
		
		@Override
		public boolean apply(NgnHistoryEvent event) {
			if(super.apply(event)){
				mUnSeen += event.isSeen() ? 0 : 1;
				return true;
			}
			return false;
		}
	}
}
