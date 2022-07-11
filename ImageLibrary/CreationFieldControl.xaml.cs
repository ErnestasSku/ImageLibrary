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

    /// <summary>
    /// Class which convers bool to specified image.
    /// </summary>
    class BoolToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new BitmapImage();
            if ((bool)value)
            {
                Uri resourseUri = new Uri(Resources.CheckIcon, UriKind.Relative);
                return new BitmapImage(resourseUri);
            }
            else
            {
                Uri resourseUri = new Uri(Resources.RemoveIcon, UriKind.Relative);
                return new BitmapImage(resourseUri);
            }
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage img = (BitmapImage)value;
            if (img != null)
            {
                if (img.UriSource == new Uri(Resources.CheckIcon, UriKind.Relative))
                    return true;
                else if (img.UriSource == new Uri(Resources.RemoveIcon, UriKind.Relative))
                    return false;
                else
                    return false;
            }

            return null;
        }
    }
}
