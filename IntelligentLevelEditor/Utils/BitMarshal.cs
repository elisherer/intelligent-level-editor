using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace IntelligentLevelEditor.Utils
{
    public interface IBitField
    {
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    sealed class BitfieldLengthAttribute : Attribute
    {
        private readonly int _length;

        public BitfieldLengthAttribute(int length)
        {
            _length = length;
        }

        public int Length { get { return _length; } }
    }

    public struct ExampleStruct : IBitField
    {
        [BitfieldLength(4)]
        public byte x;
        [BitfieldLength(5)]
        public byte z;
        [BitfieldLength(10)]
        public ushort w;
        [BitfieldLength(4)]
        public byte t;

        public byte k; //8
        public bool b; //1
        public ushort us; //16
        public long lng; //64

        [BitfieldLength(16)]
        public ushort sixteen;
    }

    public static class BitMarshal
    {
        public static Int64 ToInt64(this IBitField obj)
        {
            Int64 returnValue = 0;
            var offset = 0;

            // For every field suitably attributed with a BitfieldLength
            foreach (var fieldInfo in (obj.GetType().GetFields().OrderBy(f => f.MetadataToken))) 
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
                if (attrs.Length == 1)
                {
                    var fieldLength = ((BitfieldLengthAttribute)attrs[0]).Length;

                    // Calculate a bitmask of the desired length
                    Int64 mask = 0;
                    for (var i = 0; i < fieldLength; i++)
                        mask |= (Int64)1 << i;

                    returnValue |= ((Int64)fieldInfo.GetValue(obj) & mask) << offset;

                    offset += fieldLength;
                }
                if (offset >= 63) break;
            }
            return returnValue;
        }

        public static int SizeOfInBits(IBitField obj)
        {
            var size = 0;
            foreach (var fieldInfo in (obj.GetType().GetFields()))
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
                if (attrs.Length == 1) //specified (once)
                {
                    size += ((BitfieldLengthAttribute)attrs[0]).Length;
                }
                else //not specified
                {
                    size += fieldInfo.FieldType == typeof(Boolean) ? 1 : Marshal.SizeOf(fieldInfo.FieldType) * 8;
                }
            }
            return size;
        }

        public static int SizeOf(IBitField obj)
        {
            return (int)Math.Ceiling(BitMarshal.SizeOfInBits(obj) / (double)8);
        }

        /*
        public static byte[] ToByteArray(this IBitField obj)
        {
            var offset = 0;

            var buffer = new byte[BitMarshal.SizeOf(obj)];
            
            // For every field suitably attributed with a BitfieldLength
            foreach (FieldInfo fieldInfo in (obj.GetType().GetFields().OrderBy(f => f.MetadataToken)))
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
                if (attrs.Length == 1)
                {
                    var fieldLength = ((BitfieldLengthAttribute)attrs[0]).Length;
                    var value = (UInt64)fieldInfo.GetValue(obj);

                    // Calculate a bitmask of the desired length
                    byte mask = 0;
                    for (int i = 0; i < fieldLength; i++)
                        mask |= 1 << i;

                    returnValue |= ( & mask) << offset;

                    offset += fieldLength;
                }
            }
            return buffer;
        }*/
    }
}
