using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace blocknetcore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var formatterSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
                formatterSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                formatterSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                formatterSettings.NullValueHandling = NullValueHandling.Ignore;
                formatterSettings.Formatting = Formatting.Indented;

                var jsonOutputFormatter = new JsonOutputFormatter(formatterSettings, ArrayPool<Char>.Shared);

                config.OutputFormatters.RemoveType<JsonOutputFormatter>();
                config.OutputFormatters.Insert(0, jsonOutputFormatter);
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
