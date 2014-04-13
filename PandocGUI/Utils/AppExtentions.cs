using PandocGUI;
using PandocGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace System
{
    public static class AppExtentions
    {
        public static T GetCurrentResource<T>(this object any, string resourceKey)
            where T : class
        {
            return GetResource<T>(App.Current, resourceKey);
        }

        public static T GetResource<T>(this Application any, string resourceKey)
            where T : class
        {
            return App.Current.Resources[resourceKey] as T;
        }

        public static ViewModelLocator GetLocator(this object any, string locatorKey = "Locator")
        {
            return any.GetCurrentResource<ViewModelLocator>("Locator");
        }
    }
}