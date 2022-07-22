using ImageLibrary.Database;
using ImageLibrary.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ImageLibrary.Singletons;

/// <summary>
/// Library Database wrapper. It is used to abstract the call to the database.
/// Note: Calling Database directly using EF core causes problems with XAML designer.
/// </summary>
public sealed class LibraryDatabase
{   
    public LibraryDbContext libraryDbContext;

    private static readonly object padlock = new();
    private static LibraryDatabase? _instance = null;

    public static LibraryDatabase Instance
    {
        get 
        { 
            lock(padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new LibraryDatabase();
                    }
                    return _instance;
                }
        }
    }
    private LibraryDatabase()
    {
        libraryDbContext = new LibraryDbContext();
    }

    public static List<string> GetLibraryNames() => Instance.libraryDbContext.Librarys.Select(p => p.Name).ToList();

}
