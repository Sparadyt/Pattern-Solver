using System;
using System.Collections.Generic;

public class ToGuess
{
    public List<(int x, int y)> toGuess = new List<(int x, int y)>();

    public void GenerateToGuess()
    {
        List<(int x, int y)> validMoves = new List<(int x, int y)>();
        toGuess.Add((Program.rand.Next(1, 4), Program.rand.Next(1, 4)));

        for(int i = 1; i <= Program.rand.Next(4, 10); i++)
        {
            toGuess.Add((0, 0));
            validMoves = Program.GetPossibleMoves(toGuess[i - 1], toGuess[0..(toGuess.Count - 1)]);

            if(validMoves.Count == 0)
            {
                break;
            }

            toGuess[i] = Program.Move(validMoves);
        }
    }
}