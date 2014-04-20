using GalaSoft.MvvmLight;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    public class Config : ObservableObject
    {
        private int _key;

        [RepositoryPrimaryKey]
        public int Key
        {
            get { return _key; }
            set { Set(() => Key, ref _key, value); }
        }

        private string _pandocExePath;

        public string PandocExePath
        {
            get { return _pandocExePath; }
            set { Set(() => PandocExePath, ref _pandocExePath, value); }
        }
    }
}
