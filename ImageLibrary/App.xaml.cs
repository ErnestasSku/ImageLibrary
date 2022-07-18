using ImageLibrary.Database;
using System.Windows;

namespace ImageLibrary;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static LibraryDbContext _library = new LibraryDbContext();

}
