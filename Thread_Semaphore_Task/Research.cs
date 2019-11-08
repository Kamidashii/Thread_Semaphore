using System;
using System.Diagnostics;

namespace Thread_Semaphore_Task
{
    public class Research
    {
        private int capacity { get; set; }
        private Stopwatch stopwatch;

        public Research(int capacity)
        {
            this.capacity = capacity;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void PushProgress(int progress)
        {
            capacity -= progress;
        }

        public bool IsResearchResolved()
        {
            bool isResolved = capacity <= 0;
            if (isResolved)
            {
                stopwatch.Stop();
            }

            return isResolved;
        }

        public TimeSpan GetResearchResolvingTime()
        {
            return stopwatch.Elapsed;
        }
    }
}
