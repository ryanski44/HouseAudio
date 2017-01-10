using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseAudioReceiver
{
    public class DebugValue : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DebugValue(string label)
        {
            this.Label = label;
        }
        public string Label { get; set; }

        private string value;
        public string Value
        {
            get { return value; }
            set 
            { 
                this.value = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }
    }
}
