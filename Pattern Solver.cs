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
        Console.WriteLine("Enter the amount of itirations you would like:");
        string? itirationStr = Console.ReadLine();

        if(String.IsNullOrWhiteSpace(itirationStr))
        {
            Console.WriteLine("Please enter something");
            return;
        }


        if(!int.TryParse(itirationStr, out int itiration))
        {
            Console.WriteLine("Please enter a valid integer");
            return;
        }

        int[] toGuesses = new int[itiration];

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < itiration; i++)
        {
            toGuesses[i] = rand.Next(999999999);
            totalAttempt += Solve(toGuesses[i]);
        }

        stopwatch.Stop();
        for (int i = 0; i < itiration; i++)
        {
            Console.WriteLine($"Itiration {i}:");
            Console.WriteLine($"To Guess = {toGuesses[i]}");
            Console.WriteLine($"Attempts = {stats[i].attempts}");
            Console.WriteLine($"Time in miliseconds = {stats[i].timeMs}ms");
            Console.WriteLine($"Time in second =  {stats[i].timeSec}sec");
            Console.WriteLine();
        }

        Console.WriteLine($"Total Attempts = {totalAttempt}");
        Console.WriteLine($"Total Milisecond Time = {stopwatch.ElapsedMilliseconds}");
        Console.WriteLine($"Total Second Time = {stopwatch.ElapsedMilliseconds / 1000.0}");

        double averageAttempt = totalAttempt / (double)itiration;
        double averageTimeMs = stopwatch.ElapsedMilliseconds / (double)itiration;
        Console.WriteLine();
        Console.WriteLine($"Average Attempts = {averageAttempt}");
        Console.WriteLine($"Average Milisecond Time = {averageTimeMs}");
        Console.WriteLine($"Average Second Time = {averageTimeMs / 1000.0}");
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