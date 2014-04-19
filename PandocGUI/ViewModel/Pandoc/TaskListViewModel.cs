using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PandocGUI.Model;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.ViewModel.Pandoc
{
    public class TaskListViewModel : ViewModelBase
    {
        private static IRepository<PandocTask, int> TaskRepository = RepositoryFactory.GetInstance<PandocTask>();

        private ObservableCollection<PandocTask> _tasks;

        public ObservableCollection<PandocTask> Tasks
        {
            get { return _tasks; }
            set { Set(() => Tasks, ref _tasks, value); }
        }

        private PandocTask _selectedTask;

        public PandocTask SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                Set(() => SelectedTask, ref _selectedTask, value);
                RaisePropertyChanged(() => SelectedTaskViewModel);
            }
        }

        public PandocTaskViewModel SelectedTaskViewModel
        {
            get
            {
                var vm = ServiceLocator.Current.GetInstance<PandocTaskViewModel>();
                vm.Model = SelectedTask;
                vm.Result = null;
                vm.IsBusy = false;
                return vm;
            }
        }

        public RelayCommand Add { get; private set; }
        public RelayCommand Save { get; private set; }
        public RelayCommand Load { get; private set; }

        public TaskListViewModel()
        {
            Tasks = new ObservableCollection<PandocTask>(TaskRepository.ToList());
            SelectedTask = Tasks.Any() ? Tasks.First() : null;

            Add = new RelayCommand(() =>
                {
                    var task = new PandocTask();
                    Tasks.Add(task);
                });

            Save = new RelayCommand(() =>
                {
                    var trans = TaskRepository.BeginBatch();
                    foreach (var task in Tasks)
                    {
                        if (TaskRepository.Exists(task.Key))
                            trans.Update(task);
                        else trans.Add(task);
                    }
                    trans.Commit();
                });

            Load = new RelayCommand(() =>
                {
                    Tasks.Clear();
                    foreach (var task in TaskRepository.GetAll())
                    {
                        Tasks.Add(task);
                    }
                });
        }
    }
}