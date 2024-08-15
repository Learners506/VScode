; 脚本由 Inno Setup 脚本向导 生成！
; 有关创建 Inno Setup 脚本文件的详细资料请查阅帮助文档！

#define MyAppName "Hello"
#define MyAppVersion "1.0"
#define MyAppPublisher "ObjectARX编程站"
#define MyAppURL "http://www.objectarx.net/"
#define CAD2007 "SOFTWARE\Autodesk\AutoCAD\R17.0\ACAD-5001:804"
#define CAD2008 "SOFTWARE\Autodesk\AutoCAD\R17.1\ACAD-6001:804"
#define CAD2009 "SOFTWARE\Autodesk\AutoCAD\R17.2\ACAD-7001:804"
#define CAD2010 "SOFTWARE\Autodesk\AutoCAD\R18.0\ACAD-8001:804"
#define CAD2011 "SOFTWARE\Autodesk\AutoCAD\R18.1\ACAD-9001:804"
#define CAD2012 "SOFTWARE\Autodesk\AutoCAD\R18.2\ACAD-A001:804"
#define CAD2013 "SOFTWARE\Autodesk\AutoCAD\R19.0\ACAD-B001:804"
[Setup]
; 注: AppId的值为单独标识该应用程序。
; 不要为其他安装程序使用相同的AppId值。
; (生成新的GUID，点击 工具|在IDE中生成GUID。)
AppId={{9CA0065C-16AF-4393-BBC1-7407DCC733FD}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=F:\AutoCAD二次开发实例教程\Book\程序\1章\Chap01\Hello\bin\Debug
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"
[Registry]
Root: HKLM; Subkey: "C:\Applications\MoKaCADTools"; Flags:uninsdeletekey
[Files]
Source: "F:\AutoCAD二次开发实例教程\Book\程序\1章\Chap01\Hello\bin\Debug\Hello.dll"; DestDir: "{app}"; Flags: ignoreversion
; 注意: 不要在任何共享系统文件上使用“Flags: ignoreversion”

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[ISFormDesigner]
WizardForm=FF0A005457495A415244464F524D0030106903000054504630F10B5457697A617264466F726D0A57697A617264466F726D0C436C69656E74486569676874034C010B436C69656E74576964746803F1010D4578706C69636974576964746803F9010E4578706C69636974486569676874036E010D506978656C73506572496E636802600A54657874486569676874020C00F10A544E6577427574746F6E0A4E657874427574746F6E074F6E436C69636B07105F4E657874427574746F6E436C69636B0000F10C544E65774E6F7465626F6F6B0D4F757465724E6F7465626F6F6B00F110544E65774E6F7465626F6F6B506167650B57656C636F6D6550616765084E6578745061676507124175746F43414456657273696F6E506167650000F110544E65774E6F7465626F6F6B5061676509496E6E65725061676500F10C544E65774E6F7465626F6F6B0D496E6E65724E6F7465626F6F6B00F110544E65774E6F7465626F6F6B506167650B4C6963656E7365506167650C50726576696F75735061676507124175746F43414456657273696F6E506167650000F110544E65774E6F7465626F6F6B506167651453656C656374436F6D706F6E656E74735061676500F10C544E6577436F6D626F426F780A5479706573436F6D626F06486569676874021400000010544E65774E6F7465626F6F6B50616765124175746F43414456657273696F6E506167650743617074696F6E1211000000F78B0990E96200978189895BC58884764100750074006F0043004100440048722C670B4465736372697074696F6E1210000000F78B39686E63009781890990E9624100750074006F0043004100440048722C670C50726576696F757350616765070B57656C636F6D6550616765084E65787450616765070B4C6963656E7365506167650D4578706C69636974576964746802000E4578706C6963697448656967687402000010544E6577436865636B4C697374426F780C436865636B4C697374426F78044C656674022003546F70022605576964746803310106486569676874038100085461624F72646572020000000000F1065450616E656C094D61696E50616E656C00F10E544E65775374617469635465787414506167654465736372697074696F6E4C6162656C03546F70021D074F6E436C69636B0719506167654465736372697074696F6E4C6162656C436C69636B0B4578706C69636974546F70021D000000000000

[Code]
{ RedesignWizardFormBegin } // 不要删除这一行代码。
// 不要修改这一段代码，它是自动生成的。
var
  OldEvent_NextButtonClick: TNotifyEvent;
  AutoCADVersionPage: TWizardPage;
  CheckListBox: TNewCheckListBox;

procedure _NextButtonClick(Sender: TObject); forward;
procedure PageDescriptionLabelClick(Sender: TObject); forward;

procedure RedesignWizardForm;
begin
  { 创建自定义向导页面 }
  AutoCADVersionPage := CreateCustomPage(wpWelcome, '请选择需要安装的AutoCAD版本', '请根据需要选择AutoCAD版本');

  with WizardForm.NextButton do
  begin
    OldEvent_NextButtonClick := OnClick;
    OnClick := @_NextButtonClick;
  end;

  { AutoCADVersionPage }
  with AutoCADVersionPage.Surface do
  begin
    Name := 'AutoCADVersionPage';
  end;

  { CheckListBox }
  CheckListBox := TNewCheckListBox.Create(WizardForm);
  with CheckListBox do
  begin
    Name := 'CheckListBox';
    Parent := AutoCADVersionPage.Surface;
    Left := ScaleX(32);
    Top := ScaleY(38);
    Width := ScaleX(305);
    Height := ScaleY(129);
  end;

  CheckListBox.TabOrder := 0;

  with WizardForm.PageDescriptionLabel do
  begin
    OnClick := @PageDescriptionLabelClick;
    Top := ScaleY(29);
  end;

{ ReservationBegin }
  // 这一部分是提供给你的，你可以在这里输入一些补充代码。
   CheckListBox.AddGroup('当前系统已安装的AutoCAD版本:', '', 0, nil);
   if RegvalueExists(HKLM, '{#CAD2007}','AcadLocation') then
   begin
      CheckListBox.AddCheckBox('AutoCAD 2007', '', 0, False, True, False, True, nil);
    end
   if RegvalueExists(HKLM, '{#CAD2008}','AcadLocation') then
   begin
      CheckListBox.AddCheckBox('AutoCAD 2008', '', 0, False, True, False, True, nil);
    end
       if RegvalueExists(HKLM, '{#CAD2009}','AcadLocation') then
   begin
      CheckListBox.AddCheckBox('AutoCAD 2009', '', 0, False, True, False, True, nil);
    end
       if RegvalueExists(HKLM, '{#CAD2010}','AcadLocation') then
   begin
      CheckListBox.AddCheckBox('AutoCAD 2010', '', 0, False, True, False, True, nil);
    end
       if RegvalueExists(HKLM, '{#CAD2011}','AcadLocation') then
   begin
      CheckListBox.AddCheckBox('AutoCAD 2011', '', 0, False, True, False, True, nil);
    end
       if RegvalueExists(HKLM, '{#CAD2012}','AcadLocation') then
   begin
      CheckListBox.AddCheckBox('AutoCAD 2012', '', 0, False, True, False, True, nil);
    end
       if RegvalueExists(HKLM, '{#CAD2013}','AcadLocation') then
   begin
      CheckListBox.AddCheckBox('AutoCAD 2013', '', 0, False, True, False, True, nil);
    end
{ ReservationEnd }
end;
// 不要修改这一段代码，它是自动生成的。
{ RedesignWizardFormEnd } // 不要删除这一行代码。

procedure _NextButtonClick(Sender: TObject);
begin
  OldEvent_NextButtonClick(Sender);
end;

procedure PageDescriptionLabelClick(Sender: TObject);
begin

end;

procedure InitializeWizard();
begin
  RedesignWizardForm;
end;











