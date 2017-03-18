using System;
using Cake.Core.IO;

namespace Cake.SynVer.Tests
{
    internal class SemVerMagnitudeRunnerFixture : SemVerFixture<SynVerMagnitudeSettings>
    {
        public FilePath Original;

        public FilePath New;

        public SemVerMagnitudeRunnerFixture()
        {
            Original = "c:/temp/original.dll";
            New = "c:/temp/new.dll";
        }
        protected override void RunTool()
        {
            var tool = new SynVerMagnitudeRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.SemVerMagnitude(Original, New, Settings);
        }
    }
}