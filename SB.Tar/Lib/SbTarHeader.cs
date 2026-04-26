using System;

namespace SB.Tar.Lib
{
	public abstract class SbTarHeader
	{
		public long OffsetInFile { get; set; }
		public string Name { get; set; }         /* @ 000 */
		public string Mode { get; set; }         /* @ 100 */
		public string Uid { get; set; }                  /* @ 108 */
		public string Gid { get; set; }                  /* @ 116 */
		public string Size { get; set; }                 /* @ 124 */
		public string Mtime { get; set; }                /* @ 136 */
		public string Chksum { get; set; }               /* @ 148 */
		public string TypeFlag { get; set; }             /* @ 156 */
		public string LinkName { get; set; }             /* @ 157 */
		public string Pad { get; set; }                  /* @ 257 */

		public long DataOffset => this.OffsetInFile + 512;
		public DateTime DateModified => new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc).AddSeconds(this.Mtime.ToInt32FromOctalString());

	}
}
