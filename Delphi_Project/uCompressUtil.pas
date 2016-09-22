unit uCompressUtil;

interface

uses
  Classes, SysUtils, Windows, Types, Controls, Messages, ZLib, IdCoderMIME,
  IdHashMessageDigest, IdHash;

type
  Algorithm = class
  public
    class procedure StreamCompression(const AInStream : TStream; const AOutStream : TStream);
    class procedure StreamDecompression(const AInStream : TStream; const AOutStream : TStream);
    class procedure Base64Decode(const ABase64String : string; const OutStream : TStream); overload;
    class procedure Base64Decode(const ABase64String : string; var OutString : string); overload;
    class procedure Base64Encode(const AInStream : TStream; var OutString : string); overload;
    class procedure Base64Encode(const AInString : string; var OutString : string); overload;
    class function Base64CompressedString(const AString : string): string; overload;
    class function Base64CompressedString(const AStream : TStream): string; overload;
    class function Base64DecompressedString(const AString : string): string; overload;
    class procedure Base64DecompressedString(const AString : string; const AStream : TStream); overload;
    class function MD5FromFile(const AFileName : TFileName) : string;
    class function MD5FromString(const AString : string) : string;
  end;

implementation

{ Algorithm }

class procedure Algorithm.Base64Decode(const ABase64String : string; const OutStream : TStream);
var
  lDecode: TIdDecoderMIME;
begin
  OutStream.Size := 0;
  OutStream.Position := 0;

  // USING INDY ROUTINE
  lDecode := TIdDecoderMIME.Create;
  try
    lDecode.DecodeBegin(OutStream);
    lDecode.Decode(ABase64String);
    lDecode.DecodeEnd;

    OutStream.Position := 0;
  finally
    FreeAndNil(lDecode);
  end;
// USING SOAP ROUTINE
//  lStrStream := TStringStream.Create(AString);
//  try
//    lStrStream.Position := 0;
//    DecodeStream(lStrStream, FStream);
//    FStream.Position := 0;
//  finally
//    FreeAndNil(lStrStream);
//  end;

// USING OLD ROUTINE
//  lTempString := Base64Decode(AString);
//
//  lStrStream := TStringStream.Create(lTempString);
//  try
//    lStrStream.Position := 0;
//
//    FStream.CopyFrom(lStrStream, lStrStream.Size);
//    FStream.Position := 0;
//  finally
//    lStrStream.Free;
//  end;
end;

class procedure Algorithm.Base64Encode(const AInStream: TStream;
  var OutString: string);
var
  lEncode: TIdEncoderMIME;
begin
  AInStream.Position := 0;

  // USING INDY ROUTINE
  lEncode := TIdEncoderMIME.Create;
  try
    OutString := lEncode.Encode(AInStream);
    AInStream.Position := 0;
  finally
    FreeAndNil(lEncode);
  end;

// USING SOAP BPL ROUTINE
//  lStrStream := TStringStream.Create;
//  try
//    EncodeStream(FStream, lStrStream);
//    lStrStream.Position := 0;
//    Result := lStrStream.DataString;
//  finally
//    FreeAndNil(lStrStream);
//  end;

// OLD ROUTINE
//  lStrStream := TStringStream.Create('');
//  try
//    FStream.Position := 0;
//
//    lStrStream.CopyFrom(FStream, FStream.Size);
//    lStrStream.Position := 0;
//
    // encode to base64
//    Result := Base64Encode(lStrStream.DataString);
//
//    FStream.Position := 0;
//  finally
//    lStrStream.Free;
//  end;

end;

class function Algorithm.Base64CompressedString(const AString : string): string;
var
  lInputStream: TStringStream;
  lOutputStream: TStringStream;
begin
  lInputStream := TStringStream.Create(AString);
  lOutputStream := TStringStream.Create('');
  try
    Algorithm.StreamCompression(lInputStream, lOutputStream);
    Algorithm.Base64Encode(lOutputStream, Result);
  finally
    lInputStream.Free;
    lOutputStream.Free;
  end;
end;

class function Algorithm.Base64CompressedString(const AStream: TStream): string;
var
  lBuffer: string;
  lStrStream: TStringStream;
begin
  AStream.Position := 0;

  lStrStream := TStringStream.Create('');
  try
    lStrStream.CopyFrom(AStream, AStream.Size);
    lStrStream.Position := 0;
    lBuffer := lStrStream.DataString;
    AStream.Position := 0;
  finally
    lStrStream.Free;
  end;

  Result := Algorithm.Base64CompressedString(lBuffer);
