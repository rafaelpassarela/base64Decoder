object frmBase64Decode: TfrmBase64Decode
  Left = 0
  Top = 0
  Caption = 'Base64Decode'
  ClientHeight = 430
  ClientWidth = 588
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  DesignSize = (
    588
    430)
  PixelsPerInch = 96
  TextHeight = 13
  object MemoTexto: TMemo
    Left = 8
    Top = 8
    Width = 420
    Height = 355
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 0
    OnChange = MemoTextoChange
  end
  object ButtonEncode: TButton
    Left = 8
    Top = 369
    Width = 129
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = '&Encode'
    TabOrder = 1
    OnClick = ButtonEncodeClick
  end
  object ButtonDecode: TButton
    Left = 143
    Top = 369
    Width = 129
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = '&Decode'
    TabOrder = 2
    OnClick = ButtonDecodeClick
  end
  object CheckBox1: TCheckBox
    Left = 278
    Top = 369
    Width = 150
    Height = 17
    Anchors = [akLeft, akBottom]
    Caption = 'Compressed String ?'
    TabOrder = 3
  end
  object Button1: TButton
    Left = 451
    Top = 369
    Width = 129
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Decode &To DataSet'
    TabOrder = 4
    OnClick = Button1Click
  end
  object ButtonCompress: TButton
    Left = 8
    Top = 400
    Width = 129
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = '&Compress'
    TabOrder = 5
    OnClick = ButtonCompressClick
  end
  object ButtonDescompress: TButton
    Left = 143
    Top = 400
    Width = 129
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Decompres&s'
    TabOrder = 6
    OnClick = ButtonDescompressClick
  end
  object MemoInt: TMemo
    Left = 434
    Top = 8
    Width = 146
    Height = 355
    ScrollBars = ssVertical
    TabOrder = 7
  end
end
