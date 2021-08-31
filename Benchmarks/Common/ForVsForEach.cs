using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Common
{
    [HtmlExporter]
    public class ForVsForEach
    {
        public List<int> BigList { get; set; }
        public List<int> CopyBigList { get; set; }
        public int _result = 0;
        private int[] arrayList;

        [Params(1, 10, 100, 1_000, 10_000, 100_000, 1000_000, 10_000_000)]
        public int Param { get; set; }

        public ForVsForEach()
        {
            Setup();
        }

        [IterationSetup]
        public void Setup()
        {
            BigList = new List<int>();
            CopyBigList = new List<int>();
            for (int i = 0; i < Param; i++) {
                BigList.Add(i);
            }
            arrayList = BigList.ToArray();    
        }

        [Benchmark]
        public void ForTest()
        {
            for (int i = 0; i < Param; i++)
            {
                _result = BigList[i];
            }
        }

        [Benchmark]
        public void ForEachTest()
        {
            foreach (var item in BigList)
            {
                _result = item;
            }
        }

        [Benchmark]
        public void ForTestSingle()
        {
            for (int i = 0; i < Param; i++)
            {
                _result = BigList[i];
                CopyBigList.Add(_result);                
            }
        }

        [Benchmark]
        public void ForEachTestSingle()
        {
            foreach (var item in BigList)
            {
                _result = item;
                CopyBigList.Add(_result);                
            }
        }

        [Benchmark]
        public void ForTestMultiple()
        {
            for (int i = 0; i < Param; i++)
            {
                _result = BigList[i];
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
            }
        }

        [Benchmark]
        public void ForEachTestMultiple()
        {
            foreach (var item in BigList)
            {
                _result = item;
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
            }
        }

        [Benchmark]
        public void ForTestMultipleArray()
        {
            for (int i = 0; i < Param; i++)
            {
                _result = arrayList[i];
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
            }
        }

        [Benchmark]
        public void ForEachTestMultipleArray()
        {
            foreach (var item in arrayList)
            {
                _result = item;
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
                CopyBigList.Add(_result);
            }
        }
    }
}
