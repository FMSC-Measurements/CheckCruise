; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!
#define VERSION "2020.03.12"
#define APP "Check Cruise"

[Setup]

AppName={#APP}
AppVerName={#APP} version {#VERSION}
AppPublisher=Forest Management Service Center
AppPublisherURL=http://www.fs.fed.us/fmsc/measure/cruising/cruiseprocessing/index.php
AppSupportURL=http://www.fs.fed.us/fmsc/measure/support.shtml
AppUpdatesURL=http://www.fs.fed.us/fmsc/measure/cruising/cruiseprocessing/index.php
AppMutex=CheckCruise
; CurPageChanged in the [Code] section checks if C:\fsapps exists. If it does, it uses it as the default install directory.

DefaultDirName={pf32}\FMSC\{#APP}
DefaultGroupName=FMSC\{#APP}

;DefaultGroupName=FMSC Software\Cruise Processing Program
AllowNoIcons=yes
AllowUNCPath=no
LicenseFile=FMSC EULA.txt
InfoBeforeFile=Intro.txt
;OutputDir=\Output
OutputBaseFilename=CheckCruiseV2_{#VERSION}_Setup
OutputManifestFile=Setup-Manifest.txt
SetupIconFile=Setup.ico
Compression=lzma
SolidCompression=yes
PrivilegesRequired=none

[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Files]
Source: ..\CheckCruise\bin\Release\CheckCruise.*; DestDir: {app}; Flags: ignoreversion
Source: ..\CheckCruise\bin\Release\*.dll; DestDir: {app}; Flags: ignoreversion
Source: ..\CheckCruise\bin\Release\*.pdb; DestDir: {app}; Flags: ignoreversion
Source: ..\CheckCruise\bin\Release\x86\*.dll; DestDir: {app}/x86; Flags: ignoreversion
Source: ..\CheckCruise\bin\Release\x64\*.dll; DestDir: {app}/x64; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: {group}\Check Cruise; Filename: {app}\CheckCruise.exe
Name: {userdesktop}\Check Cruise; Filename: {app}\CheckCruise.exe; Tasks: desktopicon

[Run]
Filename: "{app}\CheckCruise.exe"; Description: "{cm:LaunchProgram,Check Cruise Program}"; Flags: nowait postinstall skipifsilent
