﻿using System;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

using Semp.Infrastructure;
using Semp.Infrastructure.Data;
using Semp.Infrastructure.Localization;
using Semp.Module.Core.Extensions;
using Semp.Module.Localization;

namespace Semp.WebHost.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomizedIdentity(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/api"),
                a => a.Use(async (context, next) =>
                {
                    var principal = new ClaimsPrincipal();

                    var cookiesAuthResult = await context.AuthenticateAsync("Identity.Application");
                    if (cookiesAuthResult?.Principal != null)
                    {
                        principal.AddIdentities(cookiesAuthResult.Principal.Identities);
                    }

                    var bearerAuthResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                    if (bearerAuthResult?.Principal != null)
                    {
                        principal.AddIdentities(bearerAuthResult.Principal.Identities);
                    }

                    context.User = principal;
                    await next();
                }));

            return app;
        }

        public static IApplicationBuilder UseCustomizedMvc(this IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.Routes.Add(new UrlSlugRoute(routes.DefaultHandler));

                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
            return app;
        }

        public static IApplicationBuilder UseCustomizedStaticFiles(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = (context) =>
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            NoCache = true,
                            NoStore = true,
                            MaxAge = TimeSpan.FromDays(-1)
                        };
                    }
                });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = (context) =>
                    {
                        var headers = context.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            Public = true,
                            MaxAge = TimeSpan.FromDays(60)
                        };
                    }
                });
            }

            return app;
        }


        public static IApplicationBuilder UseCustomizedRequestLocalization(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var cultureRepository = scope.ServiceProvider.GetRequiredService<IRepositoryWithTypedId<Culture, string>>();
                GlobalConfiguration.Cultures = cultureRepository.Query().ToList();
            }

            var supportedCultures = GlobalConfiguration.Cultures.Select(c => c.Id).ToArray();
            app.UseRequestLocalization(options =>
            options
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures)
                .SetDefaultCulture(GlobalConfiguration.DefaultCulture)
                .RequestCultureProviders.Insert(0, new EfRequestCultureProvider())
            );

            return app;
        }

        /*public static IApplicationBuilder UseCustomizedRequestLocalizationBkp(this IApplicationBuilder app)
        {
            var cultureRepository = app.ApplicationServices.GetRequiredService<IRepositoryWithTypedId<Culture, string>>();
            var cultures = cultureRepository.Query().ToList();

            GlobalConfiguration.SimpleCultures = cultures.Select(x => new SimpleCulture { Id = x.Id, Name = x.Name }).ToList();
            var supportedCultures = cultures.Select(x => new CultureInfo(x.Id)).ToList();
            var defaultCulture = cultures.Where(x => x.IsDefault).Select(x => new CultureInfo(x.Id)).FirstOrDefault();
            if(defaultCulture == null)
            {
                defaultCulture = new CultureInfo("en-US");
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    //new AcceptLanguageHeaderRequestCultureProvider(),
                    new CookieRequestCultureProvider
                    {
                        CookieName = CookieRequestCultureProvider.DefaultCookieName
                    },
                    new QueryStringRequestCultureProvider()
                }
            });
            return app;
        }*/
    }
}
