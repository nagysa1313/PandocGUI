using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PandocGUI.Utils
{
    /// <summary>
    /// This class is borrowed from codeproject.com/Articles/27432/Artificial-Inheritance-Contexts-in-WPF
    /// </summary>
    public class DataContextSpy : Freezable
    {
        public DataContextSpy()
        {
            BindingOperations.SetBinding(this, DataContextProperty, new Binding());

            this.IsSynchronizedWithCurrentItem = true;
        }

        public bool IsSynchronizedWithCurrentItem { get; set; }

        public object DataContext
        {
            get { return (object)GetValue(DataContextProperty); }
            set { SetValue(DataContextProperty, value); }
        }

        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.Register("DataContext", typeof(object),
                typeof(DataContextSpy), new PropertyMetadata(null, null, OnCoerceDataContext));

        private static object OnCoerceDataContext(DependencyObject d, object baseValue)
        {
            DataContextSpy spy = d as DataContextSpy;
            if (spy == null) return baseValue;

            if (spy.IsSynchronizedWithCurrentItem)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(baseValue);
                if (view != null)
                    return view.CurrentItem;
            }

            return baseValue;
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }
    }
}