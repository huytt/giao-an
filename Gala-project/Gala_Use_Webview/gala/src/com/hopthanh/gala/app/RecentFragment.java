package com.hopthanh.gala.app;

import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Comparator;
import java.util.HashMap;
import java.util.SortedSet;
import java.util.TreeSet;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.object.CallLogClass;

import android.app.Activity;
import android.app.Fragment;
import android.content.ContentResolver;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.provider.CallLog;
import android.provider.ContactsContract;
import android.provider.ContactsContract.PhoneLookup;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView.FindListener;
import android.widget.ListView;

public class RecentFragment extends Fragment {
	private static final String TAG = "RecentFragment";
	private View mView = null;
	private Activity mActivity = null;
	
	@Override
	public void onAttach(Activity activity) {
		// TODO Auto-generated method stub
		super.onAttach(activity);
		mActivity = activity;
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.call_layout_recent, container, false);
		ListView lvRecent = (ListView) mView.findViewById(R.id.lvRecent);
		
		MultiLayoutContentListViewAdapter adapter = new MultiLayoutContentListViewAdapter(
				generateAdapter(getCallDetails(mActivity.getApplicationContext())), 
				mActivity.getApplicationContext());
		lvRecent.setAdapter(adapter);

//		Log.d(TAG, callLog);
		return mView;
//		return super.onCreateView(inflater, container, savedInstanceState);
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
	
	private ArrayList<Object> generateAdapter(HashMap<String, ArrayList<CallLogClass>> callDeatils) {
		ArrayList<Object> result = new ArrayList<Object>();
		
		// Sort Desc keys.
		Comparator<String> comparator = new Comparator<String>() {
			
			@Override
			public int compare(String lhs, String rhs) {
				// TODO Auto-generated method stub
				return rhs.compareTo(lhs);
			}
		};
		
		SortedSet<String> keys = new TreeSet<String>(comparator);
		keys.addAll(callDeatils.keySet());
		
		for(String key : keys) {
			result.add(key);
			for (CallLogClass item : callDeatils.get(key)) {
				result.add(item);
			}
		}
		return result;
	}
	
	private String getDisplayNameByPhNumber(HashMap<String, ArrayList<String>> contacts, String phNumber) {
		for(String key : contacts.keySet()) {
			if(((ArrayList<String>) contacts.get(key)).contains(phNumber)) {
				return key;
			}
		}
		return null;
	}
	
	private HashMap<String, ArrayList<CallLogClass>> getCallDetails(Context context) {
        HashMap<String, ArrayList<CallLogClass>> result = new HashMap<String, ArrayList<CallLogClass>>();
//        StringBuffer sb = new StringBuffer();
        Cursor managedCursor = context.getContentResolver().query(CallLog.Calls.CONTENT_URI, null,
                null, null, null);
        int number = managedCursor.getColumnIndex(CallLog.Calls.NUMBER);
        int type = managedCursor.getColumnIndex(CallLog.Calls.TYPE);
        int date = managedCursor.getColumnIndex(CallLog.Calls.DATE);
        int duration = managedCursor.getColumnIndex(CallLog.Calls.DURATION);
        HashMap<String, String> contact = getContacts(context);
        
//        sb.append("Call Details :");
        while (managedCursor.moveToNext()) {
        	String displayName = "Unsaved";
        	boolean isSaved = false;
            String phNumber = managedCursor.getString(number);
        	if (contact.containsKey(phNumber)) {
        		displayName = contact.get(phNumber);
        		isSaved = true;
        	}

            String callType = managedCursor.getString(type);
            String callDate = managedCursor.getString(date);
            Date callDayTime = new Date(Long.valueOf(callDate));
            SimpleDateFormat df = new SimpleDateFormat("MM/dd/yyyy");
            String keyDate = df.format(callDayTime);
            
            String callDuration = managedCursor.getString(duration);
            String dir = null;
            int dircode = Integer.parseInt(callType);
            switch (dircode) {
            case CallLog.Calls.OUTGOING_TYPE:
                dir = "OUTGOING";
                break;

            case CallLog.Calls.INCOMING_TYPE:
                dir = "INCOMING";
                break;

            case CallLog.Calls.MISSED_TYPE:
                dir = "MISSED";
                break;
            }
        	CallLogClass item = new CallLogClass(displayName, phNumber, callType, callDate, callDayTime, callDuration, dir, dircode, isSaved);
//            sb.append("\nPhone Number:--- " + phNumber + " \nCall Type:--- "
//                    + dir + " \nCall Date:--- " + callDayTime
//                    + " \nCall duration in sec :--- " + callDuration);
//            sb.append("\n----------------------------------");
        	
            if(!result.containsKey(keyDate)) {
            	ArrayList<CallLogClass> value = new ArrayList<CallLogClass>();
            	result.put(keyDate, value);
            }
            result.get(keyDate).add(item);
        }
        managedCursor.close();
        return result;

    }
	
