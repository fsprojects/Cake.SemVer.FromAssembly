using System;
using Cake.Core.Tooling;
using Cake.Testing.Fixtures;

namespace  Cake.SynVer.Tests
{
    internal abstract class SemVerFixture<TSettings>:ToolFixture<TSettings>
                      where TSettings : ToolSettings, new()
    {
        protected SemVerFixture():base("SynVer.exe")
        {
        }
    }
}
