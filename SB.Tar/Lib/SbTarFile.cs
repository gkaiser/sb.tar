using System;
using System.Text;
using System.Collections.Generic;

namespace SB.Tar.Lib
{
	public class SbTarFile
	{
		public List<SbTarHeader> Headers = new List<SbTarHeader>();

		private SbTarFile() { }

		public static SbTarFile? FromFile(string fileName) => SbTarFile.FromStream(File.OpenRead(fileName));

		public static SbTarFile? FromStream(Stream tarStream)
		{
			var sbt = new SbTarFile();

			do
			{
				var initPosn = tarStream.Position;
				var hdrBytes = tarStream.ReadToBuff(512);
				var ustarCheck = hdrBytes.ToStringFromBytes(257, 7);

				SbTarHeader hdr = null;

				if (ustarCheck == "ustar")
					hdr = SbTarUstarHeader.ReadFromBuffer(hdrBytes, initPosn);
				else
					hdr = SbTarGnuHeader.ReadFromBuffer(hdrBytes, initPosn);

				if (string.IsNullOrEmpty(hdr.Name) && string.IsNullOrEmpty(hdr.Magic))
					break;

				tarStream.Position += Convert.ToInt32(hdr.Size, 8);

				var skip = (512 - (tarStream.Position % 512)) % 512;
        tarStream.Position += skip;

				sbt.Headers.Add(hdr);

			} while (tarStream.Position < tarStream.Length);

			return sbt;
		}

	}
}
