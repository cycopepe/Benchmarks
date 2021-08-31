using Benchmarks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Common
{
    public static class SandboxContextOptionsBuilder
    {
        private const string ConnectionString = "Data Source=.\\SQLexpress;Database=Sandbox;Trusted_Connection=True";

        public static DbContextOptions<SandboxContext> GetOptions() => new DbContextOptionsBuilder<SandboxContext>()
              .UseSqlServer(ConnectionString)
              .Options;

        public static DbContextOptions<SandboxContext> GetOptionsForDebug() => new DbContextOptionsBuilder<SandboxContext>()
              .LogTo(Console.WriteLine, LogLevel.Information)
              .UseSqlServer(ConnectionString)
              .Options;
    }
}
