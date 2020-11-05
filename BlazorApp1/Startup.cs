using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp1.Data;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Telerik.Blazor.Services;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp1
{
    public class Startup
    {
        //dependency injection
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices( IServiceCollection services )
        {
            #region Localization

            services.AddControllers( );
            services.AddLocalization( options => options.ResourcesPath = "Resources" );
            services.Configure<RequestLocalizationOptions>( options =>
            {
                var supportedCultures = new List<CultureInfo>( )
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("es-ES"),
                    new CultureInfo("bg-BG"),
                    new CultureInfo("hu-HU"),
                };

                options.DefaultRequestCulture = new RequestCulture( "hu-HU" );
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            } );

            #endregion

            // service-ek hozzáadása, pl: autentikáció, razor pages, mvc controller routing, stb...
            services.AddRazorPages( );
            services.AddTelerikBlazor( );
            services.AddServerSideBlazor( );
            services.AddSingleton<WeatherForecastService>( );
            services.AddSingleton( typeof( ITelerikStringLocalizer ), typeof( SampleResxLocalizer ) );
            services.AddSingleton<CountryService>( );
            services.AddSingleton<AppState>( );

            services.AddScoped<AuthenticationStateProvider, WinAuthStateProvider>( );

            services.AddAuthorizationCore( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            //Lokalizáció
            app.UseRequestLocalization( app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>( ).Value );

            if ( env.IsDevelopment( ) )
            {
                app.UseDeveloperExceptionPage( );
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts( );
            }

            app.UseHttpsRedirection( );

            //In ASP.NET Core, support for requests for static files ( like JavaScript, CSS, and image files) 
            //must be explicitly enabled, and only files in the app's wwwroot folder are publicly addressable by default.
            app.UseStaticFiles( );

            app.UseRouting( );

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers( );
                 endpoints.MapBlazorHub( );
                 endpoints.MapFallbackToPage( "/_Host" );
             } );
        }
    }
}
