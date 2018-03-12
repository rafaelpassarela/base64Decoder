using System;
using System.IO;
using System.Text;
using zlib;

namespace Base64Decoder
{
    static public class DecoderUtil
    {
        static private byte[] GetBytesEncoded(string value, int encType)
        {
            switch (encType)
            {
                case Resource.Id.rbFmtASCII:
                    return Encoding.ASCII.GetBytes(value);
                case Resource.Id.rbFmtUtf8:
                    return Encoding.UTF8.GetBytes(value);
                case Resource.Id.rbFmtUtf32:
                    return Encoding.UTF32.GetBytes(value);
                case Resource.Id.rbFmtUnicode:
                    return Encoding.Unicode.GetBytes(value);
                default:
                    return Encoding.UTF7.GetBytes(value);
            }
        }

        static private string GetStringEncoded(byte[] value, int encType)
        {
            switch (encType)
            {
                case Resource.Id.rbFmtASCII:
                    return Encoding.ASCII.GetString(value);
                case Resource.Id.rbFmtUtf8:
                    return Encoding.UTF8.GetString(value);
                case Resource.Id.rbFmtUtf32:
                    return Encoding.UTF32.GetString(value);
                case Resource.Id.rbFmtUnicode:
                    return Encoding.Unicode.GetString(value);
                default:
                    return Encoding.UTF7.GetString(value);
            }
        }

        static public string DecriptText(string b64Source, bool compress, int compType)
        {
            string output = "";
            var source = Convert.FromBase64String(b64Source);

            if (compress)
            {
                using (var outStream = new MemoryStream())
                {
                    using (var tinyStream = new ZOutputStream(outStream))
                    {
                        tinyStream.Write(source, 0, source.Length);
                    }
                    output = GetStringEncoded(outStream.ToArray(), compType);
                }
            }
            else
            {
                output = GetStringEncoded(source, compType);
            }
            return output;
        }

        static public string EncriptText(string source, bool compress, int compType, int compLevel)
        {
            byte[] compressed;

            if (compress)
            {
                using (var outStream = new MemoryStream())
                {
                    byte[] text;

                    using (var tinyStream = new ZOutputStream(outStream, compLevel))
                    {
                        text = GetBytesEncoded(source, compType);
                        tinyStream.Write(text, 0, text.Length);
                    }
                    compressed = outStream.ToArray();
                }
            }
            else
            {
                compressed = GetBytesEncoded(source, compType);
            }

            return Convert.ToBase64String(compressed);
        }
    }
}