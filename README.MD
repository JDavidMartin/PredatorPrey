An attempt at a predator prey model.
Where predators move randomly eating prey as it goes.
At each timestep the positions of the creatures are plotted using ImageSharp.

To run - dotnet run

To change starting parameters, change variables gridX, gridY to change the size of the grid

percentageLiving is the percentage of cells which are populated at the start of the model

predatorPercentage is the percentage of living cells which are spawned as predators, the remaining are all prey

The resulting gif is saved as result.gif