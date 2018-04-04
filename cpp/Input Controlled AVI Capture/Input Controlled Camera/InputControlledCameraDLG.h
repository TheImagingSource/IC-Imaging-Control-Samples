// InputControlledCameraDlg.h : header file
//

#pragma once
#include "afxwin.h"

// CInputControlledCameraDlg dialog
class CInputControlledCameraDlg : public CDialog
{
// Construction
public:
	CInputControlledCameraDlg(CWnd* pParent = NULL);	// standard constructor
	~CInputControlledCameraDlg();

// Dialog Data
	enum { IDD = IDD_InputControlledCamera_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnClose();
	DECLARE_MESSAGE_MAP()


private:
	DShowLib::Grabber		m_cGrabber; // The instance of the Grabber class.
	DShowLib::tIVCDRangePropertyPtr m_pInput; // The interface to the GP Input value
	DShowLib::tIVCDButtonPropertyPtr m_pInputRead; // the interface to the GP Input Read button

	CButton m_cButtonSettings;
	CButton m_cButtonLive;
	CStatic m_cStaticVideoWindow;

	char m_DeviceFile[MAX_PATH];
	char m_SettingsFile[MAX_PATH];
	char m_CodecFile[MAX_PATH];
	bool GetGPInput();
	smart_ptr<_DSHOWLIB_NAMESPACE::MediaStreamSink> pSink;
	bool m_bCreateNewAVIFiles;
	CString m_FileName;
	int m_AVIFileCounter;		// Counter for automatic file name generateion

	int m_InputPolarityCheck;	// Trigger polarity value, for check on high or low
	int m_OperationMode;		// Two pulses, or as long as high
	int m_OldInputState;		// Buffer for input state change

	void WhileSetOperation(long CurrentInputState);
	void StartStopOperation(long CurrentInputState);
	void CreateNewFile();
	void SaveCodecData();
	void ReloadSink();
	void SetUncompressedText();

public:
	afx_msg void OnBnClickedButtondevice();
	afx_msg void OnBnClickedButtonimagesettings();
	void SetButtonStates(void);

	afx_msg void OnBnClickedButtonlivevideo();
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	CStatic m_cStaticRecording;
	afx_msg void OnBnClickedButtonavisettings();
	afx_msg void OnBnClickedCheckpolarity();
	CButton m_chkPolarity;
	afx_msg void OnBnClickedRadiooperationwhileset();
	afx_msg void OnBnClickedRadiooperationsignals();
};
