; Script generated by the HM NIS Edit Script Wizard.
!addincludedir .\NSISIncludes
!include WordFunc.nsh
!insertmacro VersionCompare
!include LogicLib.nsh
!include DotNetVer.nsh
; include for some of the windows messages defines
!include "winmessages.nsh"
; HKLM (all users) vs HKCU (current user) defines
!define env_hklm 'HKLM "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"'
!define env_hkcu 'HKCU "Environment"'

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "UV Quant Suite"
!define PRODUCT_VERSION "4.1"
!define PRODUCT_PUBLISHER "Argos Scientific Inc"
!define PRODUCT_WEB_SITE "http://www.argos-sci.com"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\UV Quant.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"
!define REG_ENVIRONMENT "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\UV Quant.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Setup.exe"
InstallDir "$PROGRAMFILES\UV Quant Suite"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Var InstallDotNET

Function .onInit
	!insertmacro MUI_LANGDLL_DISPLAY
	; Make sure .NET 4.0 is installed, and install it if not there
	${If} ${HasDotNet4.0}
		StrCpy $InstallDotNET "No"
	${Else}
		StrCpy $InstallDotNET "Yes"
		MessageBox MB_OK|MB_ICONINFORMATION "${PRODUCT_NAME} requires that the .NET Framework 4.0 is installed. The .NET Framework will be installed automatically during installation of ${PRODUCT_NAME}."
		Return
	${EndIf}
	
	; ensure the OOI_HOME env. variable is removed 
	; this will ensure the env. variable is always
	; pointing to the current install of omni driver as the
	; omni driver is always installed with this installer
	 ; delete variable
   DeleteRegValue ${env_hklm} OOI_HOME
   ; make sure windows knows about the change
   SendMessage ${HWND_BROADCAST} ${WM_WININICHANGE} 0 "STR:Environment" /TIMEOUT=5000
	
FunctionEnd

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  
  ; Get .NET if required
  ;SetDetailsView hide
  ;SetDetailsView show
  
  File ".\zedgraph\ZedGraph.xml"
  File ".\zedgraph\ZedGraph.dll"
  File "..\bin\Release\UV Quant.exe"
  File "..\bin\Release\UV Quant.exe.config"
  File "..\UVQuant.manifest"
  File "..\bin\Release\UV Quant.vshost.exe"
  File "..\bin\Release\UV Quant.vshost.exe.config"
  File "..\bin\Release\UV Quant.vshost.exe.manifest"
  File ".\omnidriver\NETOmniDriver.dll"
  File "..\images\argos_logo.JPG"
  File "..\images\argos_logo_small_running.jpg"
  File "..\images\green_circle.gif"
  File "..\images\red_circle.gif"  
  File "..\images\yellow_circle.gif"
  File "..\images\argos_logo_small.JPG"
  File "..\userguide\ug_uvquant.chm"
  
  ;Since we want to add the OOI_HOME env. va. to all users
  !ifdef ALL_USERS
    !define ReadEnvStr_RegKey \
       'HKLM "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"'
  !else
    !define ReadEnvStr_RegKey 'HKCU "Environment"'
  !endif
  
  CreateDirectory "$SMPROGRAMS\UV Quant Suite"
  CreateShortCut "$SMPROGRAMS\UV Quant Suite\UV Quant Suite.lnk" "$INSTDIR\UV Quant.exe"
  CreateShortCut "$DESKTOP\UV Quant Suite.lnk" "$INSTDIR\UV Quant.exe"
  SetOutPath "$SYSDIR"
  File "Cerex_mn.dll"
  File "bwtekusb.dll"
  File "AS5216.dll"
  
  ;Install JRE if necessary
  File /oname=$TEMP\jre_setup.exe jre1p5setup.exe
  ExecWait "$TEMP\jre_setup.exe"

  ;Install .NET 4.0 if specified
  ${If} $InstallDotNET == "Yes"
    File "dotNetFx40_Full_x86_x64.exe"
    SetDetailsView hide
    ExecWait "$SYSDIR\dotNetFx40_Full_x86_x64.exe"
    Delete "$SYSDIR\dotNetFx40_Full_x86_x64.exe"
    SetDetailsView show
  ${EndIf}
  
  ;Install OmniDriver
  File /oname=$TEMP\omni_driver.exe OmniDriver-1.66-win32-redistributable.exe
  ExecWait "$TEMP\omni_driver.exe"
  Delete "$TEMP\omni_driver.exe"
  
  SetOutPath "$INSTDIR\es"
  SetOverwrite try
  File ".\zedgraph\es\ZedGraph.resources.dll"
