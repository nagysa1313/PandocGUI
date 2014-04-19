using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.View
{
    public class ViewLocator
    {
        public ViewLocator()
        {
            SimpleIoc.Default.Register<PagesView>();
            SimpleIoc.Default.Register<ConfigurationView>();
            SimpleIoc.Default.Register<TaskView>();
            SimpleIoc.Default.Register<LoadTasksView>();
            SimpleIoc.Default.Register<SaveTasksView>();
        }

        public PagesView Pages
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PagesView>();
            }
        }

        public ConfigurationView Config
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigurationView>();
            }
        }
    }
}