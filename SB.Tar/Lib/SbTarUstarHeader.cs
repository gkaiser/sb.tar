using System;
using System;

namespace SB.Tar.Lib
{
	public class SbTarUstarHeader : SbTarHeader
	{
		private SbTarUstarHeader(long offsetInFile) 
		{ 
			this.OffsetInFile = offsetInFile; 
		}

		public string Magic;       /* @ 257 */
		public string Version;     /* @ 263 */
		public string Uname;       /* @ 265 */
		public string Gname;       /* @ 297 */
		public string DevMajor;    /* @ 329 */
		public string DevMinor;    /* @ 337 */
		public string Prefix;      /* @ 345 */

		public static SbTarUstarHeader ReadFromBuffer(byte[] buff, long offsetInFile)
		{
			var hdr = new SbTarUstarHeader(offsetInFile);

			hdr.Name     = buff.ToStringFromBytes(000, 100);
			hdr.Mode     = buff.ToStringFromBytes(100, 008);
			hdr.Uid      = buff.ToStringFromBytes(108, 008);
			hdr.Gid      = buff.ToStringFromBytes(116, 008);
			hdr.Size     = buff.ToStringFromBytes(124, 012);
			hdr.Mtime    = buff.ToStringFromBytes(136, 012);
			hdr.Chksum   = buff.ToStringFromBytes(148, 008);
			hdr.TypeFlag = buff.ToStringFromBytes(156, 001);
			hdr.LinkName = buff.ToStringFromBytes(157, 100);
			hdr.Magic    = buff.ToStringFromBytes(257, 006);
			hdr.Version  = buff.ToStringFromBytes(263, 002);
			hdr.Uname    = buff.ToStringFromBytes(265, 032);
			hdr.Gname    = buff.ToStringFromBytes(297, 032);
			hdr.DevMajor = buff.ToStringFromBytes(329, 008);
			hdr.DevMinor = buff.ToStringFromBytes(337, 008);
			hdr.Prefix   = buff.ToStringFromBytes(345, 155);
			hdr.Pad      = buff.ToStringFromBytes(500, 012);

			return hdr;
		}

	}
}