SectionEnd

Section -AdditionalIcons
  SetOutPath $INSTDIR
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\UV Quant Suite\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\UV Quant.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\UV Quant.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "UV Quant was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove UV Quant and all of its components?" IDYES +2
  Abort
FunctionEnd

; ################################################################
; appends \ to the path if missing
; example: !insertmacro GetCleanDir "c:\blabla"
; Pop $0 => "c:\blabla\"
!macro GetCleanDir INPUTDIR
  ; ATTENTION: USE ON YOUR OWN RISK!
  ; Please report bugs here: http://stefan.bertels.org/
  !define Index_GetCleanDir 'GetCleanDir_Line${__LINE__}'
  Push $R0
  Push $R1
  StrCpy $R0 "${INPUTDIR}"
  StrCmp $R0 "" ${Index_GetCleanDir}-finish
  StrCpy $R1 "$R0" "" -1
  StrCmp "$R1" "\" ${Index_GetCleanDir}-finish
  StrCpy $R0 "$R0\"
${Index_GetCleanDir}-finish:
  Pop $R1
  Exch $R0
  !undef Index_GetCleanDir
!macroend
 
; ################################################################
; similar to "RMDIR /r DIRECTORY", but does not remove DIRECTORY itself
; example: !insertmacro RemoveFilesAndSubDirs "$INSTDIR"
!macro RemoveFilesAndSubDirs DIRECTORY
  ; ATTENTION: USE ON YOUR OWN RISK!
  ; Please report bugs here: http://stefan.bertels.org/
  !define Index_RemoveFilesAndSubDirs 'RemoveFilesAndSubDirs_${__LINE__}'
 
  Push $R0
  Push $R1
  Push $R2
 
  !insertmacro GetCleanDir "${DIRECTORY}"
  Pop $R2
  FindFirst $R0 $R1 "$R2*.*"
${Index_RemoveFilesAndSubDirs}-loop:
  StrCmp $R1 "" ${Index_RemoveFilesAndSubDirs}-done
  StrCmp $R1 "." ${Index_RemoveFilesAndSubDirs}-next
  StrCmp $R1 ".." ${Index_RemoveFilesAndSubDirs}-next
  IfFileExists "$R2$R1\*.*" ${Index_RemoveFilesAndSubDirs}-directory
  ; file
  Delete "$R2$R1"
  goto ${Index_RemoveFilesAndSubDirs}-next
${Index_RemoveFilesAndSubDirs}-directory:
  ; directory
  RMDir /r "$R2$R1"
${Index_RemoveFilesAndSubDirs}-next:
  FindNext $R0 $R1
  Goto ${Index_RemoveFilesAndSubDirs}-loop
${Index_RemoveFilesAndSubDirs}-done:
  FindClose $R0
 
  Pop $R2
  Pop $R1
  Pop $R0
  !undef Index_RemoveFilesAndSubDirs
!macroend

Section Uninstall
  !insertmacro RemoveFilesAndSubDirs "$INSTDIR\"
  Delete "$SYSDIR\Cerex_mn.dll"
  Delete "$SYSDIR\bwtekusb.dll"
  Delete "$DESKTOP\UV Quant Suite.lnk"
  Delete "$SMPROGRAMS\UV Quant Suite\Uninstall.lnk"
  Delete "$SMPROGRAMS\UV Quant Suite\Website.lnk"
  Delete "$SMPROGRAMS\UV Quant Suite\UV Quant Suite.lnk"

  RMDir "$SMPROGRAMS\UV Quant Suite"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  DeleteRegKey HKLM "SOFTWARE\ArgosQuant"

  SetAutoClose true
SectionEnd
