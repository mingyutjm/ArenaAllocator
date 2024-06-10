namespace ArenaAllocator
{

    public static partial class OSCore
    {
        // private const uint MEM_FREE = 0x00010000;

        public static uint Bytes(uint n) => n;
        public static uint Kilobytes(uint n) => n << 10;
        public static uint Megabytes(uint n) => n << 20;
        public static uint Gigabytes(uint n) => n << 30;

        public static IntPtr Reserve(uint size)
        {
            uint gbSnappedSize = size;
            gbSnappedSize += Gigabytes(1) - 1;
            gbSnappedSize -= gbSnappedSize % Gigabytes(1);
            IntPtr ptr = VirtualAlloc(0, gbSnappedSize, MEM_RESERVE, PAGE_NOACCESS);
            return ptr;
        }

        public static void Release(IntPtr ptr, uint size)
        {
            VirtualFree(ptr, 0, MEM_RELEASE);
        }

        public static void Commit(IntPtr ptr, uint size)
        {
            uint page_snapped_size = size;
            page_snapped_size += PageSize() - 1;
            page_snapped_size -= page_snapped_size % PageSize();
            VirtualAlloc(ptr, page_snapped_size, MEM_COMMIT, PAGE_READWRITE);
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