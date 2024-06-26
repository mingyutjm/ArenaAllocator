﻿using System.Runtime.InteropServices;

namespace ArenaAllocator
{

    public static partial class OSCore
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress,
                                                 uint dwSize,
                                                 uint flAllocationType,
                                                 uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void GetSystemInfo(ref SystemInfo info);
    }

}