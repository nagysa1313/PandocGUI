using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PandocGUI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.ViewModel.Pandoc
{
    public class PandocTaskVM : ViewModelBase
    {
        private PandocTask _model;

        public PandocTask Model
        {
            get { return _model; }
            set { Set(() => Model, ref _model, value); }
        }

        private Exception _lastError;

        public Exception LastError
        {
            get { return _lastError; }
            set { Set(() => LastError, ref _lastError, value); }
        }

        private string _failedTargetFile;

        public string FailedTargetFile
        {
            get { return _failedTargetFile; }
            set { Set(() => FailedTargetFile, ref _failedTargetFile, value); }
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

        public RelayCommand AddTargetFile { private get; set; }
        public RelayCommand<TargetFile> RemoveTargetFile { private get; set; }
        public RelayCommand BrowseForSourceFile { get; set; }
        public RelayCommand<TargetFile> BrowseForTargetFile { get; set; }
        public GalaSoft.MvvmLight.Command.RelayCommand Do { get; set; }

        public PandocTaskVM()
        {
            Model = new PandocTask();

            AddTargetFile = new RelayCommand(() =>
                {
                    Model.TargetFiles.Add(new TargetFile());
                });

            RemoveTargetFile = new RelayCommand<TargetFile>(file =>
                {
                    Model.TargetFiles.Remove(file);
                });
            BrowseForSourceFile = new RelayCommand(() =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() ?? false)
                    {
                        Model.SourceFile = dialog.FileName;
                    }
                });
            BrowseForTargetFile = new RelayCommand<TargetFile>((file) =>
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    if (dialog.ShowDialog() ?? false)
                    {
                        file.Path = dialog.FileName;
                    }
                });

            Do = new RelayCommand(() =>
                {
                    IsBusy = true;
                    Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                if (Model.TargetFiles.Count < 1) throw new InvalidOperationException("At least 1 target file needed for the task!");

                                foreach (var targetFile in Model.TargetFiles)
                                {
                                    FailedTargetFile = Path.GetFileName(targetFile.Path);

                                    var process = System.Diagnostics.Process.Start(this.GetLocator().Config.Model.PandocExePath, string.Format("{0} -f {1} -t {3} -s -o {2}"
                                        , Model.SourceFile
                                        , PandocFileExtension.Extensions[Path.GetExtension(Model.SourceFile)]
                                        , targetFile.Path
                                        , PandocFileExtension.Extensions[Path.GetExtension(targetFile.Path)]
                                        ));

                                    while (!process.HasExited) ;
                                }
                            }
                            catch (Exception e)
                            {
                                Debugger.Break();
                                LastError = e;
                            }

                            FailedTargetFile = null;
                        }).ContinueWith(task =>
                            {
                                IsBusy = false;
                            });
                }, () => !IsBusy);
        }
    }
}