using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace Base64Decoder
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private EditText memoData = null;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_git:
                    ShowGitPage();
                    return true;
                case Resource.Id.menu_share:
                    ShareText();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
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
            Button btnCopy = FindViewById<Button>(Resource.Id.buttonCopy);
            Button btnPaste = FindViewById<Button>(Resource.Id.buttonPaste);
            memoData = FindViewById<EditText>(Resource.Id.memoValue);

            CheckBox cbCompress = FindViewById<CheckBox>(Resource.Id.checkBoxCompress);
            RadioGroup gbCompLevel = FindViewById<RadioGroup>(Resource.Id.rgEncodeType);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinnerCompLevel);

            cbCompress.Checked = true;

            btnEncrypt.Click += delegate
            {
                int comp = Int32.Parse(spinner.SelectedItem.ToString());
                memoData.Text = DecoderUtil.EncriptText(memoData.Text, cbCompress.Checked, gbCompLevel.CheckedRadioButtonId, comp);
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

            btnCopy.Click += delegate
            {
                // Open the clipboard
                var clipboard = (ClipboardManager) GetSystemService(ClipboardService);
                // Set clipboard text
                clipboard.Text = memoData.Text;

                Toast.MakeText(this, Resource.String.copy_done, ToastLength.Long).Show();
            };

            btnPaste.Click += delegate
            {
                var clipboard = (ClipboardManager)GetSystemService(ClipboardService);
                if (clipboard.HasPrimaryClip)
                {
                    //since the clipboard contains plain text.
                    var item = clipboard.PrimaryClip.GetItemAt(0);
                    // Gets the clipboard as text.
                    memoData.Text = item.Text;

                    Toast.MakeText(this, Resource.String.paste_done, ToastLength.Long).Show();
                }
            };

            // drop down list (ComboBox)
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.comp_values, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private void ShowGitPage()
        {
            var uri = Android.Net.Uri.Parse("https://goo.gl/iHxzZc");
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        private void ShareText()
        {
            // send as Text Message (SMS)
            Intent intentsend = new Intent();
            intentsend.SetAction(Intent.ActionSend);
            intentsend.PutExtra(Intent.ExtraText, memoData.Text);
            intentsend.SetType("text/plain");
            StartActivity(Intent.CreateChooser(intentsend, GetText(Resource.String.share_by) ));
        }

    }
}