	public HashMap<String, ArrayList<String>> fetchContacts(Context context) {
		HashMap<String, ArrayList<String>> result = new HashMap<String, ArrayList<String>>();

		String phoneNumber = null;
		String email = null;

		Uri CONTENT_URI = ContactsContract.Contacts.CONTENT_URI;
		String _ID = ContactsContract.Contacts._ID;
		String DISPLAY_NAME = ContactsContract.Contacts.DISPLAY_NAME;
		String HAS_PHONE_NUMBER = ContactsContract.Contacts.HAS_PHONE_NUMBER;

		Uri PhoneCONTENT_URI = ContactsContract.CommonDataKinds.Phone.CONTENT_URI;
		String Phone_CONTACT_ID = ContactsContract.CommonDataKinds.Phone.CONTACT_ID;
		String NUMBER = ContactsContract.CommonDataKinds.Phone.NUMBER;

//		Uri EmailCONTENT_URI =  ContactsContract.CommonDataKinds.Email.CONTENT_URI;
//		String EmailCONTACT_ID = ContactsContract.CommonDataKinds.Email.CONTACT_ID;
//		String DATA = ContactsContract.CommonDataKinds.Email.DATA;

		StringBuffer output = new StringBuffer();

		ContentResolver contentResolver = context.getContentResolver();

		Cursor cursor = contentResolver.query(CONTENT_URI, null,null, null, null);	

		// Loop for every contact in the phone
		if (cursor.getCount() > 0) {

			while (cursor.moveToNext()) {

				String contact_id = cursor.getString(cursor.getColumnIndex( _ID ));
				String name = cursor.getString(cursor.getColumnIndex( DISPLAY_NAME ));

				int hasPhoneNumber = Integer.parseInt(cursor.getString(cursor.getColumnIndex( HAS_PHONE_NUMBER )));

				if (hasPhoneNumber > 0) {

					output.append("\n First Name:" + name);
					ArrayList<String> arrPhNumber = new ArrayList<String>();

					// Query and loop for every phone number of the contact
					Cursor phoneCursor = contentResolver.query(PhoneCONTENT_URI, null, Phone_CONTACT_ID + " = ?", new String[] { contact_id }, null);

					while (phoneCursor.moveToNext()) {
						phoneNumber = phoneCursor.getString(phoneCursor.getColumnIndex(NUMBER));
						arrPhNumber.add(phoneNumber);
						output.append("\n Phone number:" + phoneNumber);
					}

					phoneCursor.close();
					
					result.put(name, arrPhNumber);

					// Query and loop for every email of the contact
//					Cursor emailCursor = contentResolver.query(EmailCONTENT_URI,	null, EmailCONTACT_ID+ " = ?", new String[] { contact_id }, null);
//
//					while (emailCursor.moveToNext()) {
//
//						email = emailCursor.getString(emailCursor.getColumnIndex(DATA));
//
//						output.append("\nEmail:" + email);
//
//					}
//					emailCursor.close();
				}
			}
		}
		return result;
	}
	
	private HashMap<String, String> getContacts(Context context) {
		HashMap<String, String> result = new HashMap<String, String>();
		
		Cursor contacts = context.getContentResolver().query(ContactsContract.CommonDataKinds.Phone.CONTENT_URI, null, null, null, null);
		int nameFieldColumnIndex = contacts.getColumnIndex(ContactsContract.CommonDataKinds.Phone.DISPLAY_NAME);
		int numberFieldColumnIndex = contacts.getColumnIndex(ContactsContract.CommonDataKinds.Phone.NUMBER);

		while(contacts.moveToNext()) {
			String number = contacts.getString(numberFieldColumnIndex);
			String name = contacts.getString(nameFieldColumnIndex);
			result.put(number, name);
		}

		contacts.close();
		
		return result;
	}
	
	private String getCallDetails1(Context context) {

        StringBuffer sb = new StringBuffer();
        Cursor managedCursor = context.getContentResolver().query(CallLog.Calls.CONTENT_URI, null,
                null, null, null);
        int number = managedCursor.getColumnIndex(CallLog.Calls.NUMBER);
        int type = managedCursor.getColumnIndex(CallLog.Calls.TYPE);
        int date = managedCursor.getColumnIndex(CallLog.Calls.DATE);
        int duration = managedCursor.getColumnIndex(CallLog.Calls.DURATION);
        sb.append("Call Details :");
        while (managedCursor.moveToNext()) {
            String phNumber = managedCursor.getString(number);
            String callType = managedCursor.getString(type);
            String callDate = managedCursor.getString(date);
            Date callDayTime = new Date(Long.valueOf(callDate));
            String callDuration = managedCursor.getString(duration);
            String dir = null;
            int dircode = Integer.parseInt(callType);
            switch (dircode) {
            case CallLog.Calls.OUTGOING_TYPE:
                dir = "OUTGOING";
                break;

            case CallLog.Calls.INCOMING_TYPE:
                dir = "INCOMING";
                break;

            case CallLog.Calls.MISSED_TYPE:
                dir = "MISSED";
                break;
            }
            sb.append("\nPhone Number:--- " + phNumber + " \nCall Type:--- "
                    + dir + " \nCall Date:--- " + callDayTime
                    + " \nCall duration in sec :--- " + callDuration);
            sb.append("\n----------------------------------");
        }
        managedCursor.close();
        return sb.toString();

    }
}
