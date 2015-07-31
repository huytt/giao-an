package com.hopthanh.galagala.app;

import org.doubango.ngn.model.NgnHistoryEvent;
import org.doubango.ngn.services.INgnContactService;
import org.doubango.ngn.services.INgnHistoryService;
import org.doubango.ngn.sip.NgnMsrpSession;

import com.hopthanh.gala.adapter.ScreenChatQueueAdapter;
import com.hopthanh.gala.adapter.ScreenTabMessagesAdapter;
import com.hopthanh.galagala.sip.SipSingleton;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;

public class MessageTabFragment extends AbstractLayoutFragment<Object>{
	private static final String TAG = MessageTabFragment.class.getCanonicalName();
	
	private ListView mListView;
    private ScreenTabMessagesAdapter mAdapter;
	private final INgnHistoryService mHistoryService = SipSingleton.getInstance().getEngine().getHistoryService();
	private final INgnContactService mContactService = SipSingleton.getInstance().getEngine().getContactService();


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
    		Bundle savedInstanceState) {
    	// TODO Auto-generated method stub
    	mView = inflater.inflate(R.layout.screen_tab_messages, container, false);
		Log.e(TAG,"onCreate()");
		
		mAdapter = new ScreenTabMessagesAdapter(GalagalaDroid.getContext(), mHistoryService, mContactService);
		mListView = (ListView) mView.findViewById(R.id.screen_tab_messages_listView);
		mListView.setAdapter(mAdapter);
		mListView.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				// TODO Auto-generated method stub
				final NgnHistoryEvent event = (NgnHistoryEvent)parent.getItemAtPosition(position);
				if(event != null){
					((MessageActivityListener)mListener).notifyUpdateFragment(MessageActivity.MESSAGE_FRAGMENT);
				}
			}
		});

    	return mView;
    }
}
