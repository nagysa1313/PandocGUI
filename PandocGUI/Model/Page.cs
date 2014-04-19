using GalaSoft.MvvmLight;
using SharpRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    public class Page : ObservableObject
    {
        private int _key;

        public int Key
        {
            get { return _key; }
            set { Set(() => Key, ref _key, value); }
        }

        private int _order;

        public int Order
        {
            get { return _order; }
            set { Set(() => Order, ref _order, value); }
        }

        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set { Set(() => DisplayName, ref _displayName, value); }
        }

        private Type _viewType;

        public Type ViewType
        {
            get { return _viewType; }
            set { Set(() => ViewType, ref _viewType, value); }
        }

        private Type _viewModelType;

        public Type ViewModelType
        {
            get { return _viewModelType; }
            set { Set(() => ViewModelType, ref _viewModelType, value); }
        }
    }
}