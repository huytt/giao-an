package com.hopthanh.gala.adapter;

import java.util.List;
import java.util.Observable;
import java.util.Observer;

import org.doubango.ngn.media.NgnMediaType;
import org.doubango.ngn.sip.NgnMsrpSession;
import org.doubango.ngn.utils.NgnListUtils;
import org.doubango.ngn.utils.NgnPredicate;
import org.doubango.ngn.utils.NgnStringUtils;

import com.hopthanh.galagala.app.R;

import android.R.anim;
import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

public class ScreenChatQueueAdapter extends BaseAdapter implements Observer{ 
    private static final String TAG = "ScreenChatQueueAdapter";
	private List<NgnMsrpSession> mSessions;
    private final LayoutInflater mInflater;
    private final Handler mHandler;
    private final NgnPredicate<NgnMsrpSession> mFilter;
    
    public ScreenChatQueueAdapter(Context context) {
    	Log.e(TAG, "=========ScreenChatQueueAdapter============");
//    	android.os.Debug.waitForDebugger();
    	mHandler = new Handler();
        mInflater = LayoutInflater.from(context);
        NgnMsrpSession.getSessions().addObserver(this);
        mFilter = new NgnPredicate<NgnMsrpSession>(){
			@Override
			public boolean apply(NgnMsrpSession session) {
//				return session != null && NgnMediaType.isChat(session.getMediaType());
				return session != null;
			}
        };
        mSessions = NgnListUtils.filter(NgnMsrpSession.getSessions().values(), mFilter);
    }
    
    @Override
    public int getCount() {
    	Log.e(TAG, "=========getCount============");
        return mSessions == null ? 0 : mSessions.size();
    }
    
    @Override
    public Object getItem(int position) {
    	Log.e(TAG, "=========getItem============");
        return mSessions == null || mSessions.size() <= position ? null : mSessions.get(position);
    }
    
    @Override
    public long getItemId(int position) {
        return position;
    }
    
    @Override
    public void update(Observable observable, Object data) {
    	Log.e(TAG, "=========update============");
    	mSessions = NgnListUtils.filter(NgnMsrpSession.getSessions().values(), mFilter);
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
    public View getView(int position, View convertView, ViewGroup parent) {    
    	Log.e(TAG, "=========getView============");
        View view = convertView;
        NgnMsrpSession session;
        
        if (view == null) {
            view = mInflater.inflate(R.layout.screen_chat_queue_item, null);
        }
        session = (NgnMsrpSession)getItem(position);
        
        if(session != null){
            final TextView tvRemoteParty = (TextView) view.findViewById(R.id.screen_chat_queue_item_textView_remote);
            final TextView tvInfo = (TextView) view.findViewById(R.id.screen_chat_queue_item_textView_info);
			switch (session.getState()) {
				case INCOMING:
					tvInfo.setText("Incoming");
					break;
				case INPROGRESS:
					tvInfo.setText("In Progress");
					break;
				case INCALL:
					tvInfo.setText("Connected");
					break;
				case TERMINATED:
					tvInfo.setText("Terminated");
					break;
				default:
					tvInfo.setText(NgnStringUtils.emptyValue());
					break;
			}        
            
            final String remoteParty = session.getRemotePartyDisplayName();
            if(remoteParty != null){
                tvRemoteParty.setText(remoteParty);
            }
            else{
               tvRemoteParty.setText(NgnStringUtils.nullValue());
            }
        }
        
        return view;
    }
}
