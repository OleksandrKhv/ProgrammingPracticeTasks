using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Other.Level6.Fibonacci
{
    public class FirstNFibonacci
    {
        public long[] App(int N)
        {
            List<long> result = new List<long>(N) {1,1};

            for (N-=2; N > 0; N--,result.Add(result[result.Count-2]+result.Last()));

            return result.ToArray().Reverse().ToArray();
        }


        private long[] App1()
        {
            return null;
        }
    }
}
