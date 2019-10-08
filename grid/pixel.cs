using Species;
using System;
namespace pixelSpace
{
    class Pixel
    {
        private int xPos { get; set; }
        private int yPos { get; set; }
        private ISpecies Creature { get; set; } // Can either have a creature present, or null
        public bool isAlive { get; set; } = false;
        public bool hasMovedThisRound { get; set; } = false; // Each square should only be able to move once, or can run into a case where creatures can chain moves

        public Pixel(int x, int y, ISpecies SpawnedCreature) // Construct pixel with x,y and a creature
        {
            xPos = x;
            yPos = y;
            if (SpawnedCreature != null)
            {
                Creature = SpawnedCreature;
                isAlive = true;
            }
        }
        public int returnX()
        {
            return xPos;
        }
        public int returnY()
        {
            return yPos;
        }

        public ISpecies returnCreature()
        {
            if (Creature is null)
            {
                return null;
            }

            return Creature;
        }
        public void addNewCreature(ISpecies newCreature) // overwrite current Creature
        {
            Creature = newCreature;
            isAlive = true;
            hasMovedThisRound = true;
        }
        public void removeOldCreature() // remove old creature
        {
            Creature = null;
            isAlive = false;

        }

    }

}