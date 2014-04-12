using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PandocGUI.Model;
using System;
using System.Diagnostics;
using System.IO;

namespace PandocGUI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Config _config;

        public Config Config
        {
            get { return _config; }
            set { Set(() => Config, ref _config, value); }
        }

        private PandocTask _task;

        public PandocTask Task
        {
            get { return _task; }
            set { Set(() => Task, ref _task, value); }
        }

        private Exception _lastError;

        public Exception LastError
        {
            get { return _lastError; }
            set { Set(() => LastError, ref _lastError, value); }
        }

        public RelayCommand BrowseForPandocExe { get; set; }
        public RelayCommand BrowseForSourceFile { get; set; }
        public RelayCommand BrowseForOutputFile { get; set; }
        public GalaSoft.MvvmLight.Command.RelayCommand Do { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Config = new Config() { PandocExePath = "pandoc.exe" };
            Task = new PandocTask();

            BrowseForPandocExe = new RelayCommand(() =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() ?? false)
                    {
                        Config.PandocExePath = dialog.FileName;
                    }
                });
            BrowseForSourceFile = new RelayCommand(() =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() ?? false)
                    {
                        Task.SourceFile = dialog.FileName;
                    }
                });
            BrowseForOutputFile = new RelayCommand(() =>
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    if (dialog.ShowDialog() ?? false)
                    {
                        Task.TargetFile = dialog.FileName;
                    }
                });
            Do = new RelayCommand(() =>
                {
                    try
                    {
                        System.Diagnostics.Process.Start(Config.PandocExePath, string.Format("{0} -f {1} -t {3} -s -o {2}"
                            , Task.SourceFile
                            , PandocFileExtension.Extensions[Path.GetExtension(Task.SourceFile)]
                            , Task.TargetFile
                            , PandocFileExtension.Extensions[Path.GetExtension(Task.TargetFile)]
                            ));
                    }
                    catch (Exception e)
                    {
                        Debugger.Break();
                        LastError = e;
                    }
                });
        }
    }
}