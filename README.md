# **Dimwarper**
###### _Game made for KrakJam 2022_
------------------------

### Description
Diwmwarper is a platformer game in which you lay as a mysterious character who gained an ability to swap between two dimensions. You traveled to a distant land controlled by 7 different mages, each one more dangerous than the previous. Your quest is to conquer all seven towers and defeat the arch mage.

With each tower, the player will face a new type of danger
- Smaller platforms
- Moving platforms
- Fireballs
- Ice lasers
- Fire and ice monsters
- Changing speed of the game
 ------------------------
### Credits
##### Tomasz Banaś
- Programming
- UI
##### Jakub Wilczak
- Game design
- Graphics and game assets
- Testing
##### Mariusz Gajewski
- Graphics and game assets
- Testing
 ------------------------
### Gameplay

Each level follows the same scheme. Randomly generated platforms appear above the character, so the player has to jump up to keep up with a speed of the camera moving up.

##### Dimensions
- Player can **swap dimensions**.
- Player is always in one of two dimensions — _fire dimension_ or _ice dimension_.
- Player character can collide only with platforms in the same dimension.
- All dangers affect the player only if they exist in the same dimension as player is in.

##### Energy
- Player can encounter **energy crystals**, picking up one gives 1 point of energy. 
- Player can hold a maximum of 4 points of energy. 
- Player can use a point of energy to perform **double jump** to save themself, or pass through danger.

##### Dangers
- Player needs to keep up with a moving camera. Platforms which are too far behind disappear.
- Moving platforms.
- Fireballs approaching from above, kill player on contact.
- Ice lasers firing from sides, kill player on contact.
- Fire and ice monsters chasing after the player kill player on contact.
