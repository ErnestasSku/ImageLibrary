﻿namespace ImageLibrary.ViewModels.MainWindow;

internal interface ILibraryControl
{
    public string name { get; set; }
    public string path { get; set; }
    public string description { get; set; }
}
