# Modular Fish system

## Description
The Modular Fish System is a tool made for Meer Momo, a fishing game made to play on a [Pillo](https://pillo.nl/).
The system is intended to make it easy for developers of Meer Momo to easily add a large variety of fish without having to draw a large amount of sprites.

The system works by taking a base of a fish alongside multiple pools of body parts to generate a unique fish. 
These bases do require some manual setup due to them needing designated slots for body parts to go.

The system also includes various configuration options such as the chance for a hat or the size of the generated fish.

## Installation

### Requirements
- Unity editor version `6000.3.9f1`

### Installation steps

Head to https://nebbdev.itch.io/modular-fish-system-showcase

**or**

1. **Clone the repository**

Clone the repository by clicking the code button at the top of the page or clone the repository with the following command:

```
git clone https://github.com/Nebbreal/Modular-Fish-System-Showcase
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