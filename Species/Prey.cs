using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Species
{
    class Prey : ISpecies
    {
        public int health { get; set; } = 50;
        public bool avoidPrey { get; set; } = true;
        public Rgba32 color { get; set; } = Rgba32.Green;
        public int SpawningHealth { get; set; } = 150;

        public Prey()
        {

        }

        public void EndOfTurnHealthChange()
        {
            health = health + 10;
        }

        public void eatCreature(int addedHealth)
        {
            throw new SystemException("Should not be able to eat");
        }

    }

}