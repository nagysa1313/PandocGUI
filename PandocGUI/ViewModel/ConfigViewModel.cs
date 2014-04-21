using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PandocGUI.Model;
using PandocGUI.Utils;
using SharpRepository.Repository;
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

        public RelayCommand Save { get; private set; }
        public RelayCommand Load { get; private set; }
        public RelayCommand DetectVersion { get; private set; }

        public ConfigViewModel()
        {
            Model = new Config() { PandocExePath = "pandoc.exe" };

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
            Save = new RelayCommand(() =>
                {
                    var repo = RepositoryFactory.GetInstance<Config>();
                    if (repo.Exists(Model.Key))
                        repo.Update(Model);
                    else repo.Add(Model);
                });
            Load = new RelayCommand(() =>
                {
                    var repo = RepositoryFactory.GetInstance<Config>();
                    Model = repo.Any() ? repo.First() : Model;
                });

            this.PropertyChanged += (sender, e) => Save.Execute(null);
            Load.Execute(null);
            DetectVersion.Execute(null);
        }
    }
}