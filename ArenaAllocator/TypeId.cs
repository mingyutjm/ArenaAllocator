namespace ArenaAllocator
{

    public class TypeId : Singleton<TypeId>
    {
        private int _currentMaxId = 1;
        private Dictionary<System.Type, int> _typeIds = new Dictionary<Type, int>();

        public int GetId<T>()
        {
            if (!_typeIds.TryGetValue(typeof(T), out int id))
            {
                id = _currentMaxId++;
                _typeIds.Add(typeof(T), id);
            }
            return id;
        }
    }

}