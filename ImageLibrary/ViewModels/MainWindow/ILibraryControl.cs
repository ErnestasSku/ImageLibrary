using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLibrary.ViewModels.MainWindow
{
    internal interface ILibraryControl
    {
        public string name { get; set; }
        public string path { get; set; }
        public string description { get; set; }
    }
}
