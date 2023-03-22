using Godot;
using System;

public enum Objects { NOTHING = 0, SPIKE = 1, LADDER = 2, WATER = 3 }

public partial class TileCommon : TileMap
{
    TileData data;

    Player player;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }


    public Objects Collided()
    {
        Objects obj = Objects.NOTHING;
        //data = GetCellTileData(1, player.Position);
        obj = (Objects)((int)data.GetCustomDataByLayerId(1));

        return obj;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
