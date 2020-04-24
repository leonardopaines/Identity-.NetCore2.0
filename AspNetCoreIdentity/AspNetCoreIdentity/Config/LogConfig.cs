using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Config
{
    public static class LogConfig
    {

        public static IApplicationBuilder UseLogConfig(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options =>
               options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                   configuration["KissLog.OrganizationId"],
                   configuration["KissLog.ApplicationId"])
               )));

            return app;
        }
    }
}
