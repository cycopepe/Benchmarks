using BenchmarkDotNet.Attributes;
using Benchmarks.Common;
using Benchmarks.Models;

namespace Benchmarks
{
    [HtmlExporter]
    public class DatabaseBenchmarkBase
    {
        protected SandboxContext _context;

        [IterationCleanup]
        public void Clean()
        {
            _context?.Dispose();
        }

        [IterationSetup]
        public void Setup()
        {
            _context = new SandboxContext(SandboxContextOptionsBuilder.GetOptions());
        }
    }
}