using Godot;
using System;

public enum Objects { NOTHING = 0, SPIKE = 2, LADDER = 1, WATER = 3 }

public partial class TileCommon : TileMap
{
    TileData data;
    Objects obj;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }


    public Objects Collided(Player player)
    {
        obj = new Objects();

        var pos = LocalToMap(player.Position);
        data = GetCellTileData(0, pos);

        if (data != null)
        {
            obj = (Objects)((int)data.GetCustomData("ObjectType"));
            GD.Print("Data: ", obj);
        }
        return obj;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
