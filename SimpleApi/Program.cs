using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NLog;

namespace SimpleApi
{
    public class Program
    {

        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public static IConfigurationSection SimpleApiConfig;
        static IWebHost WebHost;

        public static void Main(string[] args)
        {
            try {
                _Main(args);
            }
            catch(Exception ex)
            {
                Logger.Fatal("{0} \n     StackTrace:\n     {1}",ex.Message,ex.StackTrace);
            }
        }

        private static void _Main(string[] args)
        {
            if (null == Logger)
            {
                Console.WriteLine("Logger is null!!!!");
                throw new ArgumentNullException("logger");
            }
            
            Logger.Info("Starting SimpleApi...");

            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();

            Logger.Debug("Config built...");

            SimpleApiConfig = new ConfigurationBuilder()
                .SetBasePath(Directory
                .GetCurrentDirectory())
                .AddJsonFile("props.json", optional: true, reloadOnChange: true)
                .Build()
                .GetSection("SimpleApi");

            WebHost = new WebHostBuilder()
                .UseConfiguration(config) 
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            
            Logger.Debug("WebHost built...");

            WebHost.Run();

        }
    }
}
