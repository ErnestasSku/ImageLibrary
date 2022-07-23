using ImageLibrary.Database;
using ImageLibrary.Database.Models;
using Microsoft.EntityFrameworkCore;
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
    private static readonly object padlock = new();
    private static LibraryDatabase? _instance = null;

    public LibraryDbContext libraryDbContext;
    public List<string> LibraryNames;

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
        libraryDbContext.SavedChanges += UpdateSingleton;
        LibraryNames = GetLibraryNames();
    }

    private void UpdateSingleton(object? sender, SavedChangesEventArgs e)
    {
        LibraryNames = GetLibraryNames();
    }

    public static List<string> GetLibraryNames() => Instance.libraryDbContext.Librarys.Select(p => p.Name).ToList();

    public static DbSet<Library> Libraries => Instance.libraryDbContext.Librarys;

}
