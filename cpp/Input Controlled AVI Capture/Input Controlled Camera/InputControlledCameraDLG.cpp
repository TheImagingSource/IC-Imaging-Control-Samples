// InputControlledCameraDlg.cpp : 
/*
This sample shows how to start and stop AVI capture on state change on the General
Purpose Input (also known as "Trigger" or "GP In") of The Imaging Source cameras.

AVI Capture can be either on
- two input changes (trigger pulses), one for start and one for stop
- or last as long as the Input is to high.

The sample contains
- AVI Codec selection
- AVI Codec property save and restore
- automatic file name generation for video files.
- camera setup
- handling or the GP In property

After an AVI file was captured and closed, the next is prepared. This
reduces the delay between GP Input change and capture start.

The program is as it is. If you find errors, you may fix them let me know about
that fix.

The program needs
- IC Imaging Control 3.4
- Visual Studio 2015 C++

The compiled program is avaialble a setup in "Input_Controlled_AVI_Capture_setup.exe"
The Inno Setup Script is "setup.iss".
*/

#include "stdafx.h"
#include "InputControlledCamera.h"
#include "InputControlledCameraDlg.h"
#include ".\InputControlledCameradlg.h"
#include ".\Dialogs.h"
#include "IniEx.h"
#include <direct.h>
#include <stdio.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// Save some typing...
using namespace DShowLib;


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CInputControlledCameraDlg dialog



CInputControlledCameraDlg::CInputControlledCameraDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CInputControlledCameraDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	pSink = NULL;
	m_bCreateNewAVIFiles = true;
	m_AVIFileCounter = 0;
	m_InputPolarityCheck = 0;
	m_OperationMode = 0;
	m_OldInputState = -1;

	// Create the save path for camera and program settings. It is in 
	// %appdata%/inputcontrolledavicapture directory.

	char SavePath[MAX_PATH];

	if (SUCCEEDED(SHGetFolderPath(NULL, CSIDL_APPDATA, NULL, 0, SavePath)))
	{
		struct stat info;
		strcat_s(SavePath, MAX_PATH, "\\inputcontrolledavicapture");
		if (stat(SavePath, &info) != 0)
		{
			_mkdir(SavePath);
		}
		strcpy_s(m_DeviceFile, MAX_PATH, SavePath);
		strcat_s(m_DeviceFile, MAX_PATH, "\\device.xml");

		strcpy_s(m_SettingsFile, MAX_PATH, SavePath);
		strcat_s(m_SettingsFile, MAX_PATH, "\\settings.ini");

		strcpy_s(m_CodecFile, MAX_PATH, SavePath);
		strcat_s(m_CodecFile, MAX_PATH, "\\codec.xml");


	}
}


CInputControlledCameraDlg::~CInputControlledCameraDlg()
{
}

void CInputControlledCameraDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_BUTTONIMAGESETTINGS, m_cButtonSettings);
	DDX_Control(pDX, IDC_BUTTONLIVEVIDEO, m_cButtonLive);
	DDX_Control(pDX, IDC_STATICVIDEO, m_cStaticVideoWindow);
	DDX_Control(pDX, IDC_STATICRECORDING, m_cStaticRecording);
	DDX_Control(pDX, IDC_CHECKPOLARITY, m_chkPolarity);
}

// CInputControlledCameraDlg message handlers

BEGIN_MESSAGE_MAP(CInputControlledCameraDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BUTTONDEVICE, OnBnClickedButtondevice)
	ON_BN_CLICKED(IDC_BUTTONIMAGESETTINGS, OnBnClickedButtonimagesettings)
	ON_BN_CLICKED(IDC_BUTTONLIVEVIDEO, OnBnClickedButtonlivevideo)
	ON_WM_CLOSE()
	ON_WM_TIMER()
	ON_BN_CLICKED(IDC_BUTTONAVISETTINGS, &CInputControlledCameraDlg::OnBnClickedButtonavisettings)
	ON_BN_CLICKED(IDC_CHECKPOLARITY, &CInputControlledCameraDlg::OnBnClickedCheckpolarity)
	ON_BN_CLICKED(IDC_RADIOOPERATIONWHILESET, &CInputControlledCameraDlg::OnBnClickedRadiooperationwhileset)
	ON_BN_CLICKED(IDC_RADIOOPERATIONSIGNALS, &CInputControlledCameraDlg::OnBnClickedRadiooperationsignals)
