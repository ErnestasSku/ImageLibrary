using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ImageLibrary.Converters;
using ImageLibrary.Singletons;
using ImageLibrary.Utilities;
using ImageLibrary.ViewModels.MainWindow;


namespace ImageLibrary;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    LibraryControlAdd libraryAddControl;

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
        LibList.Children.Clear();
        var list = LibraryDatabase.Libraries.Select(p => new LibraryControlltem(p)).ToList();
        foreach (var control in list) { LibList.Children.Add(control); }
        libraryAddControl = new();
        libraryAddControl.MouseDown += (object sender, MouseButtonEventArgs args) => { Preview = false; };
        LibList.Children.Add(libraryAddControl);
    }

    /// <summary>
    /// Implementation for changed property.
    /// </summary>
    /// <param name="propName">Name of the changed property</param>
    public void NotifyPropertyChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    private void TestButton_Click(object sender, RoutedEventArgs e) => Preview = true;
    

    private void TestButton_Click2(object sender, RoutedEventArgs e) => Preview = false;

    private void CreationField_CreationDone(object sender, CreationDoneEventArgs e)
    {
        var creationFieldControl = (CreationFieldControl)sender;
        Preview = true;

        if (e.Cancelled || e.Library == null)
            return;
        LibraryDatabase.Libraries.Add(e.Library);
        LibraryDatabase.Instance.libraryDbContext.SaveChangesAsync();

        NewLibraryUtilities.InitialiseFolder(e.Library.Path);

        LibList.Children.Remove(libraryAddControl);
        LibList.Children.Add(new LibraryControlltem(e.Library));
        LibList.Children.Add(libraryAddControl);
    }
}
