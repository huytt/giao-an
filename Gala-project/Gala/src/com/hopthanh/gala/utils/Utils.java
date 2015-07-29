package com.hopthanh.gala.utils;

import java.io.File;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import com.hopthanh.galagala.app.GalagalaDroid;

import android.app.ActivityManager;
import android.app.ActivityManager.RunningServiceInfo;
import android.app.AlertDialog;
import android.content.Context;
import android.graphics.Point;
import android.net.ConnectivityManager;
import android.view.Display;
import android.view.WindowManager;
import android.widget.Toast;

public class Utils {

	public static final String XONE_SERVER = "http://galagala.vn:8888/";
	public static final String XONE_SERVER_WEB = "http://galagala.vn:88";
	
	public static final int MAX_STORE_ON_GRID = 6;
	
	private Context _context;

	// constructor
	public Utils(Context context) {
		this._context = context;
	}

	/*
	 * Reading file paths from SDCard
	 */
	public ArrayList<String> getFilePaths() {
		ArrayList<String> filePaths = new ArrayList<String>();

		String fname = android.os.Environment.getExternalStorageDirectory()
				+ File.separator + AppConstant.PHOTO_ALBUM;
		File directory = new File(fname);

		// check for directory
		if (directory.isDirectory()) {
			// getting list of file paths
			File[] listFiles = directory.listFiles();

			// Check for count
			if (listFiles.length > 0) {

				// loop through all files
				for (int i = 0; i < listFiles.length; i++) {

					// get file path
					String filePath = listFiles[i].getAbsolutePath();

					// check for supported file extension
					if (IsSupportedFile(filePath)) {
						// Add image path to array list
						filePaths.add(filePath);
					}
				}
			} else {
				// image directory is empty
				Toast.makeText(
						_context,
						AppConstant.PHOTO_ALBUM
								+ " is empty. Please load some images in it !",
						Toast.LENGTH_LONG).show();
			}

		} else {
			AlertDialog.Builder alert = new AlertDialog.Builder(_context);
			//alert = new AlertDialog.Builder(_context);
			alert.setTitle("Error!");
			alert.setMessage(AppConstant.PHOTO_ALBUM
					+ " directory path is not valid! Please set the image directory name AppConstant.java class");
			alert.setPositiveButton("OK", null);
			alert.show();
		}

		return filePaths;
	}

	public static void showAlert(String message, Context context) {
		AlertDialog.Builder alert = new AlertDialog.Builder(context);
		//alert = new AlertDialog.Builder(_context);
		alert.setTitle("Error!");
		alert.setMessage(message);
		alert.setPositiveButton("OK", null);
		alert.show();
	}
	/*
	 * Check supported file extensions
	 * 
	 * @returns boolean
	 */
	private boolean IsSupportedFile(String filePath) {
		String ext = filePath.substring((filePath.lastIndexOf(".") + 1),
				filePath.length());

		if (AppConstant.FILE_EXTN
				.contains(ext.toLowerCase(Locale.getDefault())))
			return true;
		else
			return false;

	}

	public static boolean checkNetwork(Context context) {
		ConnectivityManager manager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);

		//For 3G check
		boolean is3g = manager.getNetworkInfo(ConnectivityManager.TYPE_MOBILE)
		            .isConnectedOrConnecting();
		//For WiFi Check
		boolean isWifi = manager.getNetworkInfo(ConnectivityManager.TYPE_WIFI)
		            .isConnectedOrConnecting();

		if (!is3g && !isWifi) 
		{ 
//			new Handler(Looper.getMainLooper()).post(new Runnable() {
//				public void run() {
//					Toast.makeText(context, R.string.OSD_network_disconnect,Toast.LENGTH_LONG).show();
//				}
//			});
			return false;
		}
		return true;
	}
	/*
	 * getting screen width
	 */
	@SuppressWarnings("deprecation")
	public int getScreenWidth() {
		int columnWidth;
		WindowManager wm = (WindowManager) _context
				.getSystemService(Context.WINDOW_SERVICE);
		Display display = wm.getDefaultDisplay();

		final Point point = new Point();
		try {
			display.getSize(point);
		} catch (java.lang.NoSuchMethodError ignore) { // Older device
			point.x = display.getWidth();
			point.y = display.getHeight();
		}
		columnWidth = point.x;
		return columnWidth;
	}
	
	public static Date getToday(){
	     return new Date(System.currentTimeMillis());
	}
	
	public static Date getYesterday(){
	     return new Date(System.currentTimeMillis()-24*60*60*1000);
	}
	
	public static Date getPreviousWeekDate(){
	     return new Date(System.currentTimeMillis()-7*24*60*60*1000);
	}
	
	public static Date getTomorrow(){
	     return new Date(System.currentTimeMillis()+24*60*60*1000);
	}
	
	public static boolean isServiceRunning(String serviceClassName){
        final ActivityManager activityManager = (ActivityManager)GalagalaDroid.getContext().getSystemService(Context.ACTIVITY_SERVICE);
        final List<RunningServiceInfo> services = activityManager.getRunningServices(Integer.MAX_VALUE);

        for (RunningServiceInfo runningServiceInfo : services) {
            if (runningServiceInfo.service.getClassName().equals(serviceClassName)){
                return true;
            }
        }
        return false;
    }
}
