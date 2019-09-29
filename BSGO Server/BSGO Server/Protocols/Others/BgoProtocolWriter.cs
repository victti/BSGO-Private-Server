using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BSGO_Server
{
    class BgoProtocolWriter : BinaryWriter
    {
        private MemoryStream memoryStream;

        public BgoProtocolWriter()
            : base(new MemoryStream())
        {
            memoryStream = (MemoryStream)BaseStream;
            Write((ushort)0);
        }

        private void WriteDataLength(byte[] data)
        {
            ushort num = (ushort)(GetLength() - 2);
            data[0] = (byte)((num >> 8) & 0xFF);
            data[1] = (byte)(num & 0xFF);
        }

        public override void Write(string value)
        {
            Encoding uTF = Encoding.UTF8;
            byte[] bytes = uTF.GetBytes(value);
            Write((ushort)bytes.Length);
            if (bytes.Length > 0)
            {
                Write(bytes, 0, bytes.Length);
            }
        }

        public void Write(string[] value)
        {
            Write(value.Length);
            for(int i = 0; i < value.Length; i++)
            {
                Write(value[i]);
            }
        }

        public byte[] GetBuffer()
        {
            byte[] buffer = memoryStream.GetBuffer();
            WriteDataLength(buffer);
            return buffer;
        }

        public int GetLength()
        {
            return (int)memoryStream.Length;
        }
    }
}
