using Android.App;
using Android.Widget;
using Android.OS;
using System.Text;
using System.IO;
using System.IO.Compression;
using zlib;

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

		private string EncriptText(string source, bool compress)
		{
			byte[] compressed;
			string output;

			if (compress){
				using (var outStream = new MemoryStream())
				{
					using (var tinyStream = new GZipStream(outStream, CompressionMode.Compress))
					using (var mStream = new MemoryStream(Encoding.UTF8.GetBytes(source)))
						mStream.CopyTo(tinyStream);

					compressed = outStream.ToArray();
				}
			}
			else {
				compressed = Encoding.UTF7.GetBytes(source);
			}

			var plainTextBytes = Encoding.UTF7.GetBytes(Encoding.UTF7.GetString(compressed));
			return System.Convert.ToBase64String(plainTextBytes);

			//return Encoding.UTF8.GetString(compressed);



			// “compressed” now contains the compressed string.
			// Also, all the streams are closed and the above is a self-contained operation.

			using (var inStream = new MemoryStream(compressed))
			using (var bigStream = new GZipStream(inStream, CompressionMode.Decompress))
			using (var bigStreamOut = new MemoryStream())
			{
				bigStream.CopyTo(bigStreamOut);
				output = Encoding.UTF8.GetString(bigStreamOut.ToArray());
			}

			return output;
		}

		private string DecriptText(string source)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(source);
			using (var msi = new MemoryStream(bytes))
			using (var mso = new MemoryStream())
			{
				using (var gs = new GZipStream(msi, CompressionMode.Decompress))
				{
					//gs.CopyTo(mso);
					CopyTo(gs, mso);
				}

				return Encoding.UTF8.GetString(mso.ToArray());
			}
		}
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnEncript = FindViewById<Button>(Resource.Id.buttonEncript);
			Button btnDecript = FindViewById<Button>(Resource.Id.buttonDecript);
			EditText memoData = FindViewById<EditText>(Resource.Id.memoValue);
			CheckBox cbCompress = FindViewById<CheckBox>(Resource.Id.checkBox1);

			btnEncript.Click += delegate
			{
				memoData.Text = EncriptText(memoData.Text, cbCompress.Checked);
			};

			btnDecript.Click += delegate
			{
				memoData.Text = "Decript";
			};
		}
	}
}

