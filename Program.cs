//Written for the steam version of Simplz Zoo. https://steamcommunity.com/app/7470/
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Simplz_Zoo_Unpacker
{
    public static class Program
    {
        public static BinaryReader br;

        static void Main(string[] args)
        {
            br = new BinaryReader(File.OpenRead(args[0]));
            string type = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32()));

            while (br.BaseStream.Position < br.BaseStream.Length - 4)
            {
                Subfile subfile = new();
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "\\" + subfile.name));

                MemoryStream ms = new();
                if (subfile.isCompressed == 0)
                {
                    br.ReadInt16();
                    using var ds = new DeflateStream(new MemoryStream(br.ReadBytes(subfile.sizeCompressed - 2)), CompressionMode.Decompress);
                    ds.CopyTo(ms);
                    ds.Close();
                }
                else if (subfile.isCompressed == 1 || subfile.isCompressed == 2)//What's the difference?
                    ms.Write(br.ReadBytes(subfile.sizeUncompressed));
                else
                    throw new System.Exception("Fuck!");

                BinaryReader msr = new(ms);
                msr.BaseStream.Position = 0;

                BinaryWriter bw = new BinaryWriter(File.OpenWrite(Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "\\" + subfile.name));
                bw.Write(msr.ReadBytes(subfile.sizeUncompressed));
                msr.Close();
                bw.Close();
            }
        }

        class Subfile
        {
            public string name = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32())).Replace("/", "\\");
            float unknown = br.ReadSingle();
            public int isCompressed = br.ReadInt32();
            public int sizeUncompressed = br.ReadInt32();
            public int sizeCompressed = br.ReadInt32();
        }
    }
}
