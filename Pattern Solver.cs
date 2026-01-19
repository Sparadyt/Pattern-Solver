using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static List<(long attempts, long timeMs, double timeSec)> stats = new List<(long attempts, long timeMs, double timeSec)>();
    static Random rand = new Random();
    static void Main()
    {
        long totalAttempt = 0;
        Console.WriteLine("Enter the amount of iterations you would like:");
        string? iterationStr = Console.ReadLine();

        if(String.IsNullOrWhiteSpace(iterationStr))
        {
            Console.WriteLine("Please enter something");
            return;
        }


        if(!int.TryParse(iterationStr, out int iteration))
        {
            Console.WriteLine("Please enter a valid integer");
            return;
        }

        int[] toGuesses = new int[iteration];
        long totalTimeMs = 0;
        Console.Clear();

        for (int i = 0; i < iteration; i++)
        {
            toGuesses[i] = rand.Next(999999999);
            totalAttempt += Solve(toGuesses[i]);
            totalTimeMs += stats[i].timeMs;
            Console.Write($"\rIteration {i + 1}/{iteration} finished");
        }

        Console.WriteLine("\n");

        for (int i = 0; i < iteration; i++)
        {
            Console.WriteLine($"Iteration {i + 1}:");
            Console.WriteLine($"To Guess = {toGuesses[i]}");
            Console.WriteLine($"Attempts = {stats[i].attempts}");
            Console.WriteLine($"Time in milliseconds = {stats[i].timeMs}ms");
            Console.WriteLine($"Time in second =  {stats[i].timeSec}sec");
            Console.WriteLine();
        }

        if(iteration == 1)
            return;

        Console.WriteLine($"Total Attempts = {totalAttempt}");
        Console.WriteLine($"Total Millisecond Time = {totalTimeMs}");
        Console.WriteLine($"Total Second Time = {totalTimeMs / 1000.0}");

        double averageAttempt = totalAttempt / (double)iteration;
        double averageTimeMs = totalTimeMs / (double)iteration;
        Console.WriteLine();
        Console.WriteLine($"Average Attempts = {averageAttempt}");
        Console.WriteLine($"Average Millisecond Time = {averageTimeMs}ms");
        Console.WriteLine($"Average Second Time = {averageTimeMs / 1000.0}sec");
    }

    static long Solve(int toGuess)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int guess = 0;
        long attempt = 0;

        while (true)
        {
            guess = rand.Next(999999999);

            if (guess == toGuess)
            {
                stopwatch.Stop();

                stats.Add((attempt, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedMilliseconds / 1000.0));
                return attempt;
            }

            attempt++;
        }
    }
}