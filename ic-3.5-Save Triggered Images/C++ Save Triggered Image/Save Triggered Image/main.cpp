///////////////////////////////////////////////////////////////////////////////
//
// 
// This example demonstrates how to save images on trigger signals useing
// a FrameQueueSink. The program terminates after 10 images. // 
//

#define _WIN32_WINNT 0x0601

#include <iostream>

#include <tisudshl.h>

using namespace DShowLib;

class sink_listener : public FrameNotificationSinkListener
{
public:
	virtual void sinkConnected(const FrameTypeInfo& frameType) override {}
	virtual void sinkDisconnected() override {}
	virtual void frameReceived(IFrame& frame) override
	{
		// Create a file name 
		char szFileName[MAX_PATH];
		_imagecount++;
		sprintf_s(szFileName, MAX_PATH, "img%05d.bmp", _imagecount);


		DShowLib::saveToFileBMP(frame, szFileName);
		std::cout << "image : " << _imagecount << "\n";
	}

	int _imagecount = 0;
};


int main()
{
	InitLibrary();

	atexit( ExitLibrary );
	
	Grabber grabber;

	grabber.showDevicePage();
	if (!grabber.isDevValid())
		return 1;
	
    sink_listener    listener_instance;
	FrameTypeInfo type(DShowLib::eRGB32);
	tFrameNotificationSinkPtr pSink = FrameNotificationSink::create(listener_instance, type);

    // Apply the sink to the grabber.
	grabber.setSinkType( pSink );

	// Enable trigger mode
	auto triggerMode = grabber.getVCDPropertyInterface<DShowLib::IVCDSwitchProperty>(DShowLib::VCDID_Trigger, DShowLib::VCDElement_Value);
	if (triggerMode != nullptr)
		triggerMode->setSwitch(true);

	if (!grabber.startLive(true))
		return 1;
	
	while(listener_instance._imagecount < 10)
	{
		Sleep(100);
	}
	grabber.stopLive();

	// Disable trigger mode.
	if (triggerMode != nullptr)
		triggerMode->setSwitch(false);

    std::cout << "Press any key to continue!" << std::endl;
	std::cin.get();
	return 0;
}

