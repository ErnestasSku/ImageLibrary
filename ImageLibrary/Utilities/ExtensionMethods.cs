using Microsoft.EntityFrameworkCore;

namespace ImageLibrary.Utilities;

public static class ExtensionMethods
{
    public static DbContextOptionsBuilder DatabaseName(this DbContextOptionsBuilder dbContextBuilder, string path)
    {
        return dbContextBuilder.UseSqlite(string.Concat("Data source=", path, "\\", Resources.LibraryDatabase));
    }
}
