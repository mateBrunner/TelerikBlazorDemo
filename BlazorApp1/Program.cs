using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp1
{
    public class Program
    {
        public static void Main( string[] args )
        {
            CreateHostBuilder( args ).Build( ).Run( );
        }


        //The web host manages the Blazor Server app's lifecycle and sets up host-level services. 
        //Examples of such services are configuration, logging, dependency injection, and the HTTP server.
        public static IHostBuilder CreateHostBuilder( string[] args ) =>
            Host.CreateDefaultBuilder( args )
                .ConfigureWebHostDefaults( webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>( );
                 } );
    }
}
