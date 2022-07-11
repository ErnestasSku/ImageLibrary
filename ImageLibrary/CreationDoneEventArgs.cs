using ImageLibrary.Database.Models;
using System;

namespace ImageLibrary
{
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
}