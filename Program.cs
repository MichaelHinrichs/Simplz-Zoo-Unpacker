//Written for the steam version of Simplz Zoo. https://steamcommunity.com/app/7470/
using System.IO;
using System.Text;

namespace Simplz_Zoo_Unpacker
{
    public static class Program
    {
        public static BinaryReader br;
        public static BinaryWriter bw;

        static void Main(string file)
        {
            br = new BinaryReader(File.OpenRead(file));
            string type = Encoding.ASCII.GetString(br.ReadBytes(br.ReadInt32()));
            Directory.CreateDirectory(Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file));
            System.Collections.Generic.List<Subfile> subfiles = new();
            while (br.BaseStream.Position < br.BaseStream.Length - 4)
            {
                subfiles.Add(new());
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + "\\" + subfiles[^1].subfileName));
                bw = new BinaryWriter(File.OpenWrite(Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + "\\" + subfiles[^1].subfileName + ".zlib"));
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
