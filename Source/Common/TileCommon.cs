using Godot;
using System;

public enum Objects { NOTHING = 0, SPIKE = 1, LADDER = 2, WATER = 3 }

public partial class TileCommon : TileMap
{
    TileData data;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }


    public Objects Collided(Player player)
    {
        Objects obj = Objects.NOTHING;

        var pos = LocalToMap(player.Position);
        data = GetCellTileData(1, pos);

        if (data.HasMeta("Objects"))
        {
            obj = (Objects)((int)data.GetMeta("Objects"));
        }

        return obj;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
