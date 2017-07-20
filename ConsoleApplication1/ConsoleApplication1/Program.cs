using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ConsoleApplication1
{
    class Program
    {
        static byte[] x = new byte[] { 48, 239, 114, 71, 66, 242, 4, 50 };
        static byte[] y = new byte[] { 14,166,220,137,219,237,220,79};
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                foreach (string gameFile in args)
                {
                    Console.WriteLine("Unpacking " + Path.GetFileName(gameFile));
                    if (File.Exists(gameFile))
                    {
                        try
                        {
                            DES desProv = new DESCryptoServiceProvider();
                            Stream input = new FileStream(gameFile, FileMode.Open, FileAccess.Read);
                            CryptoStream cryptStream = new CryptoStream(input, desProv.CreateDecryptor(x, y), CryptoStreamMode.Read);
                            Stream output = File.Create(Path.GetFileNameWithoutExtension(gameFile) + ".xml");
                            GZipStream gZip = new GZipStream(cryptStream, CompressionMode.Decompress);
                            gZip.CopyTo(output);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("I failed you:" + ex.Message);
                            Console.ReadKey();
                        }
                    }
                }
                Console.WriteLine("Done :) QuickTip: You can uncompress more files at once by dragging multiple files on the executable.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Please drag a file on this executable which you want to have decompressed");
                Console.ReadKey();
            }
        }
    }
}
