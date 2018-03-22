# GameEngineArchitecture1
This project focused on attempting to create a level editor using the Unity game engine.
The way in which we could develop this was however we pleased, ideally in 3D if possible.
With my attempt at this project, I opted to go for a level editor that didn't require the player
to hop leave the game, come back in editor and make their level, then go back in game and play it, I wanted the player to be
able to play it immediately.

My version of a level editor uses a simple toggle key that switches the player from the game to the level editor, with a simple
hotbar of different blocks they can place around an initial starting block. The level editor itself is part of the game in that
the player must attempt to get to the end of the level using the editor, though they have a limited amount of blocks to do it in.
The player is aided through the use of different kinds of block: Dirt, Ice and Bouncy. I did not implemement these blocks to 
completion by the time that the project deadline came around but I did make use of Unity's physics materials to get an idea of how
they could affect the gameplay.

What I have taken from this project is that if I plan on having unique block behaviours I should either further research game-engine
physics or write my own behaviours in scripts to have more impact on the player experience.

Back to the editor itself, the player is also able to save the level so that they can come back to it later. This is done
using a binary formatter which encrypts the data you are trying to save: in this case where the player is, the blocks they have
placed and how many they have left. The binary formatter can then be used to re-load that data from wherever the data file is saved.

This is preferable to using something like Unity's player-prefs because as useful as player-prefs are, they are quite insecure and
using binary formatters means that the data is a lot harder to tamper with to prevent cheating.
