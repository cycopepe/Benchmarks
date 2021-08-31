using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Loggers;
using Benchmarks.Common;
using Benchmarks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    [HtmlExporter]
    public class LoadRelatedEntities : DatabaseBenchmarkBase
    {
        
        private readonly ConsoleLogger _logger;
        private string _result = string.Empty;
        public LoadRelatedEntities()
        {
            _logger = new ConsoleLogger();
        }

        [Benchmark]
        public void DefaultLazy()
        {
            foreach (var team in _context.Teams)
            {
                foreach (var player in team.Players) {
                    _result = player.Lastname;
                }
            }
        }        

        [Benchmark]
        public void LoadRelated()
        {
            var teams = _context.Teams.Include(p => p.Players);
            foreach (var team in teams)
            {
                foreach (var player in team.Players)
                {
                    _result = player.Lastname;
                }
            }
        }

        [Benchmark]
        public void LoadRelatedToList()
        {
            var teams = _context.Teams.Include(p => p.Players).ToList();
            foreach (var team in teams)
            {
                foreach (var player in team.Players)
                {
                    _result = player.Lastname;
                }
            }
        }

        [Benchmark]
        public void LoadRelatedAsSplitQuery()
        {
            var teams = _context.Teams.Include(p => p.Players).AsSplitQuery().ToList();
            foreach (var team in teams)
            {
                foreach (var player in team.Players)
                {
                    _result = player.Lastname;
                }
            }
        }
    }
}
