using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Util
{
    public class RunnedTime
    {
        public int IterationsToRun = 1000;
        public delegate void Func();
        public long CountAverageTicks(Func func)
        {
            Stopwatch watch = new Stopwatch();
            long time = 0;
            for (int i = 0; i < IterationsToRun; i++)
            {
                watch.Reset();
                watch.Start();
                func();
                watch.Stop();
                time+=watch.ElapsedTicks;
            }
            return time/IterationsToRun;
        }
    }
}
