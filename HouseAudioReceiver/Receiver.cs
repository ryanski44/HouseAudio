using HouseAudioCommon;
using InternetTime;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public class Receiver
    {
        public WavPlayer WavePlayer { get; private set; }
        public volatile int LocalClockOffset;
        private IBufferAllocator<byte> bufferAllocator;

        public TimeSpan LocalJitter { get; set; }
        public TimeSpan Delay { get; set; }
        private DateTime zeroPositionTime;

        public SNTPClient NtpClient { get; private set; }

        private Thread clockSyncThread;

        public string Host { get; set; }

        public Receiver(string host, string ntpHost, TimeSpan delay)
        {
            this.Delay = delay;

            bufferAllocator = new BufferAllocator<byte>();

            clockSyncThread = new Thread(ClockSyncWorker);
            clockSyncThread.IsBackground = true;

            this.Host = host;
            NtpClient = new SNTPClient(ntpHost);

            clockSyncThread.Start();
            WavePlayer = new WavPlayer(bufferAllocator);
            WavePlayer.Init();

            zeroPositionTime = DateTime.UtcNow;
            Thread.Sleep(500);
            AutoJitter();
        }

        private void ClockSyncWorker()
        {
            while (true)
            {
                try
                {
                    NtpClient.Connect(false);
                    LocalClockOffset = NtpClient.LocalClockOffset;
                }
                catch (Exception ex) { }
                Thread.Sleep(10000);
            }
        }

        private List<IPAddress> GetLocalIps()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                            i.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .SelectMany(i => i.GetIPProperties().UnicastAddresses)
                .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(a => a.Address)
                .ToList();
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                using (UdpClient udpClient = new UdpClient(Constants.Network.PORT))
                {
                    //IPEndPoint localEp =  new IPEndPoint(GetLocalIps().First(), Constants.Network.PORT);
                    //udpClient.Client.Bind(localEp);

                    IPAddress multicastAddr = IPAddress.Parse(Host);
                    udpClient.JoinMulticastGroup(multicastAddr);

                    while (true)//TODO: can stop
                    {
                        var data = await udpClient.ReceiveAsync();
                        AudioPacket audioPacket = Serializer.Deserialize<AudioPacket>(new MemoryStream(data.Buffer));
                        DateTime sampleStartTime = new DateTime(audioPacket.StartTicks);
                        long targetSample = (long)((sampleStartTime - zeroPositionTime - LocalJitter + Delay - TimeSpan.FromMilliseconds(LocalClockOffset)).TotalSeconds * Constants.Audio.SAMPLE_RATE);
                        long targetPosition = targetSample * Constants.Audio.BIT_DEPTH / 8 * Constants.Audio.CHANNELS;
                        WavePlayer.AddAudioData(targetPosition, audioPacket.AudioData);
                    }
                }
            });
        }

        internal void AutoJitter()
        {
            long currentPosition = WavePlayer.WaveOut.GetPosition();
            TimeSpan elapsedIdeal = DateTime.UtcNow - zeroPositionTime;
            TimeSpan elapsedPlayed = TimeSpan.FromSeconds(((double)currentPosition / (Constants.Audio.BIT_DEPTH * Constants.Audio.CHANNELS / 8)) / Constants.Audio.SAMPLE_RATE);
            LocalJitter = (elapsedIdeal - elapsedPlayed);
        }
    }
}
