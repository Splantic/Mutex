using System;
using System.Threading;

class Program
{

    // Create a new Mutex. The creating thread does not own the mutex.
    private static Mutex mut = new Mutex();
    private const int numIterations = 2;
    private const int numThreads = 5;

    static void Main()
    {
        // Create the threads that will use the protected resource.
        for(int i = 0; i < numThreads; i++)
        {
            Thread newThread = new Thread(new ThreadStart(ThreadProc));
            newThread.Name = String.Format("Dretva {0}", i + 1);
            newThread.Start();
        }

        // The main thread exits, but the application continues to
        // run until all foreground threads have exited.
    }

    private static void ThreadProc()
    {
        for(int i = 0; i < numIterations; i++)
        {
            UseResource();
        }
    }

    // This method represents a resource that must be synchronized
    // so that only one thread at a time can enter.
    private static void UseResource()
    {
        // Wait until it is safe to enter.
        Console.WriteLine("{0} zahtjeva Mutex.", Thread.CurrentThread.Name);
        mut.WaitOne();

        Console.WriteLine("{0} je usla u kriticni dio.", Thread.CurrentThread.Name);

        // Place code to access non-reentrant resources here.

        Thread.Sleep(1500);

        Console.WriteLine("{0} napusta kriticni dio odsjecka.", Thread.CurrentThread.Name);

        // Release the Mutex.
        mut.ReleaseMutex();
        Console.WriteLine("{0} je oslobodila Mutex.", Thread.CurrentThread.Name);

        Console.ReadKey();
        Console.WriteLine("---------------------");
    }
}