/* Copyright (C) 2010-2011, Mamadou Diop.
*  Copyright (C) 2011, Doubango Telecom.
*
* Contact: Mamadou Diop <diopmamadou(at)doubango(dot)org>
*	
* This file is part of imsdroid Project (http://code.google.com/p/imsdroid)
*
* imsdroid is free software: you can redistribute it and/or modify it under the terms of 
* the GNU General Public License as published by the Free Software Foundation, either version 3 
* of the License, or (at your option) any later version.
*	
* imsdroid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
* See the GNU General Public License for more details.
*	
* You should have received a copy of the GNU General Public License along 
* with this program; if not, write to the Free Software Foundation, Inc., 
* 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
*/
package com.hopthanh.gala.utils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;

import com.hopthanh.gala.objects.ContactClass;
import com.hopthanh.galagala.app.R;

import android.app.Activity;
import android.content.Context;
import android.database.Cursor;
import android.graphics.Typeface;
import android.provider.ContactsContract;
import android.util.TypedValue;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

public class LayoutCallUtils {
	public static final int TAG_0 = 0;
	public static final int TAG_1 = 1;
	public static final int TAG_2 = 2;
	public static final int TAG_3 = 3;
	public static final int TAG_4 = 4;
	public static final int TAG_5 = 5;
	public static final int TAG_6 = 6;
	public static final int TAG_7 = 7;
	public static final int TAG_8 = 8;
	public static final int TAG_9 = 9;
	public static final int TAG_STAR = 10;
	public static final int TAG_SHARP = 11;
	public static final int TAG_CHAT = 12;
	public static final int TAG_AUDIO_CALL = 13;
	public static final int TAG_VIDEO_CALL = 14;
	public static final int TAG_DELETE = 15;
	public static final int TAG_SPEAK = 16;
	public static final int TAG_CONTACT = 17;
	public static final int TAG_CALL_TRANSFER = 18;
	
	public static void setKeypadTextButton(View view, String num, String text, int tag, View.OnClickListener listener){
		view.setTag(tag);
		view.setOnClickListener(listener);
		TextView tvNum = (TextView)view.findViewById(R.id.view_keypad_buttons_text_textView_num);
		TextView tvText = (TextView)view.findViewById(R.id.view_keypad_buttons_text_textView_text);
		
		Typeface customFont = Typeface.createFromAsset(view.getContext().getAssets() , "fonts/helveticaneuelight.ttf");
		tvNum.setTypeface(customFont);
		tvNum.setText(num);
		
		if(tag == TAG_STAR) {
			tvNum.setTextSize(TypedValue.COMPLEX_UNIT_SP, 30);
		}
		
		if(text.isEmpty()) {
			tvText.setVisibility(View.GONE);
		} else {
			tvText.setTypeface(customFont);
			tvText.setText(text);
		}
		
	}
	
	public static void setKeypadTextButton(Activity parent, int viewId, String num, String text, int tag, View.OnClickListener listener){
		setKeypadTextButton(parent.findViewById(viewId), num, text, tag, listener);
	}
	
	public static void setKeypadTextButton(View parent, int viewId, String num, String text, int tag, View.OnClickListener listener){
		setKeypadTextButton(parent.findViewById(viewId), num, text, tag, listener);
	}
	
	public static ArrayList<ContactClass> getContacts(Context context) {
		ArrayList<ContactClass> result = new ArrayList<ContactClass>();
		
		Cursor contacts = context.getContentResolver().query(ContactsContract.CommonDataKinds.Phone.CONTENT_URI, null, null, null, null);
		int nameFieldColumnIndex = contacts.getColumnIndex(ContactsContract.CommonDataKinds.Phone.DISPLAY_NAME);
		int numberFieldColumnIndex = contacts.getColumnIndex(ContactsContract.CommonDataKinds.Phone.NUMBER);

		while(contacts.moveToNext()) {
			ContactClass item = new ContactClass();
			String number = contacts.getString(numberFieldColumnIndex);
			ArrayList<String> arrPhNumbers = new ArrayList<String>();
			arrPhNumbers.add(number.replaceAll("\\s",""));
			item.setDisplayName(contacts.getString(nameFieldColumnIndex));
			item.setPhNumber(arrPhNumbers);
			result.add(item);
		}

		contacts.close();
		
		Collections.sort(result, new Comparator<ContactClass>() {
			@Override
			public int compare(ContactClass lhs, ContactClass rhs) {
				// TODO Auto-generated method stub
				return lhs.getDisplayName().compareTo(rhs.getDisplayName());
			}
		});
		
		return result;
	}

	public static void setCallActioinImageButton(View view, int imageId, int tag, View.OnClickListener listener){
//		View view = parent.findViewById(viewId);
		view.setTag(tag);
		view.setOnClickListener(listener);
		((ImageView)view.findViewById(R.id.call_action_button_imageView)).setImageResource(imageId);
	}
}