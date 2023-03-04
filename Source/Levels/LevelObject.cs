using Godot;
using System;
using Godot.Collections;

enum ObjectCategories
{
    PLATFORM, ENEMIES, CHECKPOINTS, FLOOR, BACKGROUND_ITEMS
}

public partial class LevelObject : TileSet
{
    ObjectCategories category;

    Vector2 position;

    Sprite2D icon;

    public void Create()
    {

    }

}
