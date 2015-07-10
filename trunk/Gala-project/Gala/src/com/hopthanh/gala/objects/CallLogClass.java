package com.hopthanh.gala.objects;

import java.sql.Date;

public class CallLogClass {
    private String phNumber;
    private String displayName;
    private String callType;
    private String callDate;
    private Date callDayTime;
    private String callDuration;
    private String dirName;
    private int dircode;
    private boolean isSaved;
    
    public CallLogClass() {
    	this.setDisplayName(null);
        this.phNumber = null;
        this.callType = null;
        this.callDate = null;
        this.callDayTime = null;
        this.callDuration = null;
        this.dirName = null;
        this.dircode = -1;
        this.isSaved = false;
    }
    
    public CallLogClass(
    		String displayName,
    		String phNumber,
    		String callType,
    		String callDate,
    		Date callDayTime,
    		String callDuration,
    		String dirName,
    		int dirCode,
    		boolean isSave) {
    	this.displayName = displayName;
        this.phNumber = phNumber;
        this.callType = callType;
        this.callDate = callDate;
        this.callDayTime = callDayTime;
        this.callDuration = callDuration;
        this.dirName = dirName;
        this.dircode = dirCode;
        this.isSaved = isSave;
    }
    
	public String getPhNumber() {
		return phNumber;
	}
	public void setPhNumber(String phNumber) {
		this.phNumber = phNumber;
	}
	public String getCallType() {
		return callType;
	}
	public void setCallType(String callType) {
		this.callType = callType;
	}
	public String getCallDate() {
		return callDate;
	}
	public void setCallDate(String callDate) {
		this.callDate = callDate;
	}
	public Date getCallDayTime() {
		return callDayTime;
	}
	public void setCallDayTime(Date callDayTime) {
		this.callDayTime = callDayTime;
	}
	public String getCallDuration() {
		return callDuration;
	}
	public void setCallDuration(String callDuration) {
		this.callDuration = callDuration;
	}
	public String getDirName() {
		return dirName;
	}
	public void setDirName(String dirName) {
		this.dirName = dirName;
	}
	public int getDircode() {
		return dircode;
	}
	public void setDircode(int dircode) {
		this.dircode = dircode;
	}

	public String getDisplayName() {
		return displayName;
	}

	public void setDisplayName(String displayName) {
		this.displayName = displayName;
	}

	public boolean isSaved() {
		return isSaved;
	}

	public void setSaved(boolean isSaved) {
		this.isSaved = isSaved;
	}

}