end;

class procedure Algorithm.Base64Decode(const ABase64String: string;
  var OutString: string);
var
  lStrStream: TStringStream;
begin
  lStrStream := TStringStream.Create('');
  try
    Base64Decode(ABase64String, lStrStream);
    lStrStream.Position := 0;
    OutString := lStrStream.DataString;
  finally
    lStrStream.Free;
  end;
end;

class procedure Algorithm.Base64DecompressedString(const AString: string;
  const AStream: TStream);
var
  lBuffer: string;
  lStrStream: TStringStream;
begin
  lBuffer := Algorithm.Base64DecompressedString(AString);

  lStrStream := TStringStream.Create(lBuffer);
  try
    lStrStream.Position := 0;

    AStream.Size := 0;
    AStream.CopyFrom(lStrStream, lStrStream.Size);
    AStream.Position := 0;
  finally
    lStrStream.Free;
  end;
end;

class function Algorithm.Base64DecompressedString(const AString : string):
    string;
var
  lInputStream: TStringStream;
  lOutputStream: TStringStream;
begin
  lInputStream := TStringStream.Create('');
  lOutputStream := TStringStream.Create('');
  try
    Algorithm.Base64Decode(AString, lInputStream);
    Algorithm.StreamDecompression(lInputStream, lOutputStream);
    Result := lOutputStream.DataString;
  finally
    lInputStream.Free;
    lOutputStream.Free;
  end;
end;

class procedure Algorithm.Base64Encode(const AInString: string;
  var OutString: string);
var
  lStrStream: TStringStream;
begin
  lStrStream := TStringStream.Create(AInString);
  try
    lStrStream.Position := 0;
    Base64Encode(lStrStream, OutString);
  finally
    lStrStream.Free;
  end;
end;

class function Algorithm.MD5FromFile(const AFileName: TFileName): string;
var
  lIdMD5 : TIdHashMessageDigest5;
  lFileStream : TFileStream;
  lRet: T4x4LongWordRecord;
begin
  Result := '';
  if FileExists(AFileName) then
  begin
    lIdMD5 := TIdHashMessageDigest5.Create;
    lFileStream := TFileStream.Create(AFileName, fmOpenRead or fmShareDenyWrite);
    try
      lRet := lIdMD5.HashValue(lFileStream);
      Result := lIdMD5.AsHex(lRet);
    finally
      lFileStream.Free;
      lIdMD5.Free;
    end;
  end;
end;

class function Algorithm.MD5FromString(const AString: string): string;
var
  lIdMD5 : TIdHashMessageDigest5;
begin
  Result := '';
  lIdMD5 := TIdHashMessageDigest5.Create;
  try
    Result := lIdMD5.AsHex(lIdMD5.HashValue(AString));
//    Result := lIdMD5.HashStringAsHex(AString, TEncoding.Default);
  finally
    lIdMD5.Free;
  end;
end;

class procedure Algorithm.StreamCompression(const AInStream,
  AOutStream: TStream);
var
  lSourceStream: TCompressionStream;
begin
  AOutStream.Size := 0;
  AOutStream.Position := 0;

  lSourceStream := TCompressionStream.Create(clDefault, AOutStream);
  try
    AInStream.Position := 0;
    lSourceStream.CopyFrom(AInStream, AInStream.Size);
    AInStream.Position := 0;
  finally
    FreeAndNil(lSourceStream);
  end;

  AOutStream.Position := 0;
end;

class procedure Algorithm.StreamDecompression(const AInStream,
  AOutStream: TStream);
const
  BufferSize = 4096;
var
  lOriginalStream : TDecompressionStream;
  lBuffer: array[0..BufferSize-1] of Byte;
  lSize : Int64;
begin
  AInStream.Position := 0;

  AOutStream.Size := 0;
  AOutStream.Position := 0;

  lOriginalStream := TDecompressionStream.Create(AInStream);
  try
    lSize := 1;
    while lSize <> 0 do
    begin
      lSize := lOriginalStream.Read(lBuffer, BufferSize);
      AOutStream.Write(lBuffer, lSize);
    end;
    AOutStream.Position := 0;
  finally
    FreeAndNil(lOriginalStream);
  end;
end;
end.
