using System;
using System.Collections.Generic;
using System.Threading;

namespace Thread_Semaphore_Task
{
    public class Laboratory
    {
        private readonly string _name;
        private readonly Semaphore _semaphore;
        private readonly List<Scientist> _scientists;
        private Research research;
        private object researchLocker;

        private bool researchAlreadyEnded=false;

        private readonly int _workedScientists;
        private readonly int _allScientists;
        public Laboratory(string laboratoryName, int workedScientists, int allScientists)
        {
            _name = laboratoryName;
            _workedScientists = workedScientists;
            _allScientists = allScientists;

            _scientists = new List<Scientist>(allScientists);
            _semaphore = new Semaphore(_workedScientists, _allScientists);

            research = new Research(100);
            researchLocker = new object();

            Console.WriteLine($"Laboratory: {_name} is created");
        }

        public void InitScientists()
        {
            for (int i = 0; i < _allScientists; ++i)
            {
                _scientists.Add(new Scientist($"Scientist: {i}"));
            }
        }

        public void StartResearching()
        {

            if (_allScientists == 0)
            {
                return;
            }

            for(int i = 0; i < _allScientists; ++i)
            {
                int index = i; //important!
                Thread thread = new Thread(() => StartScientistWork(index));
                thread.Start();
            }
                
        }

        private void StartScientistWork(int scientistId)
        {
            _semaphore.WaitOne();
            int resolved = _scientists[scientistId].StartWork();

            lock (researchLocker)
            {

                if (researchAlreadyEnded)
                {
                    return;
                }

                research.PushProgress(resolved);
                if (research.IsResearchResolved())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Researching ended!");
                    TimeSpan timeSpent = research.GetResearchResolvingTime();
                    Console.WriteLine($"Spent {timeSpent.Days} dd {timeSpent.Hours}:{timeSpent.Minutes}:{timeSpent.Seconds} {timeSpent.Milliseconds}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    researchAlreadyEnded = true;
                    return;
                }
            }
            _semaphore.Release();
            Thread thread = new Thread(() => StartScientistWork(scientistId));
            thread.Start();
        }
    }
}
