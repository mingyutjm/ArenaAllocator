using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ArenaAllocator
{

    [StructLayout(LayoutKind.Sequential, Size = 64)]
    public struct Arena
    {
        private static uint ARENA_COMMIT_GRANULARITY = OSCore.Kilobytes(4);

        private IntPtr _memory;
        private uint _pos;
        private uint _commitPos;
        private uint _align;
        private uint _size;

        public static Arena Create(uint size)
        {
            uint size_roundup_granularity = OSCore.Megabytes(64);
            size += size_roundup_granularity - 1;
            size -= size % size_roundup_granularity;
            IntPtr block = ArenaImpl_Reserve(size);
            uint initialCommitSize = ARENA_COMMIT_GRANULARITY;
            ArenaImpl_Commit(block, initialCommitSize);

            Arena arena = Marshal.PtrToStructure<Arena>(block);
            arena._memory = block;
            arena._pos = (uint)Marshal.SizeOf<Arena>();
            arena._commitPos = initialCommitSize;
            arena._align = 8;
            arena._size = size;
            return arena;
        }

        // 销毁
        public void Dispose()
        {
            IntPtr ptr = new IntPtr(1); //Marshal.AllocHGlobal(Marshal.SizeOf<Arena>());
            Marshal.StructureToPtr(this, ptr, true);
            ArenaImpl_Release(ptr, _size);
        }

        // 清理
        public void Clear()
        {
            uint initialCommitSize = ARENA_COMMIT_GRANULARITY;
            _pos = (uint)Marshal.SizeOf<Arena>();
            _commitPos = initialCommitSize;
        }

        public T Alloc<T>() where T : struct
        {
            return Push<T>();
        }

        public T Push<T>() where T : struct
        {
            IntPtr result = IntPtr.Zero;
            uint headSize = ArenaHead.Size;
            uint instanceSize = (uint)Marshal.SizeOf<T>();
            uint allocSize = instanceSize + headSize;
            if (_pos + allocSize <= _size)
            {
                uint postAlignPos = _pos + (_align - 1);
                postAlignPos -= postAlignPos % _align;
                uint align = postAlignPos - _pos;
                result = IntPtr.Add(_memory, (int)(_pos + align));
                _pos += allocSize + align;
                if (_commitPos < _pos)
                {
                    uint sizeToCommit = _pos - _commitPos;
                    sizeToCommit += ARENA_COMMIT_GRANULARITY - 1;
                    sizeToCommit -= sizeToCommit % ARENA_COMMIT_GRANULARITY;
                    ArenaImpl_Commit(IntPtr.Add(_memory, (int)_commitPos), sizeToCommit);
                    _commitPos += sizeToCommit;
                }
            }
            else
            {
            }

            ArenaHead head = Marshal.PtrToStructure<ArenaHead>(result);
            head.typeId = (ushort)TypeId.Instance.GetId<T>();
            head.typeSize = (ushort)instanceSize;

            IntPtr instancePtr = IntPtr.Add(result, (int)headSize);
            T instance = Marshal.PtrToStructure<T>(instancePtr);
            return instance;
        }

        public void Pop<T>() where T : struct
        {
            // U64 min_pos = sizeof(Arena);
            // U64 size_to_pop = Min(size, arena->pos);
            // U64 new_pos = arena->pos - size_to_pop;
            // new_pos = Max(new_pos, min_pos);
            // ArenaPopTo(arena, new_pos);
            //
        }

        private static IntPtr ArenaImpl_Reserve(uint size)
        {
            return OSCore.Reserve(size);
        }

        private static void ArenaImpl_Release(IntPtr ptr, uint size)
        {
            OSCore.Release(ptr, size);
        }

        private static void ArenaImpl_Commit(IntPtr ptr, uint size)
        {
            OSCore.Commit(ptr, size);
        }

        private static void ArenaImpl_Decommit(IntPtr ptr, uint size)
        {
            OSCore.Decommit(ptr, size);
        }
    }

}