package com.hopthanh.galagala.app;

import org.doubango.ngn.media.NgnMediaType;
import org.doubango.ngn.model.NgnHistorySMSEvent;
import org.doubango.ngn.model.NgnHistoryEvent.StatusType;
import org.doubango.ngn.services.INgnHistoryService;
import org.doubango.ngn.services.INgnSipService;
import org.doubango.ngn.sip.NgnMessagingSession;
import org.doubango.ngn.sip.NgnMsrpSession;
import org.doubango.ngn.utils.NgnPredicate;
import org.doubango.ngn.utils.NgnStringUtils;
import org.doubango.ngn.utils.NgnUriUtils;

import com.hopthanh.gala.adapter.ScreenChatAdapter;
import com.hopthanh.galagala.sip.SipSingleton;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ListView;

public class MessageFragment extends AbstractLayoutFragment<Object>{
	
	private static final String TAG = "ChatFragment";
	private final INgnHistoryService mHistorytService = SipSingleton.getInstance().getEngine().getHistoryService();
	private final INgnSipService mSipService = SipSingleton.getInstance().getSipService();
	private NgnMsrpSession mSession;
	private NgnMediaType mMediaType = NgnMediaType.None;
	private ScreenChatAdapter mAdapter;
	private EditText mEtCompose;
	private ListView mLvHistoy;
	private ImageButton mBtSend;
	private static String sRemoteParty = "huyphuong0803";
	private InputMethodManager mInputMethodManager;
	
	@Override
	public void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.screen_chat, container, false);
		mInputMethodManager = (InputMethodManager) GalagalaDroid.getContext().getSystemService(Context.INPUT_METHOD_SERVICE);
		
		mEtCompose = (EditText) mView.findViewById(R.id.screen_chat_editText_compose);
		mBtSend =(ImageButton) mView.findViewById(R.id.screen_chat_button_send);
		mLvHistoy = (ListView) mView.findViewById(R.id.screen_chat_listView);
		
		mAdapter = new ScreenChatAdapter(GalagalaDroid.getContext(), mHistorytService, sRemoteParty);
		mLvHistoy.setAdapter(mAdapter);
		mLvHistoy.setTranscriptMode(ListView.TRANSCRIPT_MODE_ALWAYS_SCROLL);
		mLvHistoy.setStackFromBottom(true);

		mBtSend.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if(mSipService.isRegistered() && !NgnStringUtils.isNullOrEmpty(mEtCompose.getText().toString())){
					sendMessage();
				}
				
				if(mInputMethodManager != null){
					mInputMethodManager.hideSoftInputFromWindow(mEtCompose.getWindowToken(), 0);
				}
				
				mEtCompose.setLines(2);
			}
		});
		
		mEtCompose.addTextChangedListener(new TextWatcher() {
			@Override
			public void onTextChanged(CharSequence s, int start, int before, int count) {
				mBtSend.setEnabled(!NgnStringUtils.isNullOrEmpty(mEtCompose.getText().toString()));
			}
			@Override
			public void beforeTextChanged(CharSequence s, int start, int count, int after) { }
			@Override
			public void afterTextChanged(Editable s) { }
		});
		
		// BugFix: http://code.google.com/p/android/issues/detail?id=7189
		mEtCompose.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                switch (event.getAction()) {
                    case MotionEvent.ACTION_DOWN:
                    case MotionEvent.ACTION_UP:
                        if (!v.hasFocus()) {
                            v.requestFocus();
                        }
                        break;
                }
                mEtCompose.setLines(5);
                return false;
            }
        });
		
		return mView;
	}
	
	@Override
	public void onResume() {
		super.onResume();
		if(mMediaType != NgnMediaType.None){
			initialize(mMediaType);
		}
		mAdapter.refresh();
	}
	
	@Override
	public void onPause(){
		if(mInputMethodManager != null){
			mInputMethodManager.hideSoftInputFromWindow(mEtCompose.getWindowToken(), 0);
		}
		super.onPause();
	}
	
	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if(mSession != null){
			mSession.decRef();
			mSession = null;
		}

		if (mView != null && mView.getParent() != null) {
            ((ViewGroup) mView.getParent()).removeView(mView);
            mView = null;
        }
		super.onDestroyView();
	}
	
	private void initialize(NgnMediaType mediaType){
		final boolean bIsNewScreen = mMediaType == NgnMediaType.None;
		mMediaType = mediaType;
		if(mMediaType == NgnMediaType.Chat){
			final String validUri = NgnUriUtils.makeValidSipUri(sRemoteParty);
			if(!NgnStringUtils.isNullOrEmpty(validUri)){
				mSession = NgnMsrpSession.getSession(new NgnPredicate<NgnMsrpSession>() {
					@Override
					public boolean apply(NgnMsrpSession session) {
						if(session != null && session.getMediaType() == NgnMediaType.Chat){
							return NgnStringUtils.equals(session.getRemotePartyUri(), validUri, false);
						}
						return false;
					}
				});
				if(mSession == null){
					if((mSession = NgnMsrpSession.createOutgoingSession(mSipService.getSipStack(), NgnMediaType.Chat, validUri)) == null){
						Log.e(TAG, "Failed to create MSRP session");
						return;
					}
				}
				if(bIsNewScreen && mSession != null){
					mSession.incRef();
				}
			}
			else{
				Log.e(TAG, "makeValidSipUri("+sRemoteParty+") has failed");
				return;
			}
		}
	}
	
	private boolean sendMessage(){
		boolean ret = false;
		final String content = mEtCompose.getText().toString();
		final NgnHistorySMSEvent e = new NgnHistorySMSEvent(sRemoteParty, StatusType.Outgoing);
		e.setContent(content);
		
		if(!mSipService.isRegistered()){
			Log.e(TAG, "Not registered");
			return false;
		}
		if(mMediaType == NgnMediaType.Chat){
			if(mSession != null){
				ret = mSession.SendMessage(content);
			}else{
				Log.e(TAG,"MSRP session is null");
				return false;
			}
		}
		else{
			final String remotePartyUri = NgnUriUtils.makeValidSipUri(sRemoteParty);
			final NgnMessagingSession imSession = NgnMessagingSession.createOutgoingSession(mSipService.getSipStack(), 
					remotePartyUri);
			if(!(ret = imSession.sendTextMessage(mEtCompose.getText().toString()))){
				e.setStatus(StatusType.Failed);
			}
			NgnMessagingSession.releaseSession(imSession);
		}
		
		mHistorytService.addEvent(e);
		mEtCompose.setText(NgnStringUtils.emptyValue());
		return ret;
	}
}
