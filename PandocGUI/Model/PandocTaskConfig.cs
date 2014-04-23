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


    }
}
