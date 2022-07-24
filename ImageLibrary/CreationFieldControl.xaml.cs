using ImageLibrary.Database.Models;
using ImageLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ImageLibrary.Singletons;

namespace ImageLibrary;

/// <summary>
/// Interaction logic for CreationField.xaml
/// </summary>
public partial class CreationFieldControl : UserControl, INotifyPropertyChanged
{
    /// <summary>
    /// Invoked when the window is closed (either cancelled, or suceeded).
    /// During success, newly created library is added to the event args.
    /// </summary>
    public event EventHandler<CreationDoneEventArgs>? CreationDone;

    /// <summary>
    /// UI event for updating properties when they are changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;


    private bool? validTitle;
    public bool? ValidTitle
    {
        get { return validTitle; }
        set
        {
            if (value != validTitle)
            {
                validTitle = value;
                NotifyPropertyChanged("ValidTitle");
            }
        }
    }
    private bool validPath;
    public bool ValidPath
    {
        get { return validPath; }
        set
        {
            if (value != validPath)
            {
                validPath = value;
                NotifyPropertyChanged("ValidPath");
            }
        }
    }

    public CreationFieldControl()
    {
        ValidTitle = false;
        ValidPath = false;
        DataContext = this;
        InitializeComponent();
    }

    /// <summary>
    /// Resets the control to default state.
    /// Invoked after cancelled or succesfull creation.
    /// </summary>
    private void ResetControl()
    {
        TitleTextBox.Text = "";
        PathTextBox.Text = "";
        DescriptionTextBox.Text = "";

        TitleIndicatorImage.Visibility = Visibility.Hidden;
        PathIndicatorImage.Visibility = Visibility.Hidden;
    }

    
    public void NotifyPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

    /// <summary>
    /// Creates a new library, invokes CreationDone event and resets control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AcceptButton_Click(object sender, RoutedEventArgs e)
    {
        var title = TitleTextBox.Text;
        var path = PathTextBox.Text;
        var description = DescriptionTextBox.Text;

        Library NewLibrary = new Library(path, title, description);
        CreationDone?.Invoke(this, new CreationDoneEventArgs(false, NewLibrary));
        ResetControl();
    }

    /// <summary>
    /// Cancells the creation process and resets the control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        CreationDone?.Invoke(this, new CreationDoneEventArgs(true));
        ResetControl();
    }

    private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidTitle = !TitleTextBox.Text.Equals("") && NewLibraryUtilities.ValidTitle(TitleTextBox.Text);
        if (TitleIndicatorImage.Visibility == Visibility.Hidden)
            TitleIndicatorImage.Visibility = Visibility.Visible;
    }
    
    private void PathTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidPath = !PathTextBox.Text.Equals("") && NewLibraryUtilities.ValidDirectory(PathTextBox.Text);
        if (PathIndicatorImage.Visibility == Visibility.Hidden)
            PathIndicatorImage.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Opens a new directory.
    /// NOTE: most likely place to cause problems in the future
    ///   due to using Dialog from WinForms.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DirectorySelectionButton_Click(object sender, RoutedEventArgs e)
    {
        using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
        {
            var result = dialog.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            PathTextBox.Text = dialog.SelectedPath;
        }

    }
}

