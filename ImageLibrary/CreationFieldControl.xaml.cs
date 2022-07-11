using ImageLibrary.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageLibrary
{
    /// <summary>
    /// Interaction logic for CreationField.xaml
    /// </summary>
    public partial class CreationFieldControl : UserControl, INotifyPropertyChanged
    {
        //public delegate void CreationDoneEventHandler(object sender, CreationDoneEventArgs e);
        //public event CreationDoneEventHandler CreationDone;

        public event EventHandler<CreationDoneEventArgs>? CreationDone;
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Preview { get; set;}

        private bool? validTitle;
        public bool? ValidTitle 
        { 
            get { return validTitle; } 
            set
            {
                if(value != validTitle)
                {
                    validTitle = value;
                    NotifyPropertyChanged("ValidTitle");
                }
            }
        }
        private bool? validPath; 
        public bool? ValidPath 
        { 
            get { return validPath; }
            set
            {
                if(value != validPath)
                {
                    validPath = value;
                    NotifyPropertyChanged("ValidPath");
                }
            }
        }

        public CreationFieldControl()
        {
            ValidTitle = null;
            ValidPath = null;
            DataContext = this;
            InitializeComponent();
        }

        public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleTextBox.Text;
            var path = PathTextBox.Text;
            var description = DescriptionTextBox.Text;

            Library NewLibrary = new Library(path, title, description);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CreationDone?.Invoke(this, new CreationDoneEventArgs(false));
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidTitle = !TitleTextBox.Text.Equals("") ? true : false;
        }

        private void PathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidPath = !PathTextBox.Text.Equals("") ? true : false;
        }
    }

}
