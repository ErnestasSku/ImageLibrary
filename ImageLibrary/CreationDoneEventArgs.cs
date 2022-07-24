using ImageLibrary.Database.Models;
using System;

namespace ImageLibrary;

/// <summary>
/// Creation Done Event Argument.
/// Cancelled - tells whether the creation screen was cancelled or succesfull.
/// Library - in the event that creation succeded, Libray is added to the event arguments.
/// </summary>
public class CreationDoneEventArgs : EventArgs
{
    public bool Cancelled;
    public Library? Library;
    
    public CreationDoneEventArgs(bool cancelled, Library? library = null)
    {
        Cancelled = cancelled;
        Library = library;
    }
}