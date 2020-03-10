using System;
using System.Threading;
using System.Threading.Tasks;

namespace iTim.AdvancedTopic
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteManuallyCancellableTaskAsync();

            do
            {
            } while (true);
        }
         
        private static Task<decimal> LongRunningCancellableOperation(int loop, CancellationToken cancellationToken)
        {
            Task<decimal> task = null;

            // Start a task and return it
            task = Task.Run(() =>
            {
                decimal result = 0;

                // Loop for a defined number of iterations
                for (int i = 0; i < loop; i++)
                {
                    // Check if a cancellation is requested, if yes,
                    // throw a TaskCanceledException.

                    Console.WriteLine(i);

                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);

                    // Do something that takes times like a Thread.Sleep in .NET Core 2.
                    Thread.Sleep(100);
                    result += i;
                }

                return result;
            });

            return task;
        }

        public static async Task ExecuteManuallyCancellableTaskAsync()
        {
            Console.WriteLine(nameof(ExecuteManuallyCancellableTaskAsync));

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                // Creating a task to listen to keyboard key press
                var keyBoardTask = Task.Run(() =>
                {
                    Console.WriteLine("Press enter to cancel");
                    Console.ReadKey();

                    // Cancel the task
                    cancellationTokenSource.Cancel();
                });

                try
                {
                    var longRunningTask = LongRunningCancellableOperation(500, cancellationTokenSource.Token);

                    var result = await longRunningTask;
                    Console.WriteLine("Result {0}", result);
                    Console.WriteLine("Press enter to continue");
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("Task was cancelled");
                }

                await keyBoardTask;
            }
        }
    }
}
