// Color Overlay in Display Path.cpp : 
/*
This sample demonstrates, how to place an Overlay Bitmap in Display path and
also how to force color, even if the video format of the camera is grey scale.
*/

#include "stdafx.h"
#include <conio.h>
#include "tisudshl.h"
using namespace DShowLib;

int _tmain(int argc, _TCHAR* argv[])
{
	InitLibrary();

	atexit( ExitLibrary );

	Grabber grabber;
	grabber.showDevicePage(); // Select a video capture device

	if( grabber.isDevValid())
	{
		// Prepare the overlay, which must be done, before the live video is started.
		grabber.setOverlayBitmapPathPosition (ePP_DISPLAY); 

		smart_ptr<DShowLib::OverlayBitmap> pOverlayBitmap = grabber.getOverlay(ePP_DISPLAY);
		pOverlayBitmap->setColorMode(OverlayBitmap::tOVBColorMode::eCOLOR);
		pOverlayBitmap->setEnable(true);

		grabber.startLive();
		//After the live video has been started, the overlay graphcis can be drawn.
		RECT rectangle; 
		rectangle.left = 100;
		rectangle.right = 200;
		rectangle.top = 100;
		rectangle.bottom = 200;
		pOverlayBitmap->drawFrameRect(RGB(255, 0, 0), rectangle);

		printf("Press any key to end the video\n");
		_getch();
		grabber.stopLive();
	}

	return 0;
}

