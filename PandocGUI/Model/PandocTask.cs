using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    public class PandocTask : ObservableObject
    {
        private string _sourceFile;

        public string SourceFile
        {
            get { return _sourceFile; }
            set { Set(() => SourceFile, ref _sourceFile, value); }
        }

        private string _targetFile;

        public string TargetFile
        {
            get { return _targetFile; }
            set { Set(() => TargetFile, ref _targetFile, value); }
        }
    }
}