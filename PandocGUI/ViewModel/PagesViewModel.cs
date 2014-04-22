using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using PandocGUI.Model;
using PandocGUI.View;
using PandocGUI.ViewModel.Pandoc;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Page = PandocGUI.Model.Page;

namespace PandocGUI.ViewModel
{
    public class PagesViewModel : ViewModelBase
    {
        private Page _selectedPage;

        public Page SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                Set(() => SelectedPage, ref _selectedPage, value);
                ActualPage = ServiceLocator.Current.GetInstance(SelectedPage.ViewType) as UserControl;
                ActualPage.DataContext = ServiceLocator.Current.GetInstance(SelectedPage.ViewModelType);
            }
        }

        private UserControl _actualPage;

        public UserControl ActualPage
        {
            get { return _actualPage; }
            set { Set(() => ActualPage, ref _actualPage, value); }
        }

        private ObservableCollection<Page> _pages;

        public ObservableCollection<Page> Pages
        {
            get { return _pages; }
            set { Set(() => Pages, ref _pages, value); }
        }

        public PagesViewModel()
        {
            Pages = new ObservableCollection<Page>();
            Pages.Add(new Page() { DisplayName = "Config", ViewType = typeof(ConfigurationView), ViewModelType = typeof(ConfigViewModel) });
            Pages.Add(new Page() { DisplayName = "Tasks", ViewType = typeof(TaskListView), ViewModelType = typeof(TaskListViewModel) });
            Pages.Add(new Page() { DisplayName = "Save", ViewType = typeof(SaveTasksView), ViewModelType = typeof(TaskListViewModel) });
            Pages.Add(new Page() { DisplayName = "Load", ViewType = typeof(LoadTasksView), ViewModelType = typeof(TaskListViewModel) });
            Pages.Add(new Page() { DisplayName = "Execution", ViewType = typeof(ExecutionView), ViewModelType = typeof(ExecutionViewModel) });

            SelectedPage = Pages.First();
        }
    }
}