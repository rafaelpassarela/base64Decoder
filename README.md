# base64Decoder
for Android

I've a Delphi app which generates some Json objects, in some cases as Base64Compressed. For do that, I'm using TCompressionStream
of ZLib. To integrate both Android App and Delphi App I also need to use a ZLib compression on Xamarin 

Input Char | Output  | Comp. Output
A          | QQ==    | eJxzBAAAQgBC