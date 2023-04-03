using Godot;
using System;

public enum Objects { NOTHING = 0, SPIKE = 2, LADDER = 1, WATER = 3 }

public partial class TileCommon : TileMap
{
    Objects obj;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }


    public Objects Collided(Player player)
    {
        obj = new Objects();

        var pos = LocalToMap(player.GlobalPosition);
        var data = GetCellTileData(1, pos, true);
        if (data != null)
        {
            var num = data.GetCustomData("ObjectType");
            GD.Print("Detecting");

            if (num.AsInt32() > 0)
            {
                obj = (Objects)(num.AsInt32());
                GD.Print("Data: ", obj);
            }
        }
        return obj;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