END_MESSAGE_MAP()



BOOL CInputControlledCameraDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}


	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	((CButton*)GetDlgItem(IDC_RADIOOPERATIONWHILESET))->SetCheck(1);

	// Resize the video window to 640*480 pixels.
	m_cStaticVideoWindow.SetWindowPos(NULL,0,0,640,480,SWP_NOMOVE|SWP_NOZORDER);

	// Assign this window to the Grabber object for live video display.
	m_cGrabber.setHWND(m_cStaticVideoWindow.m_hWnd);

	// Adjust the live video to the size of the window.
	CRect rect;
	m_cStaticVideoWindow.GetClientRect( &rect);
	m_cGrabber.setDefaultWindowPosition(false);
	m_cGrabber.setWindowSize(rect.Width(), rect.Height());

	ReloadSink();

	// Try to load the previously used video capture device.
	if( m_cGrabber.loadDeviceStateFromFile(m_DeviceFile))
	{
		m_cStaticRecording.SetWindowTextA("Prepare");
		// Display the device's name in the caption bar of the application.
		SetWindowText( "Input Controlled AVI Capture " + CString(m_cGrabber.getDev().toString().c_str()));
		if(!GetGPInput())
		{
			MessageBox("The current camera has no input","Input Controlled Camera");
		}

		//OnBnClickedButtonavisettings();

		SetTimer(1,100,NULL);
	}

	SetButtonStates();

	return TRUE;  // return TRUE unless you set the focus to a control.
}

void CInputControlledCameraDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}
//////////////////////////////////////////////////////////////////////////
// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon. For MFC applications using the document/view model,
//  this is automatically done for you by the framework.
//////////////////////////////////////////////////////////////////////////

void CInputControlledCameraDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting.

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle.
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}
//////////////////////////////////////////////////////////////////////////
// The system calls this function to obtain the cursor to display while
// the user dragsthe minimized window.
//////////////////////////////////////////////////////////////////////////

HCURSOR CInputControlledCameraDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

//////////////////////////////////////////////////////////////////////////
// Show the device select dialog, if the "Device" button has been clicked.
// If live video is displayed, it has to be stopped.
//////////////////////////////////////////////////////////////////////////

void CInputControlledCameraDlg::OnBnClickedButtondevice()
{
	// If live video is running, stop it.
	if(m_cGrabber.isDevValid() && m_cGrabber.isLive())
	{
		m_cGrabber.stopLive();
	}

	m_cGrabber.showDevicePage(this->m_hWnd);

	// If we have selected a valid device, save it to the file "device.xml", so
	// the application can load it automatically when it is started the next time.
	if( m_cGrabber.isDevValid())
	{
		m_cGrabber.saveDeviceStateToFile(m_DeviceFile);
		if(!GetGPInput())
		{
			MessageBox("The current camera has no input","Input Controlled Camera");
		}
		else
		{
			m_cGrabber.prepareLive(true);
			m_cGrabber.startLive();
			SetButtonStates();
		}
	}

	// Now display the device's name in the caption bar of the application.
	SetWindowText( "InputControlledCamera " + CString(m_cGrabber.getDev().toString().c_str() ));

	SetButtonStates();
}

/////////////////////////////////////////////////////////////////////
// Restore the AVI codec settings from the save files
//
void CInputControlledCameraDlg::ReloadSink()
{
	CIniEx Ini;
	pSink = NULL;
	if (Ini.Open(m_SettingsFile))
	{
		CString CodecName;
		tCodecPtr pCodec = NULL;
		CodecName = Ini.GetValue("AVI", "Codec", "" );
		if (CodecName != "")
		{
			// Select the previously used codec-
			// Create an AVI Media Stream Container.
			tMediaStreamContainerPtr pAVIContainer = MediaStreamContainer::create(MSC_AviContainer);

			// Obtain a list of available codecs.
			tCodecListPtr pAvailableCodecList = Codec::getAvailableCodecs();
			for (auto Codec : *pAvailableCodecList)
			{
				if (Codec->getName().c_str() == CodecName)
				{
					pCodec = Codec;
					break;
				}
			}

			if (pCodec != NULL)
			{
				// Get the previously saved codec's properties.
				::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, CodecName);

				if (pCodec->hasSettings())
				{
					FILE* pFile = NULL;
					fopen_s(&pFile, m_CodecFile, "rb");

					if (pFile)
					{
						fseek(pFile, 0, SEEK_END);
						int size = ftell(pFile);
						std::vector<char> Buffer(size + 1, '\0');

						fseek(pFile, 0, SEEK_SET);

						fread(Buffer.data(), size, 1, pFile);
						fclose(pFile);

						pCodec->setSettings(Buffer.data());
					}
				}
				pSink = MediaStreamSink::create(pAVIContainer, pCodec);
			}
			else
			{
				GUID subtype;
				std::string type = CodecName;
				subtype = StringToGUID(astows(type).c_str());
				pSink = MediaStreamSink::create(pAVIContainer, subtype);
				SetUncompressedText();
			}
		}

		if (pSink != NULL)
		{
			// Create the new AVI file name, display it and pass it to the sink.
			CString FileName;
			FileName = Ini.GetValue("AVI", "Filename", "");
			if (FileName != "")
			{
				pSink->setFilename((LPCSTR)FileName);
				::SetWindowText(GetDlgItem(IDC_STATICFILENAME)->m_hWnd, FileName);

				m_FileName = pSink->getFilename().c_str();
				m_FileName.Replace(".avi", "%04d.avi");		// Add a format string into the file name, so it will be replaced with the counter value
				CreateNewFile();

				pSink->setSinkMode(GrabberSinkType::ePAUSE);
				m_cGrabber.setSinkType(pSink);
			}
		}
	}
}


/////////////////////////////////////////////////////////////////////
// Create user readable text for the uncompressed codecs
//
void CInputControlledCameraDlg::SetUncompressedText()
{
	GUID subtype = pSink->getSubType();

	if (subtype == MEDIASUBTYPE_RGB24)
		::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, "RGB24");

	if (subtype == MEDIASUBTYPE_RGB32)
		::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, "RGB32");

	if (subtype == MEDIASUBTYPE_Y800)
		::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, "Y800");

	if (subtype == MEDIASUBTYPE_Y16)
		::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, "Y16");

	if (subtype == MEDIASUBTYPE_RGB555)
		::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, "RGB555");

	if (subtype == MEDIASUBTYPE_RGB565)
		::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, "RGB565");

	if (subtype == MEDIASUBTYPE_UYVY)
		::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, "UYVY");

}

/////////////////////////////////////////////////////////////////////
// Save codec settings
//
void CInputControlledCameraDlg::SaveCodecData()
{
	CIniEx Ini;
	if (Ini.Open(m_SettingsFile))
	{
		smart_ptr<Codec> pCodec = pSink->getCodec();

		if (pCodec != NULL)
		{
			Ini.SetValue("AVI", "Codec", pCodec->getName().c_str());
			::SetWindowText(GetDlgItem(IDC_STATICCODEC)->m_hWnd, pCodec->getName().c_str());
			if (pCodec->hasSettings())
			{
				FILE *pCodecFile = NULL;
					
				fopen_s(&pCodecFile, m_CodecFile, "w");

				if (pCodecFile)
				{
					fwrite(pCodec->getSettings().c_str(), strlen(pCodec->getSettings().c_str()), 1, pCodecFile);
					fclose(pCodecFile);
				}
			}
		}
		else
		{
			GUID type = pSink->getSubType();
			std::wstring guidstring = GUIDToWString( pSink->getSubType());

			Ini.SetValue("AVI", "Codec", wstoas(guidstring).c_str() );
			SetUncompressedText();
		}
		Ini.SetValue("AVI", "Filename", pSink->getFilename().c_str());
		::SetWindowText(GetDlgItem(IDC_STATICFILENAME)->m_hWnd, pSink->getFilename().c_str());
	}

}


