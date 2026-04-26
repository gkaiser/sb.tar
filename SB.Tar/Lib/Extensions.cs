using System;
using System.Collections.Generic;
using System.Text;

namespace SB.Tar.Lib
{
	internal static class Extensions
	{
		internal static byte[] ReadToBuff(this Stream s, int length)
		{
			var buff = new byte[length];

			s.ReadExactly(buff);

			//if (BitConverter.IsLittleEndian)
			//	Array.Reverse(buff);

			return buff;
		}

		internal static string ToStringFromBytes(this byte[] buff, int offset, int length)
		{
			return System.Text.Encoding.UTF8.GetString(buff, offset, length);
		}

		/*
		internal static string ReadString(this Stream s, int length)
		{
			var arr = s.ReadToBuff(length);

			arr = arr.TakeWhile(b => b != 0).ToArray();

			return System.Text.Encoding.UTF8.GetString(arr);
		}

		internal static long ReadLong(this Stream s) => BitConverter.ToInt64(s.ReadToBuff(8));
		*/

		internal static int ToInt32FromOctalString(this string s) => Convert.ToInt32(s, 8);

		public static string ToFriendlySize(this int i) => ((long)i).ToFriendlySize();

		public static string ToFriendlySize(this long l)
		{
			if (l < 1024)
				return $"{l / Math.Pow(1024, 0):N0} B";
			if (l < Math.Pow(1024, 2))
				return $"{l / Math.Pow(1024, 1):N1} KB";
			if (l < Math.Pow(1024, 3))
				return $"{l / Math.Pow(1024, 2):N1} MB";
			if (l < Math.Pow(1024, 4))
				return $"{l / Math.Pow(1024, 3):N1} GB";
			if (l < Math.Pow(1024, 5))
				return $"{l / Math.Pow(1024, 4):N1} TB";

			return $"{l:N0} B";
		}

	}
}
