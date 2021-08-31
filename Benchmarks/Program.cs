using Benchmarks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Benchmarks
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SandboxContext>();
            //AddTestData(context);

            // entry to run app
            await serviceProvider.GetService<App>().Run(args);

            //BenchmarkRunner.Run<StringConcatBenchmark>();
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
            //Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // configure logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            services.Configure<AppSettings>(configuration.GetSection("App"));

            //services.AddDbContext<DatabaseContext>(opttions => opttions.UseInMemoryDatabase("data"));
            services.AddDbContext<SandboxContext>(opttions => opttions.UseSqlServer("Data Source=.\\SQLexpress;Database=Sandbox;Trusted_Connection=True"));


            // add services:
            //services.AddTransient<IMyRespository, MyConcreteRepository>();

            // add app
            services.AddTransient<App>();
        }

        private static void AddTestData(SandboxContext context)
        {
            int players = 0;
            for (int i = 0; i < 1000; i++)
            {
                var team = new Team
                {
                    Id = i,
                    Name = "Team" + i,
                    Players = new List<Player>()
                };

                context.Teams.Add(team);

                for (int j = 0; j < 100; j++)
                {
                    var player = new Player
                    {
                        Id = players,
                        Firstname = "first " + players,
                        Lastname = "last " + players,
                        TeamId = i
                    };

                    context.Players.Add(player);
                    players++;
                }
            }

            context.SaveChanges();
        }
    }
}
