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
					hdr = SbTarGnuHeader.ReadFromBuffer(hdrBytes);

				var hdr =  new SbTarUstarHeader();
				hdr.OffsetInFile = initPosn;
				hdr.Name     = hdrBytes.ToStringFromBytes(000, 100);
				hdr.Mode     = hdrBytes.ToStringFromBytes(100, 008);
				hdr.Uid      = hdrBytes.ToStringFromBytes(108, 008);
				hdr.Gid      = hdrBytes.ToStringFromBytes(116, 008);
				hdr.Size     = hdrBytes.ToStringFromBytes(124, 012);
				hdr.Mtime    = hdrBytes.ToStringFromBytes(136, 012);
				hdr.Chksum   = hdrBytes.ToStringFromBytes(148, 008);
				hdr.TypeFlag = hdrBytes.ToStringFromBytes(156, 001);
				hdr.LinkName = hdrBytes.ToStringFromBytes(157, 100);

				hdr.Magic    = tarStream.ReadString(6);
				hdr.Version  = tarStream.ReadString(2);
				hdr.Uname    = tarStream.ReadString(32);
				hdr.Gname    = tarStream.ReadString(32);
				hdr.DevMajor = tarStream.ReadString(8);
				hdr.DevMinor = tarStream.ReadString(8);
				hdr.Prefix   = tarStream.ReadString(155);
				hdr.Pad      = hdrBytes.ToStringFromBytes(500, 012);

				if (string.IsNullOrEmpty(hdr.Name) && string.IsNullOrEmpty(hdr.Magic))
					break;

				if (hdr.Magic != "ustar")
					throw new FormatException($"This file had an unexpected magic-value. Expected \"ustar\", found \"{hdr.Magic}\".");

				tarStream.Position += 12;
				tarStream.Position += Convert.ToInt32(hdr.Size, 8);

				var skip = (512 - (tarStream.Position % 512)) % 512;
        tarStream.Position += skip;

				sbt.Headers.Add(hdr);

			} while (tarStream.Position < tarStream.Length);

			return sbt;
		}

	}
}
