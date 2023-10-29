//Written for the steam version of Simplz Zoo. https://steamcommunity.com/app/7470/
using System.IO;
using System.Text;

namespace Simplz_Zoo_Unpacker
{
    public static class Program
    {
        public static BinaryReader br;
        public static BinaryWriter bw;

        static void Main(string[] args)
        {
            br = new BinaryReader(File.OpenRead(args[0]));
            string type = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32()));
            Directory.CreateDirectory(Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]));
            System.Collections.Generic.List<Subfile> subfiles = new();
            while (br.BaseStream.Position < br.BaseStream.Length - 4)
            {
                subfiles.Add(new());
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "\\" + subfiles[^1].subfileName));
                bw = new BinaryWriter(File.OpenWrite(Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "\\" + subfiles[^1].subfileName + ".zlib"));
                bw.Write(br.ReadBytes(subfiles[^1].size));
                bw.Close();
            }
        }

        class Subfile
        {
            public string subfileName = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32())).Replace("/", "\\");
            int unknownA = br.ReadInt32();
            int unknownB = br.ReadInt32();
            int unknownC = br.ReadInt32();
            public int size = br.ReadInt32();
        }
    }
}
