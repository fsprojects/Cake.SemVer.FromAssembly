using Cake.Core.IO;
using Cake.Core.Tooling;

namespace  Cake.SynVer
{
    internal class SemVerSettings : ToolSettings
    {
        public FilePath Output { get; set; }
    }
}
