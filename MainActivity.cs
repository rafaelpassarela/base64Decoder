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
                memoData.Text = DecoderUtil.EncriptText(memoData.Text, cbCompress.Checked, gbCompLevel.CheckedRadioButtonId, Int32.Parse(edtLevel.Text));
            };

            btnDecrypt.Click += delegate
            {
                try
                {
                    memoData.Text = DecoderUtil.DecriptText(memoData.Text, cbCompress.Checked, gbCompLevel.CheckedRadioButtonId);
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

