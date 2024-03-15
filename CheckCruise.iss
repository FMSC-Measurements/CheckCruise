#define MsBuildOutputDir ".\CheckCruise\bin\Release\net472"
#define VERSION "2020.03.12"
#define APP "Check Cruise"
#define ORGANIZATION "U.S. Forest Service, Forest Management Service Center"

[Setup]

AppName={#APP}
AppID={#APP}
AppMutex=CheckCruise
AppVerName={#APP} version {#VERSION}
AppVersion={#VERSION}
AppPublisher={#ORGANIZATION}
AppPublisherURL=http://www.fs.fed.us/fmsc/measure/cruising/cruiseprocessing/index.php
AppSupportURL=http://www.fs.fed.us/fmsc/measure/support.shtml
AppUpdatesURL=http://www.fs.fed.us/fmsc/measure/cruising/cruiseprocessing/index.php
; CurPageChanged in the [Code] section checks if C:\fsapps exists. If it does, it uses it as the default install directory.

DefaultDirName={autopf}\FMSC\{#APP}
DefaultGroupName=FMSC\{#APP}

VersionInfoDescription={#APP} Setup
VersionInfoCompany={#ORGANIZATION}
VersionInfoVersion={#VERSION}

Compression=lzma
SolidCompression=yes

AllowNoIcons=yes
AllowUNCPath=no
SetupIconFile=Setup.ico
LicenseFile=FMSC EULA.txt
InfoBeforeFile=Intro.txt

PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog

OutputBaseFilename=CheckCruiseV2_{#VERSION}_Setup
OutputManifestFile=Setup-Manifest.txt

[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Files]
Source: "{#MsBuildOutputDir}\*.exe"; DestDir: {app}; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\*.dll"; DestDir: {app}; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\*.exe.config"; DestDir: {app}; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\runtimes\win-x64\native\*.dll"; DestDir: {app}\runtimes\win-x64\native; Flags: ignoreversion;
Source: "{#MsBuildOutputDir}\runtimes\win-x86\native\*.dll"; DestDir: {app}\runtimes\win-x86\native; Flags: ignoreversion;
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: {group}\Check Cruise; Filename: {app}\CheckCruise.exe
Name: {userdesktop}\Check Cruise; Filename: {app}\CheckCruise.exe; Tasks: desktopicon

[Run]
Filename: "{app}\CheckCruise.exe"; Description: "{cm:LaunchProgram,Check Cruise Program}"; Flags: nowait postinstall skipifsilent
