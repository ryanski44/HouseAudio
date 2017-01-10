using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioCommon
{
    [ProtoContract]
    public class AudioPacket
    {
        [ProtoMember(1)]
        public long StartTicks { get; set; }
        [ProtoMember(2)]
        public byte[] AudioData { get; set; }
    }
}
