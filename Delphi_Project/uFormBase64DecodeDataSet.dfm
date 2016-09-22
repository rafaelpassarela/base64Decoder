object frmBase64DecodeDataSet: TfrmBase64DecodeDataSet
  Left = 0
  Top = 0
  Caption = 'Decode DataSet View'
  ClientHeight = 243
  ClientWidth = 527
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object DBGrid1: TDBGrid
    Left = 0
    Top = 0
    Width = 527
    Height = 243
    Align = alClient
    DataSource = dtsDecode
    TabOrder = 0
    TitleFont.Charset = DEFAULT_CHARSET
    TitleFont.Color = clWindowText
    TitleFont.Height = -11
    TitleFont.Name = 'Tahoma'
    TitleFont.Style = []
  end
  object cdsDecode: TClientDataSet
    Aggregates = <>
    Params = <>
    OnCalcFields = cdsDecodeCalcFields
    Left = 152
    Top = 32
  end
  object dtsDecode: TDataSource
    Left = 168
    Top = 80
  end
end
