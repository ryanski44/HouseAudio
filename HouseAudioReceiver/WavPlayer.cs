using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public class WavPlayer : IWaveProvider
    {
        public WaveOut WaveOut;
        private IBufferAllocator<byte> bufferAllocator;
        private AudioSegment currentSegment;
        public long Position { get; private set; }

        public WavPlayer(IBufferAllocator<byte> bufferAllocator)
        {
            this.bufferAllocator = bufferAllocator;
            WaveOut = new WaveOut();
            currentSegment = null;
            Position = 0;
        }

        public void Init()
        {
            WaveOut.Init(this);
            WaveOut.Play();
        }

        public void AddAudioData(long startByte, byte[] data)
        {
            AudioSegment segment = new AudioSegment()
            {
                StartIndex = startByte,
                Data = data
            };
            if (currentSegment == null)
            {
                if(startByte + data.Length > Position)
                {
                    currentSegment = segment;
                }
            }
            else
            {
                currentSegment.Insert(segment);
            }
        }

        private void RemoveCurrentSegment()
        {
            if (currentSegment == null) return;
            bufferAllocator.ReturnBuffer(currentSegment.Data);
            currentSegment = currentSegment.NextSegment;
        }

        public WaveFormat WaveFormat
        {
            get { return new WaveFormat(); }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            for (int targetIndex  = 0; targetIndex < count; targetIndex++)
            {
                long sourceIndex = Position + targetIndex;
                if(currentSegment != null && sourceIndex >= currentSegment.StartIndex && sourceIndex < currentSegment.StartIndex + currentSegment.Data.Length)
                {
                    int copyFrom = (int)(sourceIndex - currentSegment.StartIndex);
                    int countToCopy = Math.Min(count - targetIndex, currentSegment.Data.Length - copyFrom);
                    Buffer.BlockCopy(currentSegment.Data, copyFrom, buffer, targetIndex, countToCopy);
                    targetIndex += countToCopy - 1;
                    if(Position + targetIndex >= currentSegment.StartIndex + currentSegment.Data.Length)
                    {
                        RemoveCurrentSegment();
                    }
                }
                else
                {
                    buffer[targetIndex] = 0;
                }
            }
            Position += count;
            return count;
        }
    }
}
