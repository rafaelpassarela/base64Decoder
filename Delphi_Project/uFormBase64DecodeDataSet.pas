unit uFormBase64DecodeDataSet;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, DBGrids, DB, DBClient, uCompressUtil, Grids;

type
  TfrmBase64DecodeDataSet = class(TForm)
    cdsDecode: TClientDataSet;
    dtsDecode: TDataSource;
    DBGrid1: TDBGrid;
    procedure cdsDecodeCalcFields(DataSet: TDataSet);
  private
    { Private declarations }
  public
    { Public declarations }

    class procedure Decode(const AEncodeString : string);
  end;

var
  frmBase64DecodeDataSet: TfrmBase64DecodeDataSet;

implementation

{$R *.dfm}

{ TfrmBase64DecodeDataSet }

procedure TfrmBase64DecodeDataSet.cdsDecodeCalcFields(DataSet: TDataSet);
var
  lField: TField;
  lStatus: string;
begin
  lField := cdsDecode.FindField('UpdateStatus');
  if (lField <> nil) and (lField.IsNull) then
  begin
    lStatus := '';
    case cdsDecode.UpdateStatus of
      usModified   : lStatus := 'Modificado';
      usInserted   : lStatus := 'Novo';
      usDeleted    : lStatus := 'Exclído';
    end;
    lField.Value := lStatus;
  end;
  
end;

class procedure TfrmBase64DecodeDataSet.Decode(const AEncodeString: string);
var
  lDecodeDataSetView: TfrmBase64DecodeDataSet;
  lOut : string;
  lField: TField;
  lDataSet: TClientDataSet;
  I: Integer;
begin
  Algorithm.Base64Decode(AEncodeString, lOut);
  lDecodeDataSetView := Self.Create(nil);
  try
    lDataSet := TClientDataSet.Create(nil);
    try
      lDataSet.XMLData := lOut;

      lField := TStringField.Create(lDecodeDataSetView);
      lField.FieldKind := fkInternalCalc;
      lField.FieldName := 'UpdateStatus';
      lField.DataSet := lDecodeDataSetView.cdsDecode;
      lField.Size := 30;
      lField.Visible := True;

      for I := 0 to lDataSet.FieldCount - 1 do
      begin
        lField                := TFieldClass(lDataSet.Fields[I].ClassType).Create(lDecodeDataSetView.cdsDecode);

        lField.FieldName      := lDataSet.Fields[I].FieldName;
        lField.DataSet        := lDecodeDataSetView.cdsDecode;
        lField.FieldKind      := lDataSet.Fields[I].FieldKind;
        lField.Size           := lDataSet.Fields[I].Size;
        lField.Visible        := True;
        lField.DisplayLabel   := Format( '%s [%s]', [lDataSet.Fields[I].FieldName, lDataSet.Fields[I].ClassName]);
      end;
//        CreateDataSetField(lDecodeDataSetView.cdsDecode, lDataSet.Fields[I].FieldKind,
//          , lDataSet.Fields[I].FieldName,
//          lDataSet.Fields[I].KeyFields, lDataSet.Fields[I].LookupKeyFields, '',
//          nil, lDataSet.Fields[I].DisplayName, lDataSet.Fields[I].Size, '', lDataSet.Fields[I].Visible);

      lDecodeDataSetView.cdsDecode.XMLData := lOut;
      lDecodeDataSetView.dtsDecode.DataSet := lDecodeDataSetView.cdsDecode;
      lDecodeDataSetView.ShowModal;
    finally
      FreeAndNil(lDataSet);
    end;
  finally
    FreeAndNil(lDecodeDataSetView);
  end;
end;

end.
