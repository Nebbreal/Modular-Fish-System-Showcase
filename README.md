# Meer Momo

## Description
Meer Momo is a fishing game made to play on a [Pillo](https://pillo.nl/), with disabled people in mind. In Meer Momo the player can catch random fish using the Pillo. The game should make the player feel relaxed while fishing in the comfort of their own home. 

## Installation

### Requirements
- Unity editor version `6000.1.4f1`

### Installation steps
1. **Clone the repository**

Clone the repository by clicking the code button at the top of the page or clone the repository with the following command:

```
git clone https://projects.fhict.nl:gdt/spring-2025/game-design/pillo-hulan-1.git
cd pillo-hulan-1
```

2. **Open with Unity Hub**

- Open Unity Hub
- Click on Add Project
- Navigate to the cloned repository folder and select it
- Ensure you're using the correct Unity version (see above)

3. **Load the scene you want to see**

- Go to `Assets/Scenes/YourScene.unity`
- Double click to load it
- Press play to start the game

## Structure
The folder structure is generally "task/object". Task being what type of file something is, like a script or prefab. Object being what it represents, like a fishing rod. 
Related products might be in subfolders instead of its own folder.

## Build process
The project gets merged and built inside the scene found in `Assets/Scenes/-Main.unity`, this happens in the `release` branch.