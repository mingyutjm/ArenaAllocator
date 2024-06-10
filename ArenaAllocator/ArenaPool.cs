namespace ArenaAllocator
{

    public class ArenaPool : Singleton<ArenaPool>
    {
        private Stack<Arena> _pool = new Stack<Arena>(16);

        public Arena Get()
        {
            if (_pool.TryPop(out Arena arena))
            {
                return arena;
            }
            else
            {
                arena = Arena.Create(1);
                return arena;
            }
        }

        public void Release(Arena arena)
        {
            if (_pool.Contains(arena))
            {
                Log.Error("arena already in pool");
            }
            else
            {
                _pool.Push(arena);
            }
        }
    }

}