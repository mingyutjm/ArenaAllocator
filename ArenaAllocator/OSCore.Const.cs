namespace ArenaAllocator
{

    public static partial class OSCore
    {
        // 定义常量
        private const uint MEM_RESERVE = 0x00002000;
        private const uint MEM_RELEASE = 0x8000;
        private const uint MEM_COMMIT = 0x00001000;
        private const uint MEM_DECOMMIT = 0x00004000;
        private const uint PAGE_NOACCESS = 0x01;
        private const uint PAGE_READWRITE = 0x04;
    }

}