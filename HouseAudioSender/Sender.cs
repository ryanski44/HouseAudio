using HouseAudioCommon;
using NAudio.Wave;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioSender
{
    public class Sender
    {
        private bool isRecording;

        private Stopwatch stopWatch;
        public DateTime StartTime { get; private set; }
        private long bytesSent;

        private UdpClient udpClient;
        private IPEndPoint remoteEndPoint;

        public Sender()
        {
            stopWatch = new Stopwatch();
        }

        public void Start(IPAddress multicastaddress)
        {
            udpClient = new UdpClient();
            udpClient.JoinMulticastGroup(multicastaddress);
            remoteEndPoint = new IPEndPoint(multicastaddress, Constants.Network.PORT);

            bytesSent = 0;
            isRecording = true;
            
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
            }
            stopWatch.Reset();
            //wait for the next update of the system clock, so we can be as accurate as possible. The next update is when the ticks on DateTime.Now change
            DateTime tick1 = DateTime.UtcNow;
            DateTime tick2 = DateTime.UtcNow;
            while (tick1.Ticks == tick2.Ticks)
            {
                tick1 = tick2;
                tick2 = DateTime.UtcNow;
            }
            StartTime = tick2;
            stopWatch.Start();
            
            StartTime = StartTime.AddMilliseconds(stopWatch.ElapsedMilliseconds);
            stopWatch.Restart();
        }

        public void DataAvailable(object sender, WaveInEventArgs e)
        {
            if (isRecording && udpClient != null)
            {
                AudioPacket packet = new AudioPacket()
                {
                    AudioData = new byte[e.BytesRecorded],
                    StartTicks = (StartTime + TimeSpan.FromSeconds(bytesSent / (Constants.Audio.BIT_DEPTH / 8 * Constants.Audio.CHANNELS) / Constants.Audio.SAMPLE_RATE)).Ticks
                };
                Buffer.BlockCopy(e.Buffer, 0, packet.AudioData, 0, e.BytesRecorded);
                MemoryStream ms = new MemoryStream();
                Serializer.Serialize(ms, packet);
                udpClient.Send(ms.ToArray(), (int)ms.Length, remoteEndPoint);
                bytesSent += e.BytesRecorded;
            }
        }

        public void Stop()
        {
            if (isRecording)
            {
                isRecording = false;
                udpClient.Close();
                udpClient.Dispose();
                udpClient = null;
            }
        }
    }
}
