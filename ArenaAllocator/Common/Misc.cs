namespace ArenaAllocator
{

    public static class Misc
    {
        public static uint SnapSize(uint size, uint granularity)
        {
            size += granularity - 1;
            size -= size % granularity;
            return size;
        }
    }

}