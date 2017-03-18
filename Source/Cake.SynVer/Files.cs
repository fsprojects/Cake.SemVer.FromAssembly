using Cake.Core.IO;
using SysPath = System.IO.Path;

namespace Cake.SynVer
{
    internal static class Files
    {
        public static string Normalize(this FilePath path)
        {
            return path.FullPath.Replace(SysPath.AltDirectorySeparatorChar, SysPath.DirectorySeparatorChar);
        }
    }
}
