using Godot;
using System;
using System.Collections.Generic;

public enum Objects { NOTHING = 0, SPIKE = 2, LADDER = 1, WATER = 3 }

public partial class TileCommon : TileMap
{
    int obj = 0;
    TileSetScenesCollectionSource scene;

    public void ClearCollidedObject() => obj = 0;

    public int Collided(Player Player, string DataLayerName)
    {
        var pos = LocalToMap(Player.GlobalPosition);
        var data = GetCellTileData(1, pos, true);
        if (data != null)
        {
            var num = data.GetCustomData(DataLayerName);

            obj = num.AsInt32();
            return obj;
        }
        return 0;
    }

    public List<PackedScene> GetScenes()
    {
        List<PackedScene> scenes = new List<PackedScene>();

        scene = (TileSetScenesCollectionSource)TileSet.GetSource(TileSet.GetSourceId(0));

        for (int i = 0; i < scene.GetSceneTilesCount(); i++)
        {
            scenes.Add(scene.GetSceneTileScene(i));
        }
        return scenes;
    }
}
