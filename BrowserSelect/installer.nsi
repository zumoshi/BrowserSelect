Name "BrowserSelect"
Caption "BrowserSelect Installation"
Icon "${NSISDIR}\Contrib\Graphics\Icons\nsis1-install.ico"
OutFile "BrowserSelect.exe"

InstallDir "$LOCALAPPDATA\BrowserSelect"
InstallDirRegKey HKCU "Software\BrowserSelect" "Install_Dir"

RequestExecutionLevel user

;--------------------------------
;Interface Settings
  !include "MUI2.nsh"
  !define MUI_ABORTWARNING

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_LICENSE "./License.txt"
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"
  
  ;ADD YOUR OWN FILES HERE...
  File "/oname=BrowserSelect.exe" ".\bin\Release\BrowserSelect.exe"
  File "/oname=Newtonsoft.Json.dll" ".\bin\Release\Newtonsoft.Json.dll"
  createShortCut "$SMPROGRAMS\BrowserSelect.lnk" "$INSTDIR\BrowserSelect.exe"
  
  ;Store installation folder
  WriteRegStr HKCU "Software\BrowserSelect" "" $INSTDIR
  ;For control panel uninstall
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\BrowserSelect" \
                 "DisplayName" "BrowserSelect -- select browser dynamically"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\BrowserSelect" \
                 "UninstallString" "$\"$INSTDIR\uninstall.exe$\""
  ;for register as default browser
  ;create entry in startmenuinternet
  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE" \
                 "" "Browser Select"
;add capabilities
  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities" \
                 "ApplicationName" "BrowserSelect"
  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities" \
                 "ApplicationDescription" "Choose a Browser dynamically."
  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities" \
                 "ApplicationIcon" "$INSTDIR\BrowserSelect.exe,0"

  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities\StartMenu" \
                 "StartMenuInternet" "BROWSERSELECT.EXE"

  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities\URLAssociations" \
                 "http" "bselectURL"
  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities\URLAssociations" \
                 "https" "bselectURL"
;add icon and command				 
  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\DefaultIcon" \
                 "" "$INSTDIR\BrowserSelect.exe,0"
  WriteRegStr HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\shell\open\command" \
                 "" "$\"$INSTDIR\BrowserSelect.exe$\""
;register capablities
  WriteRegStr HKCU "Software\RegisteredApplications" \
                 "BrowserSelect" "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities"
;register handler
  WriteRegStr HKCU "Software\Classes\bselectURL" \
                 "" "BrowserSelect Url"
  WriteRegStr HKCU "Software\Classes\bselectURL\shell\open\command" \
                 "" "$\"$INSTDIR\BrowserSelect.exe$\" $\"%1$\""
  
  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  ;ADD YOUR OWN FILES HERE...

  Delete "$INSTDIR\Uninstall.exe"
  Delete "$INSTDIR\BrowserSelect.exe"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$SMPROGRAMS\BrowserSelect.lnk"

  ; todo: remove user.conf file(s) after asking user
  RMDir "$INSTDIR"

  DeleteRegKey /ifempty HKCU "Software\BrowserSelect"
  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\BrowserSelect"
  DeleteRegKey HKCU "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE"
  DeleteRegValue  HKCU "Software\RegisteredApplications" "BrowserSelect"
  DeleteRegKey HKCU "Software\Classes\bselectURL"

SectionEnd