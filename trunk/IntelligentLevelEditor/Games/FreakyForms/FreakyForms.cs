using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IntelligentLevelEditor.Games.FreakyForms
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FreakyFormeeHeader
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Magic; //4 bytes
        public ushort DecompressedSize; //2 bytes
        public ushort CompressedSize; //2 bytes
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] Unknown2; // 20 bytes
        public uint Zero; //4 bytes 
    } // overall 32 bytes header

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FreakyFormeeData
    {
        public ushort Version;// (0: Standard, 1: Deluxe)
        public byte Magic;// (0xF0, FreakyForms :-) )
        public byte Version2;//? (1: Standard, 3: Deluxe)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] Unknown;//, ID of somekind

        public uint Counter;// (unknown)
        public uint Unknown2;
        public uint ZeroPadding;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)] 
        public byte[] Zeros;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
        public byte[] Unknown3;

        public ulong SomeNumber;
    }

    public struct FreakyFormsChunk
    {
        public byte Type;
        public byte[] Data;
    }

    public class FreakyForms
    {
        private FreakyFormeeHeader _header;
        private FreakyFormeeData _data;
        private readonly List<FreakyFormsChunk> _chunks = new List<FreakyFormsChunk>();
        private bool _deluxe;
        private byte[] _listArray; 

        private string _name = "", _author = "", _catchphrase = "";

        public static bool IsMatchingData(byte[] data)
        {
            return (data[0] == '3' && data[1] == 'D' && data[2] == 'C' && data[3] == 'T');
        }


        public void New()
        {
            _header.Magic = new[] { '3', 'D', 'C', 'T' };
            _header.DecompressedSize = 0;
            _header.CompressedSize = 0;
            _header.Unknown2 = new byte[20];
            for (var i = 0; i < _header.Unknown2.Length; i++)
                _header.Unknown2[i] = 0;
            _header.Zero = 0;
        }

        public void Read(Stream stream)
        {
            var buffer = new byte[Marshal.SizeOf(typeof (FreakyFormeeHeader))];
            stream.Read(buffer, 0, buffer.Length);
            _header = MarshalUtil.ByteArrayToStructure<FreakyFormeeHeader>(buffer);

            _listArray = new byte[_header.DecompressedSize];
            var ms = new MemoryStream(_listArray);
            var lz10 = new DSDecmp.Formats.Nitro.LZ10();
            try
            {
                lz10.Decompress(stream, stream.Length - stream.Position, ms);

                ms.Seek(0, SeekOrigin.Begin);

                buffer = new byte[Marshal.SizeOf(typeof(FreakyFormeeData))];
                ms.Read(buffer, 0, buffer.Length);
                _data = MarshalUtil.ByteArrayToStructure<FreakyFormeeData>(buffer);

                _deluxe = _data.Version == 1 && _data.Version2 == 3;
                
                if (!_deluxe)
                {
                    ms.Seek(4, SeekOrigin.Current); // skip file size and unknown 2 bytes
                    var chunks = ms.ReadByte();
                    
                    while (chunks > 0)
                    {
                        var chunk = new FreakyFormsChunk {Type = (byte) ms.ReadByte()};

                        if (chunk.Type == 0 || chunk.Type == 2 || chunk.Type == 6)
                        {
                            chunk.Data = new byte[0x12];
                        }
                        else
                        {
                            chunk.Data = new byte[0x10];
                        }
                        ms.Read(chunk.Data , 0, chunk.Data.Length);
                        _chunks.Add(chunk);
                        chunks--;
                    }
                    var textStart = ms.ReadByte(); //should be 5
                    var length = ms.ReadByte();
                    buffer = new byte[length * 2]; // UTF-16
                    ms.Read(buffer, 0, buffer.Length);
                    _name = Encoding.Unicode.GetString(buffer);
                    
                    length = ms.ReadByte();
                    buffer = new byte[length * 2]; // UTF-16
                    ms.Read(buffer, 0, buffer.Length);
                    _catchphrase = Encoding.Unicode.GetString(buffer);

                    length = ms.ReadByte();
                    buffer = new byte[length * 2]; // UTF-16
                    ms.Read(buffer, 0, buffer.Length);
                    _author = Encoding.Unicode.GetString(buffer);
                }
            }
            catch//(Exception ex)
            {}
            ms.Close();
        }

        public void Write(Stream stream)
        {
            //TODO
            //calculate crc32
            /*var decompressed = StructureToByteArray(_levelData);
            var crc = Crc32.Calculate(decompressed, 0, decompressed.Length - 4);
            Buffer.BlockCopy(BitConverter.GetBytes(crc), 0, decompressed, decompressed.Length - 4, 4);
            var lz10 = new DSDecmp.Formats.Nitro.LZ10();
            var ms = new MemoryStream(decompressed);
            lz10.Compress(ms, decompressed.Length, stream);*/
        }

        public void SetName(string value)
        {
            _name = value;
        }
        public string GetName()
        {
            return _name;
        }

        public void SetAuthorName(string value)
        {
            _author = value;
        }
        public string GetAuthorName()
        {
            return _author;
        }

        public void SetCatchphrase(string value)
        {
            _catchphrase = value;
        }
        public string GetCatchphrase()
        {
            return _catchphrase;
        }

        public List<FreakyFormsChunk> GetChunks()
        {
            return _chunks;
        }
    }
}
