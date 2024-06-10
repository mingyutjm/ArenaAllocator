using System.Runtime.InteropServices;

namespace ArenaAllocator
{

    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct ArenaHead
    {
        public static uint Size = 4;

        public ushort typeId;
        public ushort typeSize;
    }

}