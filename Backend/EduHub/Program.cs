﻿using Loggly;
using Loggly.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;

namespace EduHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information().WriteTo.RollingFile("Eduhub.log")
                .CreateLogger();
            try
            {
                Log.Information("Starting....");
                BuildWebHost(args).Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Failed to start services");
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSetting("detailedErrors", "true")
                .CaptureStartupErrors(true)
                .Build();
    }
}
