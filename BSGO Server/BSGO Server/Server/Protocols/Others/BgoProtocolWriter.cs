﻿using System.Drawing;
using System.IO;
using System.Numerics;
using System.Text;

namespace BSGO_Server
{
    internal class BgoProtocolWriter : BinaryWriter
    {
        private readonly MemoryStream memoryStream;

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
                Write(bytes, 0, bytes.Length);
            
        }

        public void Write(string[] value)
        {
            Write((ushort)value.Length);
            for(int i = 0; i < value.Length; i++)
                Write(value[i]);
            
        }

        public void Write(Color value)
        {
            Write((byte)(value.R * 255f));
            Write((byte)(value.G * 255f));
            Write((byte)(value.B * 255f));
            Write((byte)(value.A * 255f));
        }

        public void Write(Vector3 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }

        public void Write(Vector2 value)
        {
            Write(value.X);
            Write(value.Y);
        }

        public void Write(Quaternion value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }

        public byte[] GetBuffer()
        {
            byte[] buffer = memoryStream.GetBuffer();
            WriteDataLength(buffer);
            return buffer;
        }

        public int GetLength() =>
            (int)memoryStream.Length;
        
    }
}
