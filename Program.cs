﻿using System;
using gridSpace;
using DrawingSpace;
using SixLabors.ImageSharp;

namespace predPrey
{
    class Program
    {
        static void Main(string[] args)
        {
            grid Field = new grid(250, 250);
            
            Field.fillGrid(10, 10); //  I.e. 15% are living, and 50% of these are Predators

            int maxIterations = 1000;
            int iteration = 0;

            int[,] trackingArray = new int[maxIterations, 2];

            Drawing drawing = new Drawing();
            Image gif = drawing.DrawGrid(Field, trackingArray, iteration);
            gif.Save("Start.gif");

            while (iteration < maxIterations)
            {
                Field.MoveCreatures();
                Field.HandleEndOfRound();
                Image newImage = drawing.DrawGrid(Field, trackingArray, iteration);
                gif.Frames.InsertFrame(iteration, newImage.Frames.RootFrame);
                Console.WriteLine(iteration);
                iteration++;
            }

            for (int i = 0; i < trackingArray.Length/2; i++)
            {
                Console.WriteLine($"Round Number :{i}, Number Of Predators : {trackingArray[i,0]}, Number Of Prey:{trackingArray[i,1]}");
            }

            gif.Save("result.gif");

        }
    }
}
