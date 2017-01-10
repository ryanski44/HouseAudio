using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public class AudioSegment
    {
        public long StartIndex;
        public byte[] Data;
        public AudioSegment NextSegment;

        public void Insert(AudioSegment newSegment)
        {
            if(NextSegment == null)
            {
                if (newSegment.StartIndex + newSegment.Data.Length > StartIndex + Data.Length)
                {
                    NextSegment = newSegment;
                }
            }
            else
            {
                if(newSegment.StartIndex < NextSegment.StartIndex)
                {
                    newSegment.NextSegment = NextSegment;
                    NextSegment = newSegment;
                }
                else
                {
                    NextSegment.Insert(newSegment);
                }
            }
        }
    }
}
