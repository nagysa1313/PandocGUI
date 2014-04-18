using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PandocGUI.Model;
using PandocGUI.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private PandocTaskResult _detectedVersion;

        public PandocTaskResult DetectedVersion
        {
            get { return _detectedVersion; }
            set { Set(() => DetectedVersion, ref _detectedVersion, value); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                Set(() => IsBusy, ref _isBusy, value);
                DetectVersion.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand BrowseForPandocExe { get; private set; }
        public RelayCommand DetectVersion { get; private set; }

        public ConfigViewModel()
        {
            Model = new Config() { PandocExePath = "pandoc.exe" };

            BrowseForPandocExe = new RelayCommand(() =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() ?? false)
                    {
                        Model.PandocExePath = dialog.FileName;
                        DetectVersion.Execute(null);
                    }
                });
            DetectVersion = new RelayCommand(() =>
                {
                    IsBusy = true;
                    Task.Factory.StartNew(() =>
                        {
                            DetectedVersion = PandocRunner.Version(this.GetLocator().Config.Model.PandocExePath);
                        }).ContinueWith(task =>
                            {
                                IsBusy = false;
                            });
                }, () => !IsBusy);

            DetectVersion.Execute(null);
        }
    }
}