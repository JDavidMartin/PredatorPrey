using SixLabors.ImageSharp.PixelFormats;

namespace Species
{
    public interface ISpecies
    {
        int health { get; set; } // Current health of the creature
        bool avoidPrey { get; set; } // Only predators are able to land on prey. Both species avoid predators by default
        int SpawningHealth { get; set; } // Threshold for when a new creature will be spawned
        Rgba32 color { get; set; } // Colour to draw

        void EndOfTurnHealthChange(); // The change in health at the end of each turn, prey will go up, predators will go down
        void eatCreature(int addedhealth); // Adding health in the case of eating another species, only predators should ever be able to use this

    }

}