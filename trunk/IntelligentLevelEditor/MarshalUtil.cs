using System.Runtime.InteropServices;

namespace IntelligentLevelEditor
{
    static class MarshalUtil
    {
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
