using System;
using System.Threading;

namespace Thread_Semaphore_Task
{
    public class Scientist
    {
        private readonly string _name; 
        private readonly Random _random;
        private readonly int _minWorkTime = 1000;
        private readonly int _maxWorkTime = 5000;
        private readonly int _workPartTime = 50;

        private readonly int _minResearchCapacity = 5;
        private readonly int _maxResearchCapacity = 10;

        public bool workFinished = false;


        public Scientist(string scientistName)
        {
            _random = new Random();
            _name = scientistName;
        }
        public int StartWork()
        {
            Console.WriteLine($"{_name} is starting work..");
            int allWorkTime = _random.Next(_minWorkTime, _maxWorkTime);
            int parts = allWorkTime / _workPartTime;
            for(int i = 0; i < parts; ++i)
            {
                if (workFinished)
                {
                    Console.WriteLine($"{_name} stopped work");
                    Console.WriteLine();
                    return 0;
                }
                Thread.Sleep(_workPartTime);
            }
            int resolvedResearchCapacity = _random.Next(_minResearchCapacity, _maxResearchCapacity);
            Console.WriteLine($"{_name} finished work");
            Console.WriteLine();

            return resolvedResearchCapacity;
        }
    }
}
