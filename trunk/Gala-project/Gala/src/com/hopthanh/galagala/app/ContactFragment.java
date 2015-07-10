package com.hopthanh.galagala.app;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.SortedSet;
import java.util.TreeSet;

import com.hopthanh.gala.adapter.ContentContactListViewAdapter;
import com.hopthanh.gala.objects.ContactClass;

import android.app.Activity;
import android.app.Fragment;
import android.content.Context;
import android.database.Cursor;
import android.os.Bundle;
import android.provider.ContactsContract;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ListView;

public class ContactFragment extends Fragment {
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
		mView = inflater.inflate(R.layout.call_layout_contact, container, false);
		final ListView lvContact = (ListView) mView.findViewById(R.id.lvContact);
		EditText inputSearch = (EditText) mView.findViewById(R.id.inputSearch);
		
		ContentContactListViewAdapter adapter = new ContentContactListViewAdapter(
				getContacts(mActivity.getApplicationContext()), 
				mActivity.getApplicationContext());
		
		lvContact.setAdapter(adapter);
		
		inputSearch.addTextChangedListener(new TextWatcher() {
		     
		    @Override
		    public void onTextChanged(CharSequence cs, int arg1, int arg2, int arg3) {
		        // When user changed the Text
		    	((ContentContactListViewAdapter) lvContact.getAdapter()).filter(cs.toString());
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
	
	private void SortByDisplayName(ArrayList<ContactClass> contacts) {
		// Sort Desc keys.
		Comparator<ContactClass> comparator = new Comparator<ContactClass>() {
			
			@Override
			public int compare(ContactClass lhs, ContactClass rhs) {
				// TODO Auto-generated method stub
				return lhs.getDisplayName().compareTo(rhs.getDisplayName());
			}
		};
			
		
		SortedSet<ContactClass> contactSort = new TreeSet<ContactClass>(comparator);
		contactSort.addAll(contacts);
	
	}
	
	private ArrayList<ContactClass> getContacts(Context context) {
		ArrayList<ContactClass> result = new ArrayList<ContactClass>();
		
		Cursor contacts = context.getContentResolver().query(ContactsContract.CommonDataKinds.Phone.CONTENT_URI, null, null, null, null);
		int nameFieldColumnIndex = contacts.getColumnIndex(ContactsContract.CommonDataKinds.Phone.DISPLAY_NAME);
		int numberFieldColumnIndex = contacts.getColumnIndex(ContactsContract.CommonDataKinds.Phone.NUMBER);

		while(contacts.moveToNext()) {
			ContactClass item = new ContactClass();
			String number = contacts.getString(numberFieldColumnIndex);
			ArrayList<String> arrPhNumbers = new ArrayList<String>();
			arrPhNumbers.add(number);
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

}
