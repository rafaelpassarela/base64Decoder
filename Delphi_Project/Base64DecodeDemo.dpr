program Base64DecodeDemo;

uses
  Forms,
  uFormBase64Decode in 'uFormBase64Decode.pas' {frmBase64Decode},
  uFormBase64DecodeDataSet in 'uFormBase64DecodeDataSet.pas' {frmBase64DecodeDataSet},
  uCompressUtil in 'uCompressUtil.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfrmBase64Decode, frmBase64Decode);
  Application.Run;
end.
