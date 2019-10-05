﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Min�</author>
// <autogenerated>true</autogenerated>
//******************************************************************************

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Extension;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Plugins.KeyStore.Contracts;

namespace RawCMS.Plugins.KeyStore
{
    public class KeyStoreSettings
    {
    }

    public class KeyStorePlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<KeyStoreSettings>
    {
        public override string Name => "KeyStore";

        public override string Description => "Add KeyStore capabilities";

        private readonly KeyStoreSettings config;
        

        public KeyStorePlugin(AppEngine appEngine, KeyStoreSettings config, ILogger logger) : base(appEngine, logger)
        {
            this.config = config;
            Logger.LogInformation("KeyStore plugin loaded");
        }

        private KeyStoreService keyStoreService = new KeyStoreService();

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingletonWithOverride<IKeyStoreService, KeyStoreService>(this.Engine);
        }

        

        public override void Configure(IApplicationBuilder app)
        {
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
        }

        public override void Setup(IConfigurationRoot configuration)
        {
        }
    }
}