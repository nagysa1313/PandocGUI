using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PandocGUI.Model;
using SharpRepository.Repository;
using System;
using System.Linq;

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
        private static IRepository<PandocTask, int> TaskRepository = RepositoryFactory.GetInstance<PandocTask>();

        public RelayCommand Save { get; private set; }
        public RelayCommand Load { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Save = new RelayCommand(() =>
                {
                    var taskVM = this.GetLocator().Task;

                    if (TaskRepository.Exists(taskVM.Model.Key))
                        TaskRepository.Update(taskVM.Model);
                    else TaskRepository.Add(taskVM.Model);

                    taskVM.Model = new PandocTask();
                });

            Load = new RelayCommand(() =>
                {
                    var taskVM = this.GetLocator().Task;
                    taskVM.Model = TaskRepository.Any() ? TaskRepository.First() : taskVM.Model;
                });
        }
    }
}