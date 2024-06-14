namespace ArenaAllocator
{

    public static partial class OSCore
    {
        public static uint Bytes(uint n) => n;
        public static uint Kilobytes(uint n) => n << 10;
        public static uint Megabytes(uint n) => n << 20;
        public static uint Gigabytes(uint n) => n << 30;

        public static IntPtr Reserve(uint size)
        {
            uint gbSnappedSize = Misc.SnapSize(size, Gigabytes(1));
            IntPtr ptr = VirtualAlloc(0, gbSnappedSize, MEM_RESERVE, PAGE_NOACCESS);
            return ptr;
        }

        public static void Release(IntPtr ptr, uint size)
        {
            VirtualFree(ptr, 0, MEM_RELEASE);
        }

        public static void Commit(IntPtr ptr, uint size)
        {
            uint pageSnappedSize = Misc.SnapSize(size, PageSize());
            VirtualAlloc(ptr, pageSnappedSize, MEM_COMMIT, PAGE_READWRITE);
        }

        public static void Decommit(IntPtr ptr, uint size)
        {
            VirtualFree(ptr, size, MEM_DECOMMIT);
        }

        public static uint PageSize()
        {
            SystemInfo info = default;
            GetSystemInfo(ref info);
            return info.dwPageSize;
        }
    }

}