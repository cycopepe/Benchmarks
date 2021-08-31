using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    [HtmlExporter]
    public class ProjectPropertiesBenchmark : DatabaseBenchmarkBase
    {
        private string _result = string.Empty;
        [Benchmark]
        public void AllProperties()
        {
            foreach (var team in _context.Teams)
            {
                _result = team.Name;
            }
        }

        [Benchmark]
        public void ProjectedProperties()
        {
            foreach (var teamName in _context.Teams.Select(x=>x.Name))
            {
                _result = teamName;
            }
        }

        [Benchmark]
        public void ProjectedPropertiesIntoNewObject()
        {
            foreach (var team in _context.Teams.Select(x => new { x.Name } ))
            {
                _result = team.Name;
            }
        }
    }
}
