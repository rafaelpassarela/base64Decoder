using Android.App;
using Android.Widget;
using Android.OS;
using System.Text;
using System.IO;
using zlib;
using System;

namespace Base64Decoder
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
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

        private string EncriptText(string source, bool compress, int compType, int compLevel)
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

        private string DecriptText(string b64Source, bool compress, int compType)
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
            RadioGroup gbCompLevel = FindViewById<RadioGroup>(Resource.Id.rgEncodeType);
            EditText edtLevel = FindViewById<EditText>(Resource.Id.editCompLevel);

            cbCompress.Checked = true;

            btnEncrypt.Click += delegate
            {
                memoData.Text = EncriptText(memoData.Text, cbCompress.Checked, gbCompLevel.CheckedRadioButtonId, Int32.Parse(edtLevel.Text));
            };

            btnDecrypt.Click += delegate
            {
                try
                {
                    memoData.Text = DecriptText(memoData.Text, cbCompress.Checked, gbCompLevel.CheckedRadioButtonId);
                }
                catch (Exception e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Long).Show();
                }

            };

            btnClear.Click += delegate
            {
                memoData.Text = "";
            };
        }
    }
}

