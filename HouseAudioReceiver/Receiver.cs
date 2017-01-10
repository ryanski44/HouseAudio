using HouseAudioCommon;
using InternetTime;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public class Receiver
    {
        public WavPlayer WavePlayer { get; private set; }
        private volatile int LocalClockOffset;
        private IBufferAllocator<byte> bufferAllocator;

        private TimeSpan localJitter = TimeSpan.Zero;
        private TimeSpan delay = TimeSpan.FromMilliseconds(1000);
        private DateTime zeroPositionTime;

        public SNTPClient NtpClient { get; private set; }

        private Thread clockSyncThread;

        public string Host { get; set; }

        public Receiver(string host, string ntpHost)
        {
            bufferAllocator = new BufferAllocator<byte>();

            clockSyncThread = new Thread(ClockSyncWorker);
            clockSyncThread.IsBackground = true;

            this.Host = host;
            NtpClient = new SNTPClient(ntpHost);

            clockSyncThread.Start();
            WavePlayer = new WavPlayer(bufferAllocator);
            WavePlayer.Init();

            zeroPositionTime = DateTime.UtcNow;
            long currentPosition = WavePlayer.WaveOut.GetPosition();
            Thread.Sleep(500);
            TimeSpan elapsedIdeal = DateTime.UtcNow - zeroPositionTime;
            TimeSpan elapsedPlayed = TimeSpan.FromSeconds((currentPosition / (Constants.Audio.BIT_DEPTH * Constants.Audio.CHANNELS / 8)) / Constants.Audio.SAMPLE_RATE);
            localJitter = (elapsedIdeal - elapsedPlayed);
        }

        private void ClockSyncWorker()
        {
            NtpClient.Connect(false);
            LocalClockOffset = NtpClient.LocalClockOffset;

            Thread.Sleep(10000);
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate { ReadPackets(); }));
        }

        protected void ReadPackets()
        {
            using (UdpClient udpClient = new UdpClient())
            {

                IPEndPoint localEp = new IPEndPoint(IPAddress.Any, Constants.Network.PORT);
                udpClient.Client.Bind(localEp);

                IPAddress multicastAddr = IPAddress.Parse(Host);
                udpClient.JoinMulticastGroup(multicastAddr);

                while (true)//TODO: can stop
                {
                    byte[] data = udpClient.Receive(ref localEp);
                    AudioPacket audioPacket = Serializer.Deserialize<AudioPacket>(new MemoryStream(data));
                    DateTime sampleStartTime = new DateTime(audioPacket.StartTicks);
                    long targetSample = (long)((sampleStartTime - zeroPositionTime - localJitter + delay).TotalSeconds * Constants.Audio.SAMPLE_RATE);
                    long targetPosition = targetSample * Constants.Audio.BIT_DEPTH / 8 * Constants.Audio.CHANNELS;
                    WavePlayer.AddAudioData(targetPosition, audioPacket.AudioData);
                }
            }
        }
    }
}
