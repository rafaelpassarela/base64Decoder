unit uFormBase64Decode;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, uCompressUtil;

type
  TfrmBase64Decode = class(TForm)
    MemoTexto: TMemo;
    ButtonEncode: TButton;
    ButtonDecode: TButton;
    CheckBox1: TCheckBox;
    Button1: TButton;
    ButtonCompress: TButton;
    ButtonDescompress: TButton;
    MemoInt: TMemo;
    procedure ButtonEncodeClick(Sender: TObject);
    procedure ButtonDecodeClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure ButtonCompressClick(Sender: TObject);
    procedure ButtonDescompressClick(Sender: TObject);
    procedure MemoTextoChange(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmBase64Decode: TfrmBase64Decode;

implementation

uses uFormBase64DecodeDataSet;

{$R *.dfm}

procedure TfrmBase64Decode.Button1Click(Sender: TObject);
begin
  TfrmBase64DecodeDataSet.Decode(MemoTexto.Text);
end;

procedure TfrmBase64Decode.ButtonDescompressClick(Sender: TObject);
var
  lOut : TStringStream;
  lStr : TStringStream;
begin
  lOut := TStringStream.Create('');
  lStr := TStringStream.Create(MemoTexto.Text);
  try
    Algorithm.StreamDecompression(lStr, lOut);
    MemoTexto.Lines.LoadFromStream(lOut);
  finally
    FreeAndNil( lStr );
    FreeAndNil( lOut );
  end;
end;

procedure TfrmBase64Decode.ButtonCompressClick(Sender: TObject);
var
  lOut : TStringStream;
  lStr : TStringStream;
begin
  lOut := TStringStream.Create('');
  lStr := TStringStream.Create(MemoTexto.Text);
  try
    Algorithm.StreamCompression(lStr, lOut);
    MemoTexto.Lines.LoadFromStream(lOut);
  finally
    FreeAndNil( lStr );
    FreeAndNil( lOut );
  end;
end;

procedure TfrmBase64Decode.ButtonDecodeClick(Sender: TObject);
var
  lOut : string;
begin
  if CheckBox1.Checked then
    lOut := Algorithm.Base64DecompressedString(MemoTexto.Text)
  else
    Algorithm.Base64Decode(MemoTexto.Text, lOut);

  MemoTexto.Text := lOut;
end;

procedure TfrmBase64Decode.ButtonEncodeClick(Sender: TObject);
var
  lOut : string;
begin
  if CheckBox1.Checked then
    lOut := Algorithm.Base64CompressedString(MemoTexto.Text)
  else
    Algorithm.Base64Encode(MemoTexto.Text, lOut);
    
  MemoTexto.Text := lOut;
end;

procedure TfrmBase64Decode.MemoTextoChange(Sender: TObject);
var
  i: Integer;
begin
  MemoInt.Clear;
  for i := 1 to Length(MemoTexto.Text) do
  begin
    MemoInt.Lines.Add( IntToStr(Ord(MemoTexto.Text[i])) );
  end;
end;

end.
