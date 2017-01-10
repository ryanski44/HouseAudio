using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public interface IBufferAllocator<T>
    {
        T[] TakeBuffer(int bufferSize);
        void ReturnBuffer(T[] buffer);
    }
}
