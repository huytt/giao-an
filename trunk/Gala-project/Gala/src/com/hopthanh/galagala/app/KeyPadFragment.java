package com.hopthanh.galagala.app;

import java.util.ArrayList;
import java.util.Locale;
import java.util.Timer;
import java.util.TimerTask;

import com.hopthanh.gala.objects.ContactClass;
import com.hopthanh.gala.utils.KeypadUtils;

import android.app.Fragment;
import android.content.Context;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.text.Editable;
import android.text.InputType;
import android.text.TextWatcher;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.TextView;

public class KeyPadFragment extends Fragment {
	private View mView = null;
	private EditText inputNumber = null;
	private TextView tvDisplayName = null;
	private TextView tvphNumber = null;
	private ImageButton ibtnNumAdd = null;
	private ImageButton ibtnNumDel = null;
	private ArrayList<ContactClass> mContacts = null;

	private void loadKeyboard(View view){
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_0), "0", "+", KeypadUtils.TAG_0, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_1), "1", "", KeypadUtils.TAG_1, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_2), "2", "ABC", KeypadUtils.TAG_2, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_3), "3", "DEF", KeypadUtils.TAG_3, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_4), "4", "GHI", KeypadUtils.TAG_4, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_5), "5", "JKL", KeypadUtils.TAG_5, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_6), "6", "MNO", KeypadUtils.TAG_6, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_7), "7", "PQRS", KeypadUtils.TAG_7, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_8), "8", "TUV", KeypadUtils.TAG_8, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_9), "9", "WXYZ", KeypadUtils.TAG_9, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_star), "*", "", KeypadUtils.TAG_STAR, mOnKeyboardClickListener);
		KeypadUtils.setKeypadTextButton(view.findViewById(R.id.view_keypad_buttons_sharp), "#", "", KeypadUtils.TAG_SHARP, mOnKeyboardClickListener);
	}

	private View.OnClickListener mOnKeyboardClickListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {
			int tag = Integer.parseInt(v.getTag().toString());
			String textToAppend = (tag == KeypadUtils.TAG_STAR ? "*" : (tag == KeypadUtils.TAG_SHARP ? "#" : String.valueOf(tag)));
			
			int selStart = inputNumber.getSelectionStart();
			StringBuffer sb = new StringBuffer(inputNumber.getText().toString());
			sb.insert(selStart, textToAppend);
			inputNumber.setText(sb.toString());
			inputNumber.setSelection(selStart+1);
			
			ibtnNumAdd.setVisibility(View.VISIBLE);
			ibtnNumDel.setVisibility(View.VISIBLE);
		}
	};
	
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.call_layout_numberic_keypad_view, container, false);
		
		inputNumber = (EditText) mView.findViewById(R.id.inputNumber);
		tvDisplayName = (TextView) mView.findViewById(R.id.tvDisplayName);
		tvphNumber = (TextView) mView.findViewById(R.id.tvphNumber);
		ibtnNumAdd = (ImageButton) mView.findViewById(R.id.ibtnNumAdd);
		ibtnNumDel = (ImageButton) mView.findViewById(R.id.ibtnNumDel);
		
		LinearLayout lnSearchResult = (LinearLayout) mView.findViewById(R.id.lnSearchResult);
		
		Typeface customFont = Typeface.createFromAsset(getActivity().getApplicationContext().getAssets() , "fonts/helveticaneuelight.ttf");
		inputNumber.setTypeface(customFont);
		tvDisplayName.setTypeface(customFont);
		
		loadKeyboard(mView);
		
		inputNumber.setInputType(InputType.TYPE_NULL);
		inputNumber.setFocusable(false);
		inputNumber.setFocusableInTouchMode(false);

		ibtnNumDel.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub

				String number = inputNumber.getText().toString();
				final int selStart = inputNumber.getSelectionStart();
				if(selStart >0){
					final StringBuffer sb = new StringBuffer(number);
					sb.delete(selStart-1, selStart);
					inputNumber.setText(sb.toString());
					inputNumber.setSelection(selStart-1);
					
					if(sb.toString().isEmpty()) {
						ibtnNumAdd.setVisibility(View.GONE);
						ibtnNumDel.setVisibility(View.GONE);
					}
				}
			}
		});
		
		ibtnNumDel.setOnLongClickListener(new View.OnLongClickListener() {
			@Override
			public boolean onLongClick(View v) {
				inputNumber.getText().clear();
				ibtnNumAdd.setVisibility(View.GONE);
				ibtnNumDel.setVisibility(View.GONE);
				return true;
			}
		});
		
		mView.findViewById(R.id.view_keypad_buttons_0).setOnLongClickListener(new View.OnLongClickListener() {
			@Override
			public boolean onLongClick(View v) {
				inputNumber.append("+");
				return true;
			}
		});
		
		mContacts = KeypadUtils.getContacts(getActivity().getApplicationContext());
		inputNumber.addTextChangedListener(new TextWatcher() {
			
			@Override
			public void onTextChanged(CharSequence s, int start, int before, int count) {
				// TODO Auto-generated method stub
				ContactClass contact = filter(mContacts, s.toString());
				if(contact != null) {
					String number = "";
					for(String phNumber : contact.getPhNumber()) {
						if(phNumber.contains(s.toString())) {
							number = phNumber;
							break;
						}
					}
					tvDisplayName.setText(contact.getDisplayName());
					tvphNumber.setText(number);
				} else {
					tvDisplayName.setText("");
					tvphNumber.setText("");
				}
			}
			
			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void afterTextChanged(Editable s) {
				// TODO Auto-generated method stub
				
			}
		});
		
		lnSearchResult.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				String textToAppend = tvphNumber.getText().toString();
				inputNumber.setText(textToAppend);
				inputNumber.setSelection(textToAppend.length());
			}
		});
		return mView;
//		return super.onCreateView(inflater, container, savedInstanceState);
	}

	private ContactClass filter(ArrayList<ContactClass> contacts, String charText) {
		charText = charText.toLowerCase(Locale.getDefault());
		if (charText.length() == 0) {
			return null;
		}
		
		for (ContactClass contact : contacts) 
		{
			for(String phNumber : contact.getPhNumber()) {
				if (phNumber.toLowerCase(Locale.getDefault()).contains(charText)) 
				{
					return contact;
				}
			}
		}
		
		return null;
	}

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
