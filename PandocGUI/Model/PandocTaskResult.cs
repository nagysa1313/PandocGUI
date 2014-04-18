using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    public class PandocTaskResult : ObservableObject
    {
        private string _outputFile;

        public string OutputFile
        {
            get { return _outputFile; }
            set { Set(() => OutputFile, ref _outputFile, value); }
        }

        private string _msg;

        public string Message
        {
            get { return _msg; }
            set { Set(() => Message, ref _msg, value); }
        }

        private bool _failed;

        public bool Failed
        {
            get { return _failed; }
            set { Set(() => Failed, ref _failed, value); }
        }

        private Exception _exception;

        public Exception Exception
        {
            get { return _exception; }
            set { Set(() => Exception, ref _exception, value); }
        }
    }
}