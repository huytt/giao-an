package com.hopthanh.galagala.app;

import com.gala.sip.SipCall;
import com.gala.sip.SipEngine;
import com.hopthanh.gala.utils.LayoutCallUtils;

import android.os.Bundle;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageView;

public class InCallActivity extends SipCall {
	
	private static final String TAG = "InCallActivity";
	@Override
	public void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.call_layout_incall_audio);
		loadCallAction();
		
		ImageView imgCancel = (ImageView) findViewById(R.id.imgCancel);
		
		imgCancel.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				
			}
		});
	}
	
	private void loadCallAction(){
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_mute), R.drawable.icon_call_action_mute_80, LayoutCallUtils.TAG_AUDIO_CALL, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_chat), R.drawable.icon_call_action_chat_80, LayoutCallUtils.TAG_CHAT, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_speak), R.drawable.icon_call_action_speak_80, LayoutCallUtils.TAG_SPEAK, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_video_call), R.drawable.icon_call_action_video_call_80, LayoutCallUtils.TAG_VIDEO_CALL, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_call_transfer), R.drawable.icon_call_action_call_transfer_80, LayoutCallUtils.TAG_CALL_TRANSFER, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(findViewById(R.id.view_call_action_contact), R.drawable.icon_call_action_contact_80, LayoutCallUtils.TAG_CONTACT, mOnKeyboardClickListener);
	}
	
	private View.OnClickListener mOnKeyboardClickListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			switch(Integer.parseInt(v.getTag().toString())) {
			case LayoutCallUtils.TAG_AUDIO_CALL:
				break;
			case LayoutCallUtils.TAG_CHAT:
				break;
			case LayoutCallUtils.TAG_SPEAK:
				break;
			case LayoutCallUtils.TAG_VIDEO_CALL:
				break;
			case LayoutCallUtils.TAG_CALL_TRANSFER:
				break;
			case LayoutCallUtils.TAG_CONTACT:
				break;
			}
		}
	};

}
