using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HouseAudioReceiver
{
    public class BufferAllocator<T> : IBufferAllocator<T>
    {
        private HashSet<T[]> buffers;
        private object buffersLock;
        private ConcurrentDictionary<int, ConcurrentBag<T[]>> freeBuffers;

        public BufferAllocator()
        {
            buffers = new HashSet<T[]>();
            buffersLock = new object();
            freeBuffers = new ConcurrentDictionary<int, ConcurrentBag<T[]>>();
        }

        public void ReturnBuffer(T[] buffer)
        {
            if(buffers.Contains(buffer))
            {
                lock(buffersLock)
                {
                    if(buffers.Contains(buffer))
                    {
                        buffers.Remove(buffer);
                        AddFreeBuffer(buffer);
                    }
                }
            }
        }

        private void AddFreeBuffer(T[] buffer)
        {
            if(!freeBuffers.ContainsKey(buffer.Length))
            {
                freeBuffers[buffer.Length] = new ConcurrentBag<T[]>();
            }
            freeBuffers[buffer.Length].Add(buffer);
        }

        public T[] TakeBuffer(int bufferSize)
        {
            T[] buffer = null;
            if (freeBuffers.ContainsKey(bufferSize))
            {
                if (!freeBuffers[bufferSize].TryTake(out buffer))
                {
                    buffer = new T[bufferSize];
                }
            }
            else
            {
                buffer = new T[bufferSize];
            }
            lock (buffersLock)
            {
                buffers.Add(buffer);
            }
            return buffer;
        }
    }
}