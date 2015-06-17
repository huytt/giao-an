package com.hopthanh.gala.web_api_util;

public interface ITaskLoadJsonDataListener<T> {
	public void executeTask();
	public T parserJson();
	public void onTaskComplete(T result);
}
