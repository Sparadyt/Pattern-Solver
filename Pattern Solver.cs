using System;
using System.Diagnostics;

class Program
{
    static Random rand = new Random();
    static void Main()
    {
        int toSolve = 589632147;
        Solve(toSolve);
    }

    static void Solve(long toSolve)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int guess = 0;
        long attempt = 0;

        while(true)
        {
            guess = rand.Next(999999999);

            if(guess == toSolve)
            {
                stopwatch.Stop();
                double elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000.0;
                Console.WriteLine($"Guessed {toSolve} in {stopwatch.ElapsedMilliseconds}ms");
                System.Console.WriteLine($"{elapsedSeconds}secs");
                System.Console.WriteLine($"{attempt} attempts");
                return;
            }

            attempt++;
        }
    }
}