using NUnit.Framework;
using System;
using System.Threading.Tasks;


namespace iTim.AdvancedTopic
{
    [TestFixture]
    public class AsyncAwaitTester
    {
        public Task TaskA()
        {
            return new Task(() =>
            {
                Task.Delay(3);
                Console.WriteLine("Task A");
            });
        }

        public Task TaskB()
        {
            return new Task(() =>
            {
                Task.Delay(1);
                Console.WriteLine("Task B");
            });
        }
    }

    public class AdvancedTopics
    {
        [Test]
        public void Async_Await()
        {
            AsyncAwaitTester asyncAwaitTester = new AsyncAwaitTester();
            asyncAwaitTester.TaskA();
            asyncAwaitTester.TaskB();
        }

        [Test]
        public void StackVSHeap()
        {

        }
    }
}
