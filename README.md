# Genetic Algorithm

Created a "Tanks collect the mines" game. 
The game is played with 1 human player, and 15 AI players.  All players start at the same location and orientation on every "round".
Each player must control the tank using independent 'left tread' and 'right tread' controls. For the human player, Key Q: Left tread forward, key A: Left tread reverse, key W: Right tread forward, key S: Right tread reverse.  So to move forward, the player must press both Q and W simultaneously.  Pressing only Q should send the tank into a clockwise circle.  Pressing both W and A should cause the tank to spin counter-clockwise in place without moving. 

 After app initialization, the position of the mines remains constant with every round.  When a tank touches a mine, it is "collected" but NOT removed from the map so other tanks can also collect it. If the tank goes off the left side of the screen, it reappears on the right side.
 
Each round lasts 30 seconds. Each player tries to collect as many mines as possible within the time limit.

#Implemented a genetic algorithm to improve the tank AI.

The AI cannot "see" or otherwise detect the mines.
The AI only has a sequence of "left/right tread at x power" commands, which it blindly follows.
When the game initially starts, randomize the sequence of commands for each AI.
After each round, run the genetic algorithm on the population,
Implemented a fitness function that evaluates the distance to the nearest mine, every frame. With big bonuses for actually collecting a mine.
