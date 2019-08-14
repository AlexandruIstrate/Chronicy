#define ApplicationName "Chronicy Tracking Service"
#define Version "1.0.0"
#define ServiceName "Chronicy.Service"
#define ServiceDisplayName "Chronicy Tracking Service"

#define ProjectPath "..\..\Windows\Chronicy\Chronicy.Service\bin\Release"
#define ExecutableName "Chronicy.Service.exe"

#define InstallerFileName "Chronicy.Service"

[Setup]
AppName={#ApplicationName}
AppVersion={#Version}
AppId={{5E204208-6FD9-4FD8-AA99-126EECB71868}}
DefaultDirName={commonpf}\Chronicy\Chronicy.Service
OutputBaseFilename={#InstallerFileName}-{#Version}
PrivilegesRequired=admin

ArchitecturesAllowed=x86 x64
ArchitecturesInstallIn64BitMode=x64

[Files]
Source: "{#ProjectPath}\*" ; DestDir: {app} ; Excludes: "*.pdb,*.xml,*.config" ; Flags: ignoreversion recursesubdirs

[Run]
Filename: {sys}\sc.exe; Parameters: "create {#ServiceName} start= auto binPath= ""{app}\{#ExecutableName}"" DisplayName= ""{#ServiceDisplayName}""" ; Flags: runhidden

[UninstallRun]
Filename: {sys}\sc.exe; Parameters: "stop {#ServiceName}" ; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "delete {#ServiceName}" ; Flags: runhidden