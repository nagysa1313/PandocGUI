using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    public class TargetFile : ObservableObject
    {
        private string _filePath;

        public string Path
        {
            get { return _filePath; }
            set { Set(() => Path, ref _filePath, value); }
        }
    }

    public class PandocTask : ObservableObject
    {
        private string _sourceFile;

        public string SourceFile
        {
            get { return _sourceFile; }
            set { Set(() => SourceFile, ref _sourceFile, value); }
        }

        private ObservableCollection<TargetFile> _targetFiles;

        public ObservableCollection<TargetFile> TargetFiles
        {
            get { return _targetFiles; }
            set { Set(() => TargetFiles, ref _targetFiles, value); }
        }

        public PandocTask()
        {
            TargetFiles = new ObservableCollection<TargetFile>();
        }
    }
}