
#ifndef _DIALOGS_H_INC_
#define _DIALOGS_H_INC_

#ifdef DIALOGS_EXPORTS
#define DIALOGS_API __declspec(dllexport)
#else
#define DIALOGS_API __declspec(dllimport)
#endif

#include <tisudshl.h>

DIALOGS_API smart_ptr<_DSHOWLIB_NAMESPACE::MediaStreamSink> showMediaStreamSinkDialog( smart_ptr<_DSHOWLIB_NAMESPACE::GrabberSinkType> pOldSink = 0, HWND hParent = 0 );

struct tMediaStreamSinkDialogSettings
{
	std::wstring fileName;
	_DSHOWLIB_NAMESPACE::MediaStreamSink::tCreateData sinkData;

	tMediaStreamSinkDialogSettings( const std::wstring& fn, const _DSHOWLIB_NAMESPACE::MediaStreamSink::tCreateData& cd )
		:	fileName( fn ),
			sinkData( cd )
	{
	}
};

DIALOGS_API void showVideoCaptureDialog( _DSHOWLIB_NAMESPACE::Grabber& grabber, smart_ptr<tMediaStreamSinkDialogSettings>& pSettings = smart_ptr<tMediaStreamSinkDialogSettings>(), HWND hParent = 0 );
DIALOGS_API void showVideoCaptureDialog( _DSHOWLIB_NAMESPACE::Grabber& grabber, HWND hParent = 0 );


#endif

