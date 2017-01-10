using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public static class Extensions
    {
        private static DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixMSSinceEPOCH(this DateTime dt)
        {
            long unixTimeStampInTicks = dt.Ticks - EPOCH.Ticks;
            return unixTimeStampInTicks / TimeSpan.TicksPerMillisecond;
        }

        public static DateTime FromUnixMSSinceEPOCHToDateTime(this long unixTimestampMs)
        {
            DateTime unixYear0 = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = unixTimestampMs * TimeSpan.TicksPerSecond / 1000;
            DateTime dtUnix = new DateTime(EPOCH.Ticks + unixTimeStampInTicks);
            return dtUnix;
        }
    }
}
