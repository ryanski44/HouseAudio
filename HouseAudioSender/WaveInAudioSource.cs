using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioSender
{
    public class WaveInAudioSource
    {
        public WaveInAudioSource(int deviceId, string productName)
        {
            this.DeviceId = deviceId;
            this.ProductName = productName;
        }

        public int DeviceId;
        public string ProductName;

        public override string ToString()
        {
            return ProductName;
        }
    }
}
