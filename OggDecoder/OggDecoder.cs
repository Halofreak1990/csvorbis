using System;
using System.IO;

namespace OggDecoder
{
	/// <summary>
	/// Ogg Vorbis decoder test application.
	/// </summary>
	class Decoder
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		//[STAThread]
		static void Main(string[] args) 
		{
			TextWriter s_err = Console.Error;
			FileStream input = null, output = null;

			if (args.Length != 2)
			{
				return;
			}

			try
			{
				input = new FileStream(args[0], FileMode.Open, FileAccess.Read);
				output = new FileStream(args[1], FileMode.OpenOrCreate);

				OggDecodeStream decode = new OggDecodeStream(input, true);

				byte[] buffer = new byte[4096];
				int read;

				while ((read = decode.Read(buffer, 0, buffer.Length)) > 0)
				{
					output.Write(buffer, 0, read);
				}
			}
			catch (Exception e)
			{
				s_err.WriteLine(e);
			}
			finally
			{
				// make sure the files are always closed

				if (input != null)
				{
					input.Dispose();
				}

				if (output != null)
				{
					output.Dispose();
				}
			}
		}
	}
}
