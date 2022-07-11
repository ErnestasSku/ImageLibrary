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
using ImageLibrary.Converters;
using ImageLibrary.Database;
using ImageLibrary.Database.Models;
using ImageLibrary.ViewModels.MainWindow;


namespace ImageLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static List<LibraryControlltem> LibraryList;
        private bool preview;
        public bool Preview {
            get { return preview; }
            set
            {
                if(preview != value)
                {
                    preview = value;
                    NotifyPropertyChanged("Preview");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            DataContext = this;
            Preview = true;
            InitializeComponent();
            PrepareLibraryList();
            PrepareBindings();
            
        }

        /// <summary>
        /// Prepares bindings for elements in UI.
        /// Note: most bindings are done in the xaml, this is used for special scenarios.
        /// </summary>
        public void PrepareBindings()
        {
            // CreationField uses the CreationField DataContext.
            // This binds Preview from this class to CreationField VisibilityProperty.
            Binding binding = new("Preview")
            {
                Source = this,
                Converter = new InverseVisibility(),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            CreationField.SetBinding(VisibilityProperty, binding);
        }

        /// <summary>
        /// Reads and loads existing libraries along with a new library.
        /// </summary>
        private void PrepareLibraryList()
        {
            LibraryList = App._library.Librarys.Select(p => new LibraryControlltem(p)).ToList();
            foreach (var control in LibraryList) { LibList.Children.Add(control); }
            var Add = new LibraryControlAdd();
            Add.MouseDown += (object sender, MouseButtonEventArgs args) => { Preview = false; };
            LibList.Children.Add(Add);
        }

        /// <summary>
        /// Implementation for changed property.
        /// </summary>
        /// <param name="propName">Name of the changed property</param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            Preview = true;
        }

        private void TestButton_Click2(object sender, RoutedEventArgs e)
        {
            Preview = false;
        }
    }
}