//////////////////////////////////////////////////////////////////////////
// Show the image settings dialog of IC Imaging Control. 
//
void CInputControlledCameraDlg::OnBnClickedButtonimagesettings()
{
	if( m_cGrabber.isDevValid())
	{
		m_cGrabber.showVCDPropertyPage(this->m_hWnd);
		m_cGrabber.saveDeviceStateToFile(m_DeviceFile);
	}
	
}

//////////////////////////////////////////////////////////////////////////
// This method sets the states of the dialog's buttons.
//
void CInputControlledCameraDlg::SetButtonStates(void)
{
	bool bDevValid =  m_cGrabber.isDevValid();
	bool bIsLive   = m_cGrabber.isLive();

	m_cButtonSettings.EnableWindow(bDevValid);
	m_cButtonLive.EnableWindow(bDevValid);
	
	if( !bDevValid )
	{
		m_cButtonLive.SetWindowText("Live Start");
	}


	if( bIsLive )
	{
		m_cButtonLive.SetWindowText("Live Stop");
	}
	else
	{
		m_cButtonLive.SetWindowText("Live Start");
	}


}

//////////////////////////////////////////////////////////////////////////
// This method starts and stops the live video.
//
void CInputControlledCameraDlg::OnBnClickedButtonlivevideo()
{
	if( m_cGrabber.isDevValid())
	{
		if (m_cGrabber.isLive())
		{
			m_cGrabber.stopLive();
		}
		else
		{
			m_cGrabber.prepareLive(true);
			m_cGrabber.startLive();
			pSink->setSinkMode( GrabberSinkType::ePAUSE);
		}
		SetButtonStates();
	}
}

//////////////////////////////////////////////////////////////////////////
// Close program
void CInputControlledCameraDlg::OnClose()
{
	m_cGrabber.stopLive();

	CDialog::OnClose();
}


///////////////////////////////////////////////////////////////////////////
// This function queries the GP Input properties. There are two of them, 
// one holds the value, the other one reads the value.
// The function returns true, if GP Input is supported by the current 
// camera and the properties have been querried successfully.
//
// This function must be called every time, a new device has been opened.
//
bool CInputControlledCameraDlg::GetGPInput()
{
	bool bResult = false;
	if( m_cGrabber.isDevValid() )
	{
		tIVCDPropertyItemsPtr pItems = m_cGrabber.getAvailableVCDProperties();
		pItems->findInterfacePtr(VCDID_GPIO, VCDElement_GPIOIn ,m_pInput);
		if( m_pInput != NULL )
		{
			pItems->findInterfacePtr(VCDID_GPIO, VCDElement_GPIORead ,m_pInputRead);
			if( m_pInputRead != NULL )
			{
				bResult = true;
			}
		}
	}

	return bResult;
}


/////////////////////////////////////////////////////////////////////
// The timer event function, that is used for polling the GP Input.
//
void CInputControlledCameraDlg::OnTimer(UINT_PTR nIDEvent)
{
	long lInputState = 0;
	KillTimer(nIDEvent);
	if( m_cGrabber.isDevValid() )
	{
		if( pSink != NULL  && m_cGrabber.isLive() )
		{
			if( m_pInputRead != NULL )
			{
				m_pInputRead->push();				// Get the current value from the device
				m_pInput->get_Value(&lInputState);	// Now read the value	
				
				if (m_OperationMode == 0)
					WhileSetOperation(lInputState);
				else
					StartStopOperation(lInputState);

			}
		}
	}
	else
	{
		m_cStaticRecording.SetWindowTextA("No Device");
	}

	SetTimer(nIDEvent,100,NULL);
	CDialog::OnTimer(nIDEvent);
}

/////////////////////////////////////////////////////////////////////
// This function is used for AVI capture while the GP Input is set
//
void CInputControlledCameraDlg::WhileSetOperation(long CurrentInputState)
{
	if (CurrentInputState == m_InputPolarityCheck)
	{
		if (pSink->getSinkMode() == GrabberSinkType::eRUN)
		{
			m_cStaticRecording.SetWindowTextA("Pause");

			if (m_bCreateNewAVIFiles)
			{
				m_cGrabber.stopLive();

				CreateNewFile();

				pSink->setSinkMode(GrabberSinkType::ePAUSE);
				m_cGrabber.startLive();
			}
			else
			{
				pSink->setSinkMode(GrabberSinkType::ePAUSE);
			}
		}
	}
	else
	{
		if (pSink->getSinkMode() == GrabberSinkType::ePAUSE)
		{
			m_cStaticRecording.SetWindowTextA("Record");
			pSink->setSinkMode(GrabberSinkType::eRUN);
		}
	}

}

