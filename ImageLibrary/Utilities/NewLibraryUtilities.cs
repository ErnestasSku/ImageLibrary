using System.Linq;
using System.IO;
using ImageLibrary.Database;
using ImageLibrary.Singletons;

namespace ImageLibrary.Utilities;

public static class NewLibraryUtilities
{
    public static bool ValidDirectory(string path)
    {
        var info = new DirectoryInfo(path);
        if (!info.Exists)
            return false;

        var dirs = info.GetDirectories();
        var files = info.GetFiles();

        if (dirs.Length == 0 && files.Length == 0)
            return true;
        return false;
    }

    public static bool ValidTitle(string title) => LibraryDatabase.Instance.LibraryNames.All(x => !x.Equals(title));


    public static void InitialiseFolder(string path)
    {
        Directory.CreateDirectory(string.Concat(path, "\\", Resources.UploadFolder));
        Directory.CreateDirectory(string.Concat(path, "\\", Resources.StoredFolder));
        var db = new CategoryDbContext(path);
        var _ = db.Database.EnsureCreated();
        
    }
}
