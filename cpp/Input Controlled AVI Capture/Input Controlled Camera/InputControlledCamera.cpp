// InputControlledCamera.cpp : Implements the classes for the application.
//

#include "stdafx.h"
#include "InputControlledCamera.h"
#include "InputControlledCameraDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CInputControlledCameraApp

BEGIN_MESSAGE_MAP(CInputControlledCameraApp, CWinApp)
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()


// CInputControlledCameraApp construction

CInputControlledCameraApp::CInputControlledCameraApp()
{
}


// The one and only CInputControlledCameraApp object

CInputControlledCameraApp theApp;


// CInputControlledCameraApp initialization

BOOL CInputControlledCameraApp::InitInstance()
{

	// Init IC Imaging Control 

	if( !DShowLib::InitLibrary() )
	{
		AfxMessageBox( "The IC Imaging Control Class Library could not be initialized.\n(invalid license key?)" );
		exit( 1 );
	}
	// At the end of the program, the IC Imaging Control Class Library must be cleaned up
	// by a call to ExitLibrary().
	atexit(DShowLib::ExitLibrary );

	// InitCommonControls() is required on Windows XP, if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	InitCommonControls();

	CWinApp::InitInstance();

	AfxEnableControlContainer();

	// Standard initialization
	// If you are not using these features and wish to reduce the size
	// of your final executable, you should remove from the following
	// the specific initialization routines you do not need
	// Change the registry key under which our settings are stored
	// TODO: You should modify this string to be something appropriate
	// such as the name of your company or organization
	SetRegistryKey(_T("InputControlledCamera"));

	CInputControlledCameraDlg dlg;
	m_pMainWnd = &dlg;
	dlg.DoModal();


	// Since the dialog has been closed, return FALSE so that we exit the
	// application, rather than start the application's message pump.
	return FALSE;
}
