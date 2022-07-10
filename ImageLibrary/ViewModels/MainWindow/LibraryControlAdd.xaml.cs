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

namespace ImageLibrary.ViewModels.MainWindow
{
    /// <summary>
    /// Interaction logic for LibraryControllAdd.xaml
    /// </summary>
    public partial class LibraryControlAdd : UserControl, ILibraryControl
    {
        public string name { get; set; }
        public string path { get; set; }
        public string description { get; set; }

        public LibraryControlAdd()
        {
            name = "Add new item";
            path = "";
            description = "Click to add new item";
            DataContext = this;
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            var control = sender as LibraryControlAdd;
            control.Background = new SolidColorBrush(Colors.RoyalBlue);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            var control = sender as LibraryControlAdd;
            control.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
