using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using System.Runtime.InteropServices;
namespace Cake.SynVer
{
    /// <summary>
    /// <para>Contains functionality related to the <see href="https://github.com/fsprojects/SyntacticVersioning">SyntacticVersioning</see>.</para>
    /// <para>
    /// In order to use the commands for this addin you will need to include the following in your build.cake file to download and
    /// reference the addin from NuGet.org:
    /// <code>
    /// #addin Cake.SynVer
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("SynVer")]
    [ComVisible(true)]
    public static class SynVerAliases
    {
        /// <summary>
        /// Get the magnitude by running SynVer.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="original">the previously published dll</param>
        /// <param name="next">the dll to be published</param>
        /// <example>
        /// <code>
        /// SemVerMagnitude("./packages/NAME/45/NAME.dll", "NAME/bin/Debug/NAME.dll");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static Magnitude SemVerMagnitude(this ICakeContext context, FilePath original, FilePath next)
        {
            return SemVerMagnitude(context, original, next, null);
        }
        /// <summary>
        /// Get the magnitude by running SynVer.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="original">the previously published dll</param>
        /// <param name="next">the dll to be published</param>
        /// <param name="output">send output to a file</param>
        /// <example>
        /// <code>
        /// SemVerMagnitude("./packages/NAME/45/NAME.dll", "NAME/bin/Debug/NAME.dll", "output.txt");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static Magnitude SemVerMagnitude(this ICakeContext context, FilePath original, FilePath next, FilePath output)
        {
            var runner = new SynVerMagnitudeRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            return runner.SemVerMagnitude(original, next, new SynVerMagnitudeSettings { Output = output });
        }
    }
}
