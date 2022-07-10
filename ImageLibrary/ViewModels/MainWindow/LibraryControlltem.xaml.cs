using System;
using System.Collections.Generic;
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
using ImageLibrary.Database.Models;

namespace ImageLibrary.ViewModels.MainWindow
{
    /// <summary>
    /// Interaction logic for LibraryControlltem.xaml
    /// </summary>
    public partial class LibraryControlltem : UserControl, ILibraryControl
    {
        public string name { get; set; }
        public string path { get; set; }
        public string description { get; set; }

        public LibraryControlltem()
        {
            InitializeComponent();
        }

        public LibraryControlltem(Library library)
        {
            name = library.Name;
            path = library.Path;
            description = library.Description ?? "";
            DataContext = this;
            InitializeComponent();
        }
    }
}
