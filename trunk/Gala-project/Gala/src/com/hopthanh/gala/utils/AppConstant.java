package com.hopthanh.gala.utils;

import java.util.Arrays;
import java.util.List;

public class AppConstant {
	// Number of styles.
//	public static final int NUM_OF_STYLES = 10;
//	public static final int LAYOUT_TYPE_SLIDE_IMAGE = 1;
//	public static final int LAYOUT_TYPE_SLIDE_GRIDVIEW = 2;
//	public static final int LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW = 3;
//	public static final int LAYOUT_TYPE_HORIZONTAL_SCROLL_VIEW_SPECIAL_STORES = 4;
//	public static final int LAYOUT_TYPE_NORMAL = 5;
	
	// Number of columns of Grid View
	public static final int NUM_OF_COLUMNS_PORTRAIT = 2;
	public static final int NUM_OF_COLUMNS_LANDSCAPE = 3;

	// Gridview image padding
	public static final int GRID_PADDING = 3; // in dp
	public static final int GRID_SPACING = 1; // in dp

	// SD card image directory
	public static final String PHOTO_ALBUM = "Pictures/Galagala/Media/Mall-Banner";

	// supported file formats
	public static final List<String> FILE_EXTN = Arrays.asList("jpg", "jpeg",
			"png");
}
