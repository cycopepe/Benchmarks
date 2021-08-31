using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Common
{
    public class AsyncAwaitBenchmark
    {
        [Benchmark]
        public void sync() {
            for (int i = 0; i < 10; i++) {
                DoStuff();
            }
        }

        [Benchmark]
        public async Task AwaitAsyncWitoutDiscard()
        {
            for (int i = 0; i < 10; i++)
            {
                await DoStuff();
            }
        }

        [Benchmark]
        public async Task AwaitAsyncWitout()
        {
            for (int i = 0; i < 10; i++)
            {
                _ = await DoStuffReturn();
            }
        }

        private async Task DoStuff() {
            //Console.WriteLine("Start doing stuff");
            await Task.Delay(1000);
            //Console.WriteLine($"Finishing doing stuff");
            //return Task.FromResult(true);
        }

        private async Task<bool> DoStuffReturn()
        {
            //Console.WriteLine("Start doing stuff");
            await Task.Delay(1000);
            //Console.WriteLine($"Finishing doing stuff");
            return true;
        }
    }
}
