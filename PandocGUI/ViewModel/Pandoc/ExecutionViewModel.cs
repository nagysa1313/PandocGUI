using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PandocGUI.Model;
using PandocGUI.Utils;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.ViewModel.Pandoc
{
    public class ExecutionViewModel : ViewModelBase
    {
        private static IRepository<PandocTask, int> TaskRepository = RepositoryFactory.GetInstance<PandocTask>();

        private ObservableCollection<Tuple<PandocTask, PandocTaskResult>> _tasks;

        public ObservableCollection<Tuple<PandocTask, PandocTaskResult>> Tasks
        {
            get { return _tasks; }
            set { Set(() => Tasks, ref _tasks, value); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(() => IsBusy, ref _isBusy, value); }
        }

        public RelayCommand Do { get; private set; }

        public ExecutionViewModel()
        {
            Tasks = new ObservableCollection<Tuple<PandocTask, PandocTaskResult>>();
            Do = new RelayCommand(() =>
                {
                    if (IsBusy) return;

                    IsBusy = true;
                    Task.Factory.StartNew(() =>
                        {
                            App.Current.Dispatcher.Invoke(() =>
                                {
                                    Tasks.Clear();
                                });

                            var tasks = TaskRepository.GetAll();

                            PandocRunner.Run(this.GetLocator().Config.Model.PandocExePath, tasks, (task, result) =>
                                {
                                    App.Current.Dispatcher.Invoke(() =>
                                        {
                                            this.GetLocator().Execution.Tasks.Add(new Tuple<PandocTask, PandocTaskResult>(task, result));
                                        });
                                });


                        }).ContinueWith(task => IsBusy = false);
                });
        }
    }
}