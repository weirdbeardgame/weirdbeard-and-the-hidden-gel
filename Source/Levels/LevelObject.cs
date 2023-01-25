using Godot;
using System;
using System.Collections.Generic;

enum ObjectCategories
{
    PLATFORM, ENEMIES, CHECKPOINTS, FLOOR, BACKGROUND_ITEMS
}

public class LevelObject : TileSet
{
    ObjectCategories category;

    Vector2 position;

    Sprite icon;

    public void Create()
    {
        
    }

}
