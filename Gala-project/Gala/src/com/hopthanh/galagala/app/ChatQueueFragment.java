package com.hopthanh.galagala.app;

import org.doubango.ngn.sip.NgnMsrpSession;

import com.hopthanh.gala.adapter.ScreenChatQueueAdapter;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;

public class ChatQueueFragment extends Fragment{
	private static final String TAG = ChatQueueFragment.class.getCanonicalName();
	
	private ListView mListView;
    private ScreenChatQueueAdapter mAdapter;
    private View mView = null;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
    		Bundle savedInstanceState) {
    	// TODO Auto-generated method stub
    	mView = inflater.inflate(R.layout.screen_chat_queue, container, false);
        mListView = (ListView)mView.findViewById(R.id.screen_chat_queue_listView);
        mAdapter = new ScreenChatQueueAdapter(GalagalaDroid.getContext());
		mListView.setAdapter(mAdapter);
//		mListView.setOnItemClickListener(new OnItemClickListener() {
//
//			@Override
//			public void onItemClick(AdapterView<?> parent, View view,
//					int position, long id) {
//				// TODO Auto-generated method stub
//				final NgnMsrpSession session = (NgnMsrpSession)mAdapter.getItem(position);
//                if(session != null){
//                	startChat(session.getRemotePartyUri(), false);
//                }
//			}
//		});

    	return mView;
    }
}
