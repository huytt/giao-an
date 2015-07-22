package com.hopthanh.galagala.sip;

import org.doubango.ngn.sip.NgnAVSession;

import com.hopthanh.gala.utils.LayoutCallUtils;
import com.hopthanh.galagala.app.AbstractLayoutFragment;
import com.hopthanh.galagala.app.R;
import com.hopthanh.galagala.app.R.drawable;
import com.hopthanh.galagala.app.R.id;
import com.hopthanh.galagala.app.R.layout;

import android.app.Activity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.ImageView;

public class OutComingAudioCallFrament extends AbstractLayoutFragment<Object>{
	private static final String TAG = "OutComingAudioCallFrament";
	private Activity mActivity = null;

	public OutComingAudioCallFrament() {
		super();
	}
	
	@Override
	public void onAttach(Activity activity) {
		// TODO Auto-generated method stub
		super.onAttach(activity);
		mActivity = activity;
	}
	
	@Override
	public void onDetach() {
		// TODO Auto-generated method stub
		super.onDetach();
		mActivity = null;
	}
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.call_layout_out_coming_audio, container, false);
		
		loadCallAction(mView);
		
		ImageView imgCancel = (ImageView) mView.findViewById(R.id.imgCancel);
		
		imgCancel.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				SipSingleton.getInstance().hangUpCall(InCallActivity.getInstance().getSession());
				mActivity.finish();
			}
		});

		return mView;
	}
	
	private void loadCallAction(View view){
		LayoutCallUtils.setCallActioinImageButton(view.findViewById(R.id.view_call_action_mute), R.drawable.icon_call_action_mute_80, LayoutCallUtils.TAG_AUDIO_CALL, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(view.findViewById(R.id.view_call_action_chat), R.drawable.icon_call_action_chat_80, LayoutCallUtils.TAG_CHAT, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(view.findViewById(R.id.view_call_action_speak), R.drawable.icon_call_action_speak_80, LayoutCallUtils.TAG_SPEAK, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(view.findViewById(R.id.view_call_action_video_call), R.drawable.icon_call_action_video_call_80, LayoutCallUtils.TAG_VIDEO_CALL, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(view.findViewById(R.id.view_call_action_call_transfer), R.drawable.icon_call_action_call_transfer_80, LayoutCallUtils.TAG_CALL_TRANSFER, mOnKeyboardClickListener);
		LayoutCallUtils.setCallActioinImageButton(view.findViewById(R.id.view_call_action_contact), R.drawable.icon_call_action_contact_80, LayoutCallUtils.TAG_CONTACT, mOnKeyboardClickListener);
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

	@Override
	public void onDestroyView() {
		// TODO Auto-generated method stub
		if (mView != null && mView.getParent() != null) {
            ((ViewGroup) mView.getParent()).removeView(mView);
            mView = null;
            System.gc();
        }
		super.onDestroyView();
	}
}
