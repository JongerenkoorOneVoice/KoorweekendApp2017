using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace KoorweekendApp2017
{
	public static class ImageHelper
	{
		public static String MediaFileToBase64(MediaFile mediaFile)
		{
			byte[] bytes = null;
			using (MemoryStream ms = new MemoryStream())
			{
				mediaFile.Source.CopyTo(ms);
				bytes = ms.ToArray();

			}

			String base64Image = String.Empty;
			if (bytes != null)
			{
				base64Image = Convert.ToBase64String(bytes);
			}

			//	String.Empty if not set
			return base64Image;
		}

		public static MediaFile Base64ToMediaFile(String base64)
		{
			Byte[] bytes = Convert.FromBase64String(base64);
			MediaFile mediaFile = default(MediaFile);
			using (Stream stream = new MemoryStream(bytes))
			{
				mediaFile = new MediaFile("", () => { return stream; });
			}
			//ImageSource source = ImageSource.FromStream(() => { return stream; });
			return mediaFile;
		}

		public async static Task<MediaFile> ResizeMediaFile(byte[] bytes)
		{
			MediaFile mediaFile = default(MediaFile);
			try
			{
				
				Image newImage = new Image();
				Stream stream = new MemoryStream(bytes);
				newImage.Source = ImageSource.FromStream(() => { return stream; });



				String testString = String.Empty;
				var test = await newImage.ScaleTo(0.25);



				byte[] newImageBytes = new byte[testString.Length * sizeof(char)];
				Buffer.BlockCopy(testString.ToCharArray(), 0, newImageBytes, 0, newImageBytes.Length);


				Stream streams = new MemoryStream(newImageBytes);
				mediaFile = new MediaFile("", () => { return streams; });







			}

			catch (Exception ex)
			{
				var x = ex;


			}
			return mediaFile;
		}
	}
}
