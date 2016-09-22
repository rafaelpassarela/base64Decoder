using Android.App;
using Android.Widget;
using Android.OS;
using System.Text;
using System.IO;
using zlib;
using System;

namespace Base64Decoder
{
    [Activity(Label = "Base64Decoder", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];
            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] GetBytesEncoded(string value, int encType)
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

        public static string GetStringEncoded(byte[] value, int encType)
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

        private string EncriptText(string source, bool compress)
        {
            byte[] compressed;

            RadioGroup grp = FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            if (compress)
            {
                EditText edtLevel = FindViewById<EditText>(Resource.Id.editCompLevel);
                int level = Int32.Parse(edtLevel.Text);

                using (var outStream = new MemoryStream())
                {
                    byte[] text;

                    using (var tinyStream = new ZOutputStream(outStream, level))
                    {
                        text = GetBytesEncoded(source, grp.CheckedRadioButtonId);
                        tinyStream.Write(text, 0, text.Length);
                    }
                    compressed = outStream.ToArray();
                }
            }
            else
            {
                compressed = GetBytesEncoded(source, grp.CheckedRadioButtonId);
            }

            return Convert.ToBase64String(compressed);
        }

        private string DecriptText(string b64Source, bool compress)
        {
            string output = "";
            var source = Convert.FromBase64String(b64Source);

            RadioGroup grp = FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            if (compress)
            {
                using (var outStream = new MemoryStream())
                {
                    using (var tinyStream = new ZOutputStream(outStream))
                    {
                        tinyStream.Write(source, 0, source.Length);
                    }
                    output = GetStringEncoded(outStream.ToArray(), grp.CheckedRadioButtonId);
                }
            }
            else
            {
                output = GetStringEncoded(source, grp.CheckedRadioButtonId);
            }
            return output;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnEncrypt = FindViewById<Button>(Resource.Id.buttonEncrypt);
            Button btnDecrypt = FindViewById<Button>(Resource.Id.buttonDecrypt);
            Button btnClear = FindViewById<Button>(Resource.Id.buttonClear);
            EditText memoData = FindViewById<EditText>(Resource.Id.memoValue);
            CheckBox cbCompress = FindViewById<CheckBox>(Resource.Id.checkBoxCompress);

            cbCompress.Checked = true;

            btnEncrypt.Click += delegate
            {
                memoData.Text = EncriptText(memoData.Text, cbCompress.Checked);
            };

            btnDecrypt.Click += delegate
            {
                memoData.Text = DecriptText(memoData.Text, cbCompress.Checked);
            };

            btnClear.Click += delegate
            {
                memoData.Text = "";
            };
        }
    }
}

