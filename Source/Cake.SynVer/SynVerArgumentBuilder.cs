﻿using System;
using Cake.Core;
using Cake.Core.IO;

namespace  Cake.SynVer
{
    /// <summary>
    /// The top level argument builder for the SynVer CLI Tool
    /// </summary>
    /// <typeparam name="T">The Settings type to build arguments from</typeparam>
    internal abstract class SynVerArgumentBuilder<T>
        where T : SynVerSettings
    {
        private readonly ICakeEnvironment _environment;
        private readonly ProcessArgumentBuilder _builder;
        private readonly T _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SynVerArgumentBuilder{T}"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="setting">The settings</param>
        protected SynVerArgumentBuilder(ICakeEnvironment environment, T setting)
        {
            _environment = environment;
            _settings = setting;
            _builder = new ProcessArgumentBuilder();
        }

        /// <summary>
        /// Gets the Cake Environment
        /// </summary>
        protected ICakeEnvironment Environment => _environment;


        public ProcessArgumentBuilder GetArguments()
        {
            AddArguments(_builder, _settings);
            AddCommonArguments();
            return _builder;
        }

        /// <summary>
        /// Adds the arguments to the specified argument builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="settings">The settings.</param>
        protected abstract void AddArguments(ProcessArgumentBuilder builder, T settings);

        void AddCommonArguments()
        {
            if (_settings.Output != null) 
            {
                _builder.Append("--output");

                _builder.AppendQuoted( _settings.Output.MakeAbsolute(_environment).Normalize());
            }
        }
   }
}
