namespace ArenaAllocator;

struct TestStruct
{
    public int a;
    public int b;
    public int c;
}

class Program
{
    static void Main(string[] args)
    {
        Arena arena = Arena.Create(OSCore.Gigabytes(1));
        TestStruct s = arena.Alloc<TestStruct>();
        s.a = 1;
        s.b = 2;
        s.c = 3;
        Log.Info($"{s.a}, {s.b}, {s.c}");
        Console.ReadKey();
    }
}