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
        public AudioSegment CurrentSegment;
        public long Position { get; private set; }

        public WavPlayer(IBufferAllocator<byte> bufferAllocator)
        {
            this.bufferAllocator = bufferAllocator;
            WaveOut = new WaveOut();
            CurrentSegment = null;
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
            if (CurrentSegment == null)
            {
                if(startByte + data.Length > Position)
                {
                    CurrentSegment = segment;
                }
            }
            else
            {
                CurrentSegment.Insert(segment);
            }
        }

        private void RemoveCurrentSegment()
        {
            if (CurrentSegment == null) return;
            bufferAllocator.ReturnBuffer(CurrentSegment.Data);
            CurrentSegment = CurrentSegment.NextSegment;
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
                bool found = false;
                while(CurrentSegment != null && sourceIndex >= CurrentSegment.StartIndex)
                {
                    if (sourceIndex < CurrentSegment.StartIndex + CurrentSegment.Data.Length)
                    {
                        int copyFrom = (int)(sourceIndex - CurrentSegment.StartIndex);
                        int countToCopy = Math.Min(count - targetIndex, CurrentSegment.Data.Length - copyFrom);
                        Buffer.BlockCopy(CurrentSegment.Data, copyFrom, buffer, targetIndex, countToCopy);
                        targetIndex += countToCopy - 1;
                        found = true;
                        if (Position + targetIndex >= CurrentSegment.StartIndex + CurrentSegment.Data.Length)
                        {
                            RemoveCurrentSegment();
                        }
                        break;
                    }
                    else
                    {
                        RemoveCurrentSegment();
                    }
                }
                if(!found)
                {
                    buffer[targetIndex] = 0;
                }
            }
            Position += count;
            return count;
        }
    }
}
