using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using InternetTime;
using System.Net;
using HouseAudioCommon;
using ProtoBuf;

namespace HouseAudioReceiver
{
    public partial class Form1 : Form
    {
        private Receiver receiver;

        private DebugCollection debugCollection;

       
        private Thread debugThread;

        public Form1()
        {
            InitializeComponent();
            
            debugThread = new Thread(DebugWorker);
            debugThread.IsBackground = true;
            debugCollection = new DebugCollection();
        }

        private void WriteDebugValue(string key, string value)
        {
            BeginInvoke(new Action(delegate { debugCollection.WriteValue(key, value); }));
        }

        private void DebugWorker()
        {
            while (!this.IsDisposed)
            {
                if (receiver.WavePlayer != null)
                {
                    WriteDebugValue("WavePlayerPosition", (receiver.WavePlayer.Position * 1000 / 4 / 44100).ToString());
                    if (receiver.WavePlayer.WaveOut.PlaybackState == PlaybackState.Playing)
                    {
                        WriteDebugValue("WaveOutPosition", (receiver.WavePlayer.WaveOut.GetPosition() * 1000 / 4 / 44100).ToString());
                    }
                    WriteDebugValue("LocalClockOffset", receiver.NtpClient.LocalClockOffset.ToString());
                    Thread.Sleep(500);
                }
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            receiver = new Receiver(textHost.Text, textNTPHost.Text);

            receiver.Start();

            debugThread.Start();
        }

        private DateTime lastSentSync = DateTime.MinValue;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = true;
            dataGridView.DefaultCellStyle.DataSourceNullValue = null;
            dataGridView.DataSource = debugCollection.BindingList;
        }
    }
}