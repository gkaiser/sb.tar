using SB.Tar.Lib;

namespace SB.Tar
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var tf = Lib.SbTarFile.FromFile(@"C:\Users\gkaiser\Downloads\testfile-nix-ustar.tar");

			foreach (var h in tf.Headers)
			{
				Console.WriteLine($"=> {h.Name} ({h.Size.ToInt32FromOctalString().ToFriendlySize()})");
			}

			if (System.Diagnostics.Debugger.IsAttached)
			{
				Console.WriteLine();
				Console.WriteLine("Done, press [ENTER] to quit... ");
				Console.ReadLine();
			}
		}
	}
}
