using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using pixelSpace;
using gridSpace;
using Species;
using System;

namespace DrawingSpace
{
    class Drawing
    {
        public Drawing()
        {
        }

        public Image DrawGrid(grid currentGrid, int[,] trackingArray, int iteration)
        {
            int maxRows = currentGrid.returnNumRows();
            int maxCols = currentGrid.returnNumCols();
            var image = new Image<Rgba32>(maxRows, maxCols);
            int numPredators=0;
            int numPrey=0;

            for (int y = 0; y < maxCols; y++)
            {
                for (int x = 0; x < maxCols; x++)
                {
                    Pixel PixelToDraw = currentGrid.returnCell(x, y);

                    if (PixelToDraw.isAlive)
                    {

                        image[x,y] = PixelToDraw.returnCreature().color;
                        if (PixelToDraw.returnCreature().GetType() == typeof(Species.Predator))
                        {
                            numPredators++;
                        }
                        else
                        {
                            numPrey++;
                        }
                    }
                    else
                    {
                        image[x, y] = Rgba32.Black;
                    }
                }
            }

            trackingArray[iteration,0] = numPredators;
            trackingArray[iteration,1] = numPrey;
            Console.WriteLine($"Number of Predators: {numPredators}");
            return image;
        }
    }
}
