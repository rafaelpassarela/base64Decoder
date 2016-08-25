using Android.App;
using Android.Widget;
using Android.OS;

namespace Base64Decoder
{
	[Activity(Label = "Base64Decoder", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnEncript = FindViewById<Button>(Resource.Id.buttonEncript);
			Button btnDecript = FindViewById<Button>(Resource.Id.buttonDecript);
			TextView memoData = FindViewById<TextView>(Resource.Id.textViewData);

			btnEncript.Click += delegate {
				memoData.Text = "Encript";
			};

			btnDecript.Click += delegate
			{
				memoData.Text = "Decript";
			};
		}
	}
}

