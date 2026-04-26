using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Tar.Lib
{
	internal class SbTarGnuHeader : SbTarHeader
	{
		private SbTarGnuHeader() { }

		public string Magic;       /* @ 257 */
		public string Version;     /* @ 263 */
		public string Uname;       /* @ 265 */
		public string Gname;       /* @ 297 */
		public string DevMajor;    /* @ 329 */
		public string DevMinor;    /* @ 337 */
		public string Prefix;      /* @ 345 */

		public static override SbTarGnuHeader ReadFromBuffer(byte[] buff)
		{

			return null;
		}

	}
}
