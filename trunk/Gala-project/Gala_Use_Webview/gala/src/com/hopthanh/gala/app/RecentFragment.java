package com.hopthanh.gala.app;

import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;

import com.hopthanh.gala.adapter.MultiLayoutContentListViewAdapter;
import com.hopthanh.object.CallLogClass;

import android.app.Activity;
import android.app.Fragment;
import android.content.Context;
import android.database.Cursor;
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
		
//		String callLog = getCallDetails(mActivity.getApplicationContext());
		SimpleDateFormat df = new SimpleDateFormat("MM/dd/yyyy");
		java.util.Date today = Calendar.getInstance().getTime();
		
		MultiLayoutContentListViewAdapter adapter = new MultiLayoutContentListViewAdapter(
				getCallDetails(mActivity.getApplicationContext()).get(df.format(today)), 
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
