using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public class DebugCollection
    {
        private BindingList<DebugValue> values;
        private Dictionary<string, DebugValue> map;
        private object _lock = new object();

        public DebugCollection()
        {
            values = new BindingList<DebugValue>();
            map = new Dictionary<string, DebugValue>();
        }

        public BindingList<DebugValue> BindingList
        {
            get { return values; }
        }

        public void WriteValue(string key, string value)
        {
            lock (_lock)
            {
                if (!map.ContainsKey(key))
                {
                    map[key] = new DebugValue(key);
                    values.Add(map[key]);
                }
                map[key].Value = value;
            }
        }
    }
}
