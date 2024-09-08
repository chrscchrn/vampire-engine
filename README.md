# Vampire Engine

## A 2D Game Engine - BYO-Physics

### Dev Env:
 - figure out the auto import module feature in omni and lsp

### TODO:

 1. Entity.ColliderTracker
    && Entity.AddComponent(new Collider(size, position))
    && Collider.Position
    && Entity.CollideCheck<Solid>(at)

 2. TileMap.AddColliders()


### Program.cs
```
using {{ gameNamespace }};
using vampire;

var Engine = new Engine();
var Game = new Gamge();
Engine.Scene = Game;
Engine.Run();
```
