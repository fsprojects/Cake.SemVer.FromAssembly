using Cake.Core.IO;
using Cake.Core.Tooling;

namespace  Cake.SynVer
{
    internal class SynVerSettings : ToolSettings
    {
        public FilePath Output { get; set; }
    }
}
