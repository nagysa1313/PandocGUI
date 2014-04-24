using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    public class PandocTaskConfig : ObservableObject
    {
        private bool _parseRaw;

        public bool ParseRaw
        {
            get { return _parseRaw; }
            set { Set(() => ParseRaw, ref _parseRaw, value); }
        }

        private bool _normalize;

        public bool Normalize
        {
            get { return _normalize; }
            set { Set(() => Normalize, ref _normalize, value); }
        }

        private bool _preserveTabs;

        public bool PreserveTabs
        {
            get { return _preserveTabs; }
            set { Set(() => PreserveTabs, ref _preserveTabs, value); }
        }

        private int _tabStop;

        public int TabStop
        {
            get { return _tabStop; }
            set { Set(() => TabStop, ref _tabStop, value); }
        }

        public PandocTaskConfig()
        {
            TabStop = 4;
        }
    }
}
