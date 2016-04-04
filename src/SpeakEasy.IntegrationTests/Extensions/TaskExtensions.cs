using System;
using System.Threading.Tasks;

namespace SpeakEasy.IntegrationTests.Extensions
{
    public static class TaskExtensions
    {
        public static void Await(this Task task)
        {
            try
            {
                task.Wait();
            }
            catch (AggregateException ex) when (ex.InnerExceptions.Count == 1)
            {
                throw ex.InnerException;
            }
        }
    }
}
