using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PandocGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        private Config _model;

        public Config Model
        {
            get { return _model; }
            set { Set(() => Model, ref _model, value); }
        }

        public RelayCommand BrowseForPandocExe { get; set; }

        public ConfigViewModel()
        {
            Model = new Config() { PandocExePath = "pandoc.exe" };

            BrowseForPandocExe = new RelayCommand(() =>
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() ?? false)
                {
                    Model.PandocExePath = dialog.FileName;
                }
            });
        }
    }
}