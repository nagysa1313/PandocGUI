/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PandocGUI"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PandocGUI.ViewModel.Pandoc;

namespace PandocGUI.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ConfigViewModel>();
            SimpleIoc.Default.Register<PandocTaskViewModel>();
            SimpleIoc.Default.Register<TaskListViewModel>();
            SimpleIoc.Default.Register<PagesViewModel>();
            SimpleIoc.Default.Register<ExecutionViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public TaskListViewModel Tasks
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TaskListViewModel>();
            }
        }

        public ConfigViewModel Config
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigViewModel>();
            }
        }

        public PagesViewModel Pages
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PagesViewModel>();
            }
        }

        public ExecutionViewModel Execution
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExecutionViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}