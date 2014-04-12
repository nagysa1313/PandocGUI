using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    public class Config : ObservableObject
    {
        private string _pandocExePath;

        public string PandocExePath
        {
            get { return _pandocExePath; }
            set { Set(() => PandocExePath, ref _pandocExePath, value); }
        }
    }
}
