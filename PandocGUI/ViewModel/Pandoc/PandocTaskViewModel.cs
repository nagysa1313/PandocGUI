﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PandocGUI.Model;
using PandocGUI.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.ViewModel.Pandoc
{
    public class PandocTaskViewModel : ViewModelBase
    {
        private PandocTask _model;

        public PandocTask Model
        {
            get { return _model; }
            set { Set(() => Model, ref _model, value); }
        }

        private PandocTaskResult _result;

        public PandocTaskResult Result
        {
            get { return _result; }
            set { Set(() => Result, ref _result, value); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                Set(() => IsBusy, ref _isBusy, value);
                Do.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddTargetFile { get; private set; }
        public RelayCommand<TargetFile> RemoveTargetFile { get; private set; }
        public RelayCommand Do { get; private set; }

        public PandocTaskViewModel()
        {
            Model = new PandocTask();

            AddTargetFile = new RelayCommand(() =>
                {
                    Model.TargetFiles.Add(new TargetFile());
                });

            RemoveTargetFile = new RelayCommand<TargetFile>(file =>
                {
                    Model.TargetFiles.Remove(file);
                }, file => file != null);

            Do = new RelayCommand(() =>
                {
                    IsBusy = true;
                    Task.Factory.StartNew(() =>
                        {
                            Result = PandocRunner.Run(this.GetLocator().Config.Model.PandocExePath, Model).Last();
                        }).ContinueWith(task =>
                            {
                                IsBusy = false;
                            });
                }, () => !IsBusy);
        }
    }
}