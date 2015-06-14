package com.hopthanh.gala.app;

import java.sql.Date;

import android.app.Fragment;
import android.content.Context;
import android.database.Cursor;
import android.os.Bundle;
import android.provider.CallLog;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

public class RecentFragment extends Fragment {
	private static final String TAG = "RecentFragment";
	private View mView = null;

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container,
			Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		mView = inflater.inflate(R.layout.call_layout_recent, container, false);
		String callLog = getCallDetails(inflater.getContext());
		Log.d(TAG, callLog);
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
	
	private String getCallDetails(Context context) {

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
