using Species;
using System;
using pixelSpace;
using System.Collections.Generic;

namespace gridSpace
{
    class grid
    {
        int[,] moveArray = new int[,] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 0, 0 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
        private int maxX { get; set; }
        private int maxY { get; set; }
        private Pixel[,] Cells { get; set; }

        public grid(int x, int y)
        {
            maxX = x;
            maxY = y;
            Cells = new Pixel[x, y];
        }

        public void fillGrid(int livingPercentage, int predPrayRatio)
        {
            Random AliveDeterminer = new Random();
            Random speciesDeterminer = new Random();
            int emptyTiles = 0;
            int numPreds = 0;
            int numPrey = 0;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (AliveDeterminer.Next(0, 101) < livingPercentage)
                    {
                        if (speciesDeterminer.Next(0, 101) < predPrayRatio)
                        {
                            Cells[x, y] = new Pixel(x, y, new Predator());
                            numPreds++;
                        }
                        else
                        {
                            Cells[x, y] = new Pixel(x, y, new Prey());
                            numPrey++;
                        }
                    }
                    else
                    {
                        Cells[x, y] = new Pixel(x, y, null);
                        emptyTiles++;

                    }
                }

            }
        }

        public int returnNumRows()
        {
            return maxX;
        }
        public int returnNumCols()
        {
            return maxY;
        }

        public Pixel returnCell(int x, int y)
        {
            if (x < 0 || x >= maxY || y < 0 || y >= maxX)
            {
                return null;
            }
            return Cells[x, y];
        }

        public void MoveCreatures()
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    // Console.WriteLine($"({x},{y})");
                    Pixel pixelToMove = returnCell(x, y);
                    if (pixelToMove.isAlive && !pixelToMove.hasMovedThisRound)
                    {
                        // Console.WriteLine("Alive, move it");
                        ISpecies creatureToMove = pixelToMove.returnCreature();
                        //Get New Move
                        int[,] newMove = getNewMove(pixelToMove, false);

                        Pixel NewPixel = returnCell(newMove[0, 0], newMove[0, 1]);
                        if (NewPixel == pixelToMove)
                        {
                            // Console.WriteLine("Same Pixel do nothing");
                            continue;
                        }
                        if (NewPixel.isAlive)
                        {
                            if (!creatureToMove.avoidPrey)
                            {
                                handleEating(creatureToMove, NewPixel.returnCreature());
                            }
                        }
                        // Console.WriteLine("Moving");
                        NewPixel.addNewCreature(creatureToMove);
                        pixelToMove.removeOldCreature();

                    }
                    else
                    {
                        // Console.WriteLine("Dead move on");
                    }

                }

            }

        }

        public int[,] getNewMove(Pixel oldPixel, bool baby) // get new move for creature, and specify if a baby is spawning
        {

            int[,] allMoves = getAllPossibleMoves(oldPixel.returnX(), oldPixel.returnY()); //get all possible moves

            List<int> availableMoves = new List<int>();

            for (int i = 0; i < allMoves.Length / 2; i++)
            {
                Pixel nextCell = returnCell(allMoves[i, 0], allMoves[i, 1]); // Get next cell
                if (nextCell != null) // if cell exists
                {
                    if (canMoveToCell(oldPixel, nextCell, baby)) // check if can move to new cell
                    {
                        availableMoves.Add(i); // add this index to list of available moves
                    }
                }
            }

            if (availableMoves.Count != 0) // 
            {
                Random moveChooser = new Random();
                int randomIndex = moveChooser.Next(0, availableMoves.Count);
                int randomMoveIndex = availableMoves[randomIndex];
                int[,] newMove = new int[,] { { allMoves[randomMoveIndex, 0], allMoves[randomMoveIndex, 1] } };
                return newMove;
            }

            return new int[,] { { oldPixel.returnX(), oldPixel.returnY() } };

        }
        public int[,] getAllPossibleMoves(int startingX, int startingY)
        {
            int[,] moveList = new int[9, 2];

            for (int i = 0; i < moveArray.Length / 2; i++)
            {
                moveList[i, 0] = startingX + moveArray[i, 0];
                moveList[i, 1] = startingY + moveArray[i, 1];

            }

            return moveList;
        }

        public bool canMoveToCell(Pixel oldCell, Pixel newCell, bool baby)
        {

            if (baby) // If baby spawning
            {
                if (newCell.isAlive) // only spawn in free space
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            if (newCell.isAlive) // Living cell
            {
                if (newCell == oldCell)
                {
                    return true;
                }

                if (oldCell.returnCreature().avoidPrey) // Am a prey, cant move to other prey or predators
                {
                    return false;

                }
                else // Am predator, eat the prey
                {
                    if (newCell.returnCreature().GetType() == typeof(Species.Predator)) // can't move to other predators
                    {
                        return false;

                    }
                    return true; // it's a prey eat it
                }
            }

            return true; // empty cell, fine to move to

        }

        public void handleEating(ISpecies predator, ISpecies prey)
        {
            predator.eatCreature(prey.health + 50);
        }

        public void TestSetup()
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    Cells[x, y] = new Pixel(x, y, null);

                }

            }
            Cells[3, 3] = new Pixel(3, 3, new Predator());
            Cells[3, 2] = new Pixel(3, 2, new Predator());

        }

        public void HandleEndOfRound()
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    Pixel CurrentCell = returnCell(x, y);
                    CurrentCell.hasMovedThisRound = false;
                    if (CurrentCell.isAlive)
                    {
                        ISpecies currentCreature = CurrentCell.returnCreature();
                        currentCreature.EndOfTurnHealthChange();
                        if (currentCreature.health < 0)
                        {
                            CurrentCell.removeOldCreature();
                            continue;
                        }
                        if (currentCreature.health > currentCreature.SpawningHealth)
                        {
                            currentCreature.health -= currentCreature.SpawningHealth / 2;
                            //Spawn new creature
                            int[,] spawningPosition = getNewMove(CurrentCell, true);
                            Pixel spawningCell = returnCell(spawningPosition[0, 0], spawningPosition[0, 1]);
                            if (spawningCell != CurrentCell) // If no spawning space available, by default returns same cell. Quick check to prevent this
                            {
                                CurrentCell.hasMovedThisRound = true;

                                if (currentCreature.GetType() == typeof(Species.Predator))
                                {
                                    spawningCell.addNewCreature(new Predator());
                                }
                                else
                                {
                                    spawningCell.addNewCreature(new Prey());
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}