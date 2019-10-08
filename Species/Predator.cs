using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Species
{
    class Predator : ISpecies
    {
        public int health { get; set; } = 200;
        public Rgba32 color { get; set; } = Rgba32.Red;
        public bool avoidPrey { get; set; } = false;
        public int SpawningHealth { get; set; } = 350;

        public Predator()
        {
        }

        public void EndOfTurnHealthChange()
        {
            health = health - 25;
        }

        public void eatCreature(int addedHealth)
        {
            health += addedHealth;
        }
    }
}
