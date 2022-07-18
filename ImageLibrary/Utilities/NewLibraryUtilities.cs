using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ImageLibrary.Database;

namespace ImageLibrary.Utilities
{
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

        public static void InitialiseFolder(string path)
        {
            Directory.CreateDirectory(string.Concat(path, "\\", Resources.UploadFolder));
            Directory.CreateDirectory(string.Concat(path, "\\", Resources.StoredFolder));
            var db = new CategoryDbContext(path);
            var result = db.Database.EnsureCreated();

        }
    }
}
