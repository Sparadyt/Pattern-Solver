using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class Program
{
    static (int x, int y) pos;
    public static Random rand = new Random();

    static void Main()
    {
        Console.Clear();
        Console.WriteLine("Enter the amount of iterations you would like:");
        string? iterationStr = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(iterationStr))
        {
            PrintError("Please enter something");
            return;
        }

        if (!int.TryParse(iterationStr, out int iteration))
        {
            PrintError("Please enter a valid integer");
            return;
        }

        if(iteration < 0)
        {
            PrintError("Please enter a number more that -1");
            return;
        }

        ulong totalTimeMs = 0;
        ulong totalAttempts = 0;
        Console.Clear();

        //Iteration
        for (int i = 0; i < iteration; i++)
        {
            ulong attempts = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ToGuess toGuess = new ToGuess();
            toGuess.GenerateToGuess();

            //Starting Move
            while (true)
            {
                List<(int x, int y)> moves = new List<(int x, int y)>();
                bool repeat = true;

                //Choosing random starting position
                pos = (rand.Next(1, 4), rand.Next(1, 4));

                //Move
                while (true)
                {
                    int same = 0;
                    attempts++;

                    List<(int x, int y)> validMoves = GetPossibleMoves(pos, moves);

                    //If there are no valid moves left, break
                    if (validMoves.Count == 0)
                    {
                        pos = (rand.Next(1, 4), rand.Next(1, 4));
                        break;
                    }

                    pos = Move(validMoves);
                    moves.Add(pos);

                    //Checking if elements match
                    for(int j = 0; j < toGuess.toGuess.Count; j++)
                    {
                        if(j == moves.Count || j == toGuess.toGuess.Count)
                        {
                            break;
                        }

                        if(moves[j] == toGuess.toGuess[j])
                        {
                            same++;
                        }
                    }

                    if(same == toGuess.toGuess.Count)
                    {
                        repeat = false;
                        break;
                    }
                }

                if(!repeat)
                {
                    break;
                }
            }

            stopwatch.Stop();
            totalTimeMs += Convert.ToUInt64(stopwatch.ElapsedMilliseconds);
            totalAttempts += attempts;

            Console.WriteLine($"Iteration {i + 1}:");
            Console.WriteLine($"  Attempts = {attempts}");
            Console.Write($"  To guess = ");
            foreach(var toGuess2 in toGuess.toGuess)
                Console.Write($"{toGuess2}, ");
            Console.WriteLine("\b\b");

            Console.WriteLine($"  Total attempts = {totalAttempts}");
            Console.WriteLine($"  Time in milliseconds = {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"  Total time in ms = {totalTimeMs}ms");
            Console.WriteLine($"  Time in seconds = {stopwatch.Elapsed.TotalSeconds}sec");
            Console.WriteLine($"  Total time in seconds = {totalTimeMs / 1000.0}sec");
            Console.WriteLine();
        }
    }

    public static List<(int x, int y)> GetPossibleMoves((int x, int y) pos, List<(int x, int y)> takenPos_)
    {
        //List of all the moves
        List<(int x, int y)> validMoves = new List<(int x, int y)>
        {
            (1, 1), (2, 1), (3, 1),
            (1, 2), (2, 2), (3, 2),
            (1, 3), (2, 3), (3, 3)
        };

        //Removes taken moves
        validMoves = validMoves.Where(move => !takenPos_.Contains(move)).ToList();
        validMoves.Remove(pos);

        for(int i = validMoves.Count - 1; i >= 0; i--)
        {
            if(((pos.x + 2) == validMoves[i].x && (pos.y - 1 != validMoves[i].y || (pos.y + 1 != validMoves[i].y)) ||
            (pos.y + 2) == validMoves[i].y && (pos.x - 1 != validMoves[i].x || (pos.x + 1 != validMoves[i].x)) ||
            (pos.x - 2) == validMoves[i].x && (pos.y - 1 != validMoves[i].y || (pos.y + 1 != validMoves[i].y)) ||
            (pos.y - 2) == validMoves[i].y && (pos.x - 1 != validMoves[i].x || (pos.x + 1 != validMoves[i].x))))
                validMoves.Remove(validMoves[i]);
        }

        return validMoves;
    }

    public static (int x, int y) Move(List<(int x, int y)> validMoves)
    {
        return validMoves[rand.Next(validMoves.Count)];
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {message}");
        Console.ResetColor();
        Console.WriteLine("(Enter any key to continue)");
        Console.ReadKey();
    }
}