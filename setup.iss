#define MyAppName "NetCheck"
#define MyAppDescription "A simple internet connectivity monitoring application"
#define MyAppVersion "5.0.1.0"
#define MyAppPublisher "Caprine Logic"
#define MyAppExeName "netcheck.exe"
#define MyAppCopyright "Copyright (C) 2020-2025 Caprine Logic"

[Setup]
AppId={{516638A0-35EC-49B9-A711-2CED2C25A8C7}
AppMutex=NetCheckMutex
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppCopyright={#MyAppCopyright}
VersionInfoVersion={#MyAppVersion}
DefaultDirName={autoappdata}\{#MyAppPublisher}\{#MyAppName}
DisableDirPage=yes
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
AllowNoIcons=yes
OutputDir=.\bin
OutputBaseFilename=netcheck_setup
SetupIconFile=.\resources\icons\icon.ico
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
WizardResizable=no
ArchitecturesAllowed=x64compatible
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}
ShowTasksTreeLines=True
AlwaysShowDirOnReadyPage=True
VersionInfoCompany={#MyAppPublisher}
VersionInfoCopyright={#MyAppCopyright}
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}
VersionInfoProductTextVersion={#MyAppVersion}
VersionInfoDescription={#MyAppDescription}

[Code]
function FromUpdate: Boolean;
begin
	Result := ExpandConstant('{param:update|no}') = 'yes'
end;

function FromNormal: Boolean;
begin
	Result := FromUpdate = False
end;

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";
Name: startup; Description: "Run on Startup"

[Files]
Source: ".\bin\MinSizeRel\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "{#MyAppName}"; ValueData: "{app}\{#MyAppExeName}"; Tasks: startup; Flags: uninsdeletevalue

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent; Check: FromUpdate
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent unchecked; Check: FromNormal

[UninstallDelete]
Type: filesandordirs; Name: "{app}"
