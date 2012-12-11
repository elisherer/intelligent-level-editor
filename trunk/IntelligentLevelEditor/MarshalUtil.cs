using System;
using System.Runtime.InteropServices;

namespace IntelligentLevelEditor
{
    static class MarshalUtil
    {
        public static string ByteArrayToString(byte[] array)
        {
            int i;
            var arraystring = string.Empty;
            for (i = 0; i < array.Length && i < 40; i++)
                arraystring += String.Format("{0:X2} ", array[i]);
            if (i == 40) return arraystring + "..."; //ellipsis
            return arraystring;
        }

        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var temp = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return temp;
        }

        public static byte[] StructureToByteArray<T>(T structure) where T : struct
        {
            var size = Marshal.SizeOf(structure);
            var byteArray = new byte[size];
            var pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, pointer, false);
            Marshal.Copy(pointer, byteArray, 0, size);
            Marshal.FreeHGlobal(pointer);
            return byteArray;
        }
    }
}
