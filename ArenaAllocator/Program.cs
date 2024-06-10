using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
        Console.WriteLine("Hello, World!");

        Unsafe.SizeOf<>()
        Arena arena = Arena.Create(OSCore.Gigabytes(1));
        TestStruct s = arena.Alloc<TestStruct>();
        s.a = 1;
        s.b = 2;
        s.c = 3;

        Log.Info($"{s.a}, {s.b}, {s.c}");

        Console.ReadKey();

        // string stringA = "I seem to be turned around!";
        // int copylen = stringA.Length;
        //
        // // Allocate HGlobal memory for source and destination strings
        // IntPtr sptr = Marshal.StringToHGlobalAnsi(stringA);
        // IntPtr dptr = Marshal.AllocHGlobal(copylen + 1);
        // unsafe
        // {
        //     byte* src = (byte*)sptr.ToPointer();
        //     byte* dst = (byte*)dptr.ToPointer();
        //
        //     if (copylen > 0)
        //     {
        //         // set the source pointer to the end of the string
        //         // to do a reverse copy.
        //         src += copylen - 1;
        //
        //         while (copylen-- > 0)
        //         {
        //             *dst++ = *src--;
        //         }
        //         *dst = 0;
        //     }
        // }
        // string stringB = Marshal.PtrToStringAnsi(dptr);
        //
        // Console.WriteLine("Original:\n{0}\n", stringA);
        // Console.WriteLine("Reversed:\n{0}", stringB);
        //
        // // Free HGlobal memory
        // Marshal.FreeHGlobal(dptr);
        // Marshal.FreeHGlobal(sptr);
    }
}