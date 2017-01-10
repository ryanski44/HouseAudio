using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using HouseAudioSender;
using HouseAudioReceiver;

namespace HouseAudioTest
{
    [TestFixture]
    public class TestReceiver
    {
        [Test]
        public void TestReceiveOrder()
        {
            string ip = "239.0.0.222";
            string ntp = "0.us.pool.ntp.org";

            Sender audioSender = new Sender();

            audioSender.Start(System.Net.IPAddress.Parse(ip));

            Receiver sut = new Receiver(ip, ntp);

            sut.Start();

            //TODO
        }
    }
}
