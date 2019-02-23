﻿using GraphiQl;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Extension;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.Service;
using RawCMS.Library.Service.Contracts;
using RawCMS.Plugins.GraphQL.Classes;
using System;

namespace RawCMS.Plugins.GraphQL
{
    public class GraphQLPlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<GraphQLSettings>
    {
        public override string Name => "GraphQL";

        public override string Description => "Add GraphQL CMS capabilities";

        public override void Init()
        {
            Logger.LogInformation("GraphQL plugin loaded");
        }

        GraphQLService graphService = new GraphQLService();

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSingleton(s => graphService);
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddScoped<ICollectionMetadata, CollectionMetadataService>();
            services.AddScoped<ISchema, GraphQLSchema>();

        }

        private AppEngine appEngine;
        public override void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            this.appEngine = appEngine;
            graphService.SetCRUDService(this.appEngine.Service);
            graphService.SetLogger(this.appEngine.GetLogger(this));
            graphService.SetSettings(this.config);

            base.Configure(app, appEngine);

            app.UseMiddleware<GraphQLMiddleware>();
            //app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            //{
            //    BuildUserContext = ctx => new GraphQLUserContext
            //    {
            //        User = ctx.User
            //    },
            //    EnableMetrics = this.config.EnableMetrics
            //});

            app.UseGraphiQl(this.config.GraphiQLPath, this.config.Path);
        }

      
        private IConfigurationRoot configuration;

        public override void Setup(IConfigurationRoot configuration)
        {
            base.Setup(configuration);
            this.configuration = configuration;
        }

        public GraphQLSettings GetDefaultConfig()
        {
            return new GraphQLSettings
            {
                Path = "/api/graphql",
                EnableMetrics = false,
                GraphiQLPath = "/graphql"
            };
        }

        private GraphQLSettings config;

        public void SetActualConfig(GraphQLSettings config)
        {
            this.config = config;
        }
    }
}