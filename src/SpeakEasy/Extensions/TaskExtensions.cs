using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeakEasy.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Synchronously iterates over an enumerable of tasks with exception handling. For more information see:
        /// http://social.msdn.microsoft.com/Forums/en-US/parallelextensions/thread/95355648-1fa6-4b2d-a260-954c3421c453/
        /// </summary>
        /// <typeparam name="TResult">The result of the sequence of tasks</typeparam>
        /// <param name="taskIterator">The sequence of tasks to run</param>
        /// <param name="completionSource">The completion source of the sequence, this is will be the final result</param>
        public static void Iterate<TResult>(this IEnumerable<Task> taskIterator, TaskCompletionSource<TResult> completionSource)
        {
            var tasks = taskIterator.GetEnumerator();

            // need to declare recursive body before assigning it because it calls itself.
            Action<Task> recursiveBody = null;
            recursiveBody = completedTask =>
            {
                if (completedTask != null && completedTask.IsFaulted)
                {
                    completionSource.TrySetException(completedTask.Exception.InnerExceptions);
                    tasks.Dispose();
                }
                else if (tasks.MoveNext())
                {
                    tasks.Current.ContinueWith(recursiveBody, TaskContinuationOptions.ExecuteSynchronously);
                }
                else
                {
                    tasks.Dispose();
                }
            };

            // when we're done make sure the tasks enumerable gets disposed
            recursiveBody(null);
        }
    }
}
