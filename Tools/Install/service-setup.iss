#define ApplicationName "Chronicy"
#define Version "3.0.0"
#define ServiceName "Chronicy.Service"
#define ServiceDisplayName "Chronicy Tracking Service"

#define ProjectPath "..\..\Windows\Chronicy\Chronicy.Service\bin\Release"
#define ExecutableName "Chronicy.Service.exe"

[Setup]
AppName={#ApplicationName}
AppVersion={#Version}
DefaultDirName={commonpf}\Chronicy
PrivilegesRequired=admin

[Files]
Source: "{#ProjectPath}\*" ; DestDir: {app} ; Excludes: "*.pdb,*.xml,*.config" ; Flags: ignoreversion recursesubdirs

[Run]
Filename: {sys}\sc.exe; Parameters: "create {#ServiceName} start= auto binPath= ""{app}\{#ExecutableName}"" DisplayName= ""{#ServiceDisplayName}""" ; Flags: runhidden

[UninstallRun]
Filename: {sys}\sc.exe; Parameters: "stop {#ServiceName}" ; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "delete {#ServiceName}" ; Flags: runhidden