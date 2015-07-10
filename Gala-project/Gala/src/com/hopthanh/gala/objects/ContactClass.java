package com.hopthanh.gala.objects;

import java.util.ArrayList;

public class ContactClass {
    private String displayName;
    private ArrayList<String> phNumber;
    
    public ContactClass() {
    	this.displayName = null;
        this.phNumber = null;
    }
    
    public ContactClass(
    		String displayName,
    		ArrayList<String> phNumber) {
    	this.displayName = displayName;
        this.phNumber = phNumber;
    }
    
	public ArrayList<String> getPhNumber() {
		return phNumber;
	}
	public void setPhNumber(ArrayList<String> phNumber) {
		this.phNumber = phNumber;
	}

	public String getDisplayName() {
		return displayName;
	}

	public void setDisplayName(String displayName) {
		this.displayName = displayName;
	}
}
