using ImageLibrary.Database;
using System;
using System.Windows;

namespace ImageLibrary;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    App()
    {
        //StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        StartupUri = new Uri("Experimental/ExperimentWindow.xaml", UriKind.Relative);
    }
}