/////////////////////////////////////////////////////////////////////
// This function is used for AVI capture start and stop on single
// GP In state changes (trigger pulses)
//
void CInputControlledCameraDlg::StartStopOperation(long CurrentInputState)
{

	if (CurrentInputState != m_OldInputState)
	{
		if (CurrentInputState == m_InputPolarityCheck)
		{
			if (pSink->getSinkMode() == GrabberSinkType::eRUN)
			{
				m_cStaticRecording.SetWindowTextA("Pause");

				if (m_bCreateNewAVIFiles)
				{
					m_cGrabber.stopLive();

					CreateNewFile();
					pSink->setSinkMode(GrabberSinkType::ePAUSE);
					m_cGrabber.startLive();
				}
				else
				{
					pSink->setSinkMode(GrabberSinkType::ePAUSE);
				}
			}
			else
			{
				if (pSink->getSinkMode() == GrabberSinkType::ePAUSE)
				{
					m_cStaticRecording.SetWindowTextA("Record");
					pSink->setSinkMode(GrabberSinkType::eRUN);
				}
			}
		}

		m_OldInputState = CurrentInputState;
	}

}


/////////////////////////////////////////////////////////////////////
// Show the codec's property dialog and save the codec's data.
//
void CInputControlledCameraDlg::OnBnClickedButtonavisettings()
{
	if( m_cGrabber.isDevValid() )
	{
		if (m_cGrabber.isLive())
		{
			m_cGrabber.stopLive();
			m_cStaticRecording.SetWindowTextA("Stopped");
		}
	}

	pSink =  showMediaStreamSinkDialog(pSink, this->m_hWnd );
	if( pSink != NULL )
	{
		SaveCodecData();

		m_FileName = pSink->getFilename().c_str();
		m_FileName.Replace(".avi","%04d.avi");
		CreateNewFile();

		pSink->setSinkMode( GrabberSinkType::ePAUSE);
		m_cGrabber.setSinkType(pSink);
	}
	m_cStaticRecording.SetWindowTextA("Pause");
	SetButtonStates();
}

/////////////////////////////////////////////////////////////////////
// Create a new AVI file name and prepare the AVI capture sink with it.
//
void CInputControlledCameraDlg::CreateNewFile()
{
	if( pSink != NULL )
	{
		CString NewFileName= "";
		m_AVIFileCounter++;
		NewFileName.Format(m_FileName,m_AVIFileCounter);
		pSink->setFilename(std::string((LPCSTR)NewFileName) );
	}
}
/////////////////////////////////////////////////////////////////////
// Polarity check box handler
//
void CInputControlledCameraDlg::OnBnClickedCheckpolarity()
{
	if (m_chkPolarity.GetCheck())
		m_InputPolarityCheck = 1;
	else
		m_InputPolarityCheck = 0;

}


/////////////////////////////////////////////////////////////////////
// Operation radio button handler for the AVI capture while GP In is set
//
void CInputControlledCameraDlg::OnBnClickedRadiooperationwhileset()
{
	int value = GetCheckedRadioButton(IDC_RADIOOPERATIONWHILESET, IDC_RADIOOPERATIONWHILESET);
	if (value == IDC_RADIOOPERATIONWHILESET)
	{
		m_OperationMode = 0;
	}

}


/////////////////////////////////////////////////////////////////////
// Operation radio button handler for the AVI capture for signals for start and stop
//
void CInputControlledCameraDlg::OnBnClickedRadiooperationsignals()
{
	int value = GetCheckedRadioButton(IDC_RADIOOPERATIONSIGNALS, IDC_RADIOOPERATIONSIGNALS);
	if (value == IDC_RADIOOPERATIONSIGNALS)
	{
		m_OperationMode = 1;
	}
}
