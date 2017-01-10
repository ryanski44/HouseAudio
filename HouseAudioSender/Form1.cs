using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;
using System.Net;
using HouseAudioCommon;
using ProtoBuf;

namespace HouseAudioSender
{
    public partial class Form1 : Form
    {
        private Sender audioSender;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            audioSender = new Sender();
            this.Location = Properties.Settings.Default.MyLoc;
            this.WindowState = Properties.Settings.Default.MyState;
            Task.Run(() =>
            {
                WriteLine("Initializing Wave In Devices");
                int waveInDevices = WaveIn.DeviceCount;
                for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
                {
                    WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                    WriteLine("Device {0}: {1}, {2} channels",
                        waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
                    Invoke(new Action(delegate
                    {
                        comboBoxAudioSource.Items.Add(new WaveInAudioSource(waveInDevice, deviceInfo.ProductName));
                    }));
                }
                Invoke(new Action(delegate
                {
                    comboBoxAudioSource.Enabled = true;
                    if (comboBoxAudioSource.Items.Count > 0)
                    {
                        comboBoxAudioSource.SelectedIndex = 0;
                    }
                }));
                WriteLine("Loaded Wave In Devices");
            });
        }

        public void WriteLine(string line, params object[] args)
        {
            WriteLine(string.Format(line, args));
        }

        public void WriteLine(string line)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(delegate { WriteLine(line); }));
            }
            else
            {
                textBoxStatus.AppendText(line + Environment.NewLine);
            }
        }

        public void Write(string line)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(delegate { Write(line); }));
            }
            else
            {
                textBoxStatus.AppendText(line);
            }
        }

        private void comboBoxAudioSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!buttonStartStop.Enabled)
            {
                buttonStartStop.Enabled = true;
            }
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (buttonStartStop.Text == "Start")
            {
                audioSender.Start(IPAddress.Parse(textBoxUDPIP.Text), (comboBoxAudioSource.SelectedItem as WaveInAudioSource).DeviceId);
                WriteLine("Started Recording at {0} UTC", audioSender.StartTime);
                buttonStartStop.Text = "Stop";
            }
            else
            {
                audioSender.Stop();
                buttonStartStop.Text = "Start";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioSender.Stop();

            Properties.Settings.Default.MyState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.MyLoc = this.Location;
            }

            else
            {
                Properties.Settings.Default.MyLoc = this.RestoreBounds.Location;
            }

            Properties.Settings.Default.Save();
        }
    }
}
