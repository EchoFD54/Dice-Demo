# 3D Dice Roller

This is a physics-based 3D dice roller made with Unity 6 developed to demostrate basic unity abilities and practices.

## Technical details

* Unity  engine version: 6000.3.24f1 LTS
* Using Universal Render Pipeline
* Playable Scene: Assets/Scenes/DiceDemo_Main.unity 
* Playable executable: On the [Releases tab](https://github.com/EchoFD54/Dice-Demo/releases/tag/v1.0)


## Controls
[SPACE] - Roll the dice

[R] - Reset dice to starting position

[ESC] - Quit game


## Implementation

### Physics and rolling
The roll is performed by the DiceController.cs script using ForceMode.Impulse on the rigibody of the dice and it applies a random force and torque to it.

 When the dice lands, it checks if the dice has stopped moving, and then waits for half a second before actually attempting to read the face in case the dice was just moving slowly.

### Face detection 
 This project uses face normals to determine the face in which the dice has landed. The DiceReader.cs script uses Vector3.Dot to compare the world's "up" direction against the faces of the dice. The face that points closest to the sky is the winner face.
 
 The dot product can also check if the dice lands in a tilted way or if it perfectly lands on an edge of the dice.

 ### UI
 The scripts communicate using C# Action events to notify the DiceUI.cs script in order to display the winning face of a dice.
 
This UI is a world space canvas that listens for the roll result and uses a billboard effect to ensure its always looking at the camera.

### Camera 
The camera is a stationary Cinemachine camera that uses a rotation composer, it pans and tracks the dice across the table slowly and wihtout snapping.

## Credits & Third-Party Assets
* 3D dice model: used the [Collection Dice set for role-playing games](https://assetstore.unity.com/packages/3d/environments/collection-dice-set-for-role-playing-games-202821) made by "Armor and Rum" from the Unity Asset Store











