using Thread_Semaphore_Task.Constants;

namespace Thread_Semaphore_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Laboratory laboratory = new Laboratory("Cybenotix", ProjectConstants.UsingThreadsCount, ProjectConstants.GeneralThreadsCount);
            laboratory.InitScientists();
            laboratory.StartResearching();
        }
    }
}
