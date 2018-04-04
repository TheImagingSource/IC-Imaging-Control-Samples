// IniEx.h: interface for the CIniEx class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_INIEX_H__36888C4C_12D3_4F65_A78B_2F3C3576B5B8__INCLUDED_)
#define AFX_INIEX_H__36888C4C_12D3_4F65_A78B_2F3C3576B5B8__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


#define MAX_SECTION_COUNT	256
class CIniEx  
{
private:
	//Functions
	int		LookupKey		(int nSectionIndex,CString *Key);
	int		LookupSection	(CString *Section);
	int		CompareStrings	(CString *str1,CString *str2);
	void    GrowIfNecessary	(void);
	void	FindBackupFile	(void);
private:
	//Variables
	CMapStringToString	*m_tmpLines;
	CString				m_ErrStr;
	CStringArray    	**m_Keys;
	CStringArray    	**m_Values;
	CStringArray		m_Sections;

	int		m_SectionNo;
	int		m_GrowSize;
	BOOL	m_NoCaseSensitive;
	BOOL	m_writeWhenChange;
	CString	m_BackupFileName;
	CString m_FileName;
	BOOL	m_makeBackup;
	BOOL	m_Changed;	

public:
	void GetKeysInSection(CString section,CStringArray &keys);
	void GetSections(CStringArray &sections);

	CIniEx(int GrowSize=4);
	virtual ~CIniEx();

	BOOL Open(LPCSTR pFileName,
			  BOOL writeWhenChange=TRUE,
			  BOOL createIfNotExist=TRUE,
			  BOOL noCaseSensitive=TRUE,
			  BOOL makeBackup=FALSE);
	BOOL OpenAtExeDirectory(LPCSTR pFileName,
			  BOOL writeWhenChange=TRUE,
			  BOOL createIfNotExist=TRUE,
			  BOOL noCaseSensitive=TRUE,
			  BOOL makeBackup=FALSE);
	void ResetContent();

	CString WriteFile(BOOL makeBackup=FALSE);

	CString GetValue(CString Section,CString Key,CString DefaultValue="");
	CString GetValue(CString Key);
	int     GetValueInt(CString Section,CString Key, int iDefault = 0);
	float   GetValueFloat(CString Section,CString Key, float fDefault = 0.0f);
	

	void SetValue(CString Key,CString Value);
	void SetValue(CString Section,CString Key,CString Value);
	void SetValue(CString Section,CString Key,int iValue);

	BOOL RemoveKey(CString Key);
	BOOL RemoveKey(CString Section,CString Key);

	BOOL RemoveSection(CString Section);

	BOOL GetWriteWhenChange();
	void SetWriteWhenChange(BOOL WriteWhenChange);

	void SetBackupFileName(CString &backupFile);


};

#endif // !defined(AFX_INIEX_H__36888C4C_12D3_4F65_A78B_2F3C3576B5B8__INCLUDED_)
