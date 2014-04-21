using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PandocGUI.Utils.Behaviors
{
    public class BrowseForBehavior : Behavior<Button>
    {
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string),
                typeof(BrowseForBehavior), new PropertyMetadata(null));

        public bool UseOpenFileDialog
        {
            get { return (bool)GetValue(UseOpenFileDialogProperty); }
            set { SetValue(UseOpenFileDialogProperty, value); }
        }

        public static readonly DependencyProperty UseOpenFileDialogProperty =
            DependencyProperty.Register("UseOpenFileDialog", typeof(bool),
                typeof(BrowseForBehavior), new PropertyMetadata(false));

        #region AfterDialogCommand
        public ICommand AfterDialogCommand
        {
            get { return (ICommand)GetValue(AfterDialogCommandProperty); }
            set { SetValue(AfterDialogCommandProperty, value); }
        }

        public static readonly DependencyProperty AfterDialogCommandProperty =
            DependencyProperty.Register("AfterDialogCommand", typeof(ICommand),
                typeof(BrowseForBehavior), new PropertyMetadata(null));

        public object AfterDialogCommandParameter
        {
            get { return (object)GetValue(AfterDialogCommandParameterProperty); }
            set { SetValue(AfterDialogCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty AfterDialogCommandParameterProperty =
            DependencyProperty.Register("AfterDialogCommandParameter", typeof(object),
                typeof(BrowseForBehavior), new PropertyMetadata(null)); 
        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.Click += AssociatedObject_Click;
        }

        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (UseOpenFileDialog)
            {
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog() ?? false)
                {
                    Path = dialog.FileName;
                }
            }
            else
            {
                var dialog = new SaveFileDialog();
                if (dialog.ShowDialog() ?? false)
                {
                    Path = dialog.FileName;
                }
            }

            if (AfterDialogCommand != null && AfterDialogCommand.CanExecute(AfterDialogCommandParameter))
            {
                AfterDialogCommand.Execute(AfterDialogCommandParameter);
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Click -= AssociatedObject_Click;

            base.OnDetaching();
        }
    }
}
