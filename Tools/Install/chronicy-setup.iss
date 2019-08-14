; Installers directory
#define InstallerSource "Output"

#define ApplicationName "Chronicy"
; TODO: Move to a file
#define Version "1.0.0"

#define ServiceInstallerName "Chronicy.Service-" + Version + ".exe"
#define ExcelInstallerName "Chronicy.Excel-" + Version + ".exe"

#define InstallerDestination "{app}\Install"

#define ServiceInstallerRunLocation InstallerDestination + "\" + ServiceInstallerName
#define ExcelInstallerRunLocation InstallerDestination + "\" + ExcelInstallerName

[Setup]
AppName={#ApplicationName}
AppVersion={#Version}
DefaultDirName="{commonpf}\Chronicy"
OutputBaseFilename={#ApplicationName}-{#Version}

; The service installer requires Administrator rights
PrivilegesRequired=admin

ArchitecturesAllowed=x86 x64
ArchitecturesInstallIn64BitMode=x64

[Components]
Name: "service"; Description: "Chronicy Tracking Service"; Types: full compact custom; Flags: fixed
Name: "excel"; Description: "Microsoft Excel Extension"; Types: full

[Files]
Source: "{#InstallerSource}\{#ServiceInstallerName}"; DestDir: {#InstallerDestination}; Components: service; AfterInstall: RunOtherInstaller('{#ServiceInstallerRunLocation}')
Source: "{#InstallerSource}\{#ExcelInstallerName}"; DestDir: {#InstallerDestination}; Components: excel; AfterInstall: RunOtherInstaller('{#ExcelInstallerRunLocation}')

[Code]
procedure RunOtherInstaller(path: String);
var
  ResultCode: Integer;
begin
  if not Exec(ExpandConstant(path), '', '', SW_SHOWNORMAL,
    ewWaitUntilTerminated, ResultCode)
  then
    MsgBox('Other installer failed to run!' + #13#10 +
      SysErrorMessage(ResultCode), mbError, MB_OK);
end;