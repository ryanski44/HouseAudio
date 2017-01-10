using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioCommon
{
    public static class Constants
    {
        public static class Network
        {
            public static readonly int MAX_PACKET_DATA_SIZE = 65535;

            public static readonly int PORT = 4587;
        }

        public static class Audio
        {
            public static readonly int SAMPLE_RATE = 44100;
            public static readonly byte BIT_DEPTH = 16;
            public static readonly byte CHANNELS = 2;
        }
    }
}
