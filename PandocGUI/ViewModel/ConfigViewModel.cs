using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PandocGUI.Model;
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

        private string _detectedVersion;

        public string DetectedVersion
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
                            try
                            {
                                var startInfo = new ProcessStartInfo(Model.PandocExePath, "--version");
                                startInfo.UseShellExecute = false;
                                startInfo.RedirectStandardOutput = true;

                                var process = System.Diagnostics.Process.Start(startInfo);

                                while (!process.HasExited) ;

                                DetectedVersion = process.StandardOutput.ReadLine();
                            }
                            catch (Exception e)
                            {
                                Debugger.Break();
                                this.GetLocator().Task.LastError = e;
                            }
                        }).ContinueWith(task =>
                            {
                                IsBusy = false;
                            });
                }, () => !IsBusy);

            DetectVersion.Execute(null);
        }
    }
}