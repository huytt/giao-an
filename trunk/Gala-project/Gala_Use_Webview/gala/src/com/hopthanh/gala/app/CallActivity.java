package com.hopthanh.gala.app;

import android.app.Activity;
import android.os.Bundle;
import android.widget.GridLayout;
import android.widget.ImageButton;

public class CallActivity extends Activity {
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.numberic_keypad_layout);
		
//		GridLayout glNumbericKey = (GridLayout) findViewById(R.id.glNumbericKey);
		
		ImageButton ibtnKeypad = (ImageButton) findViewById(R.id.ibtnKeypad);
		ibtnKeypad.requestFocus();
	}
}
