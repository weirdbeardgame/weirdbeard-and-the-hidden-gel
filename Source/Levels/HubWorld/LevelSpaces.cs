using Godot;
using System;
using Godot.Collections;
public partial class LevelSpaces : Node2D
{
    public HubActor actor;

    TileSet set;
    TileData data;
    public override void _Ready()
    {
        base._Ready();
        set = Owner.GetNode<TileSet>("TileSet");
        data = GetNode<TileData>("TileData");
    }

    public void EnterLevel()
    {
        actor.Deactivate();
        LevelCommon level = (LevelCommon)data.GetCustomData("Level");
        SceneManager.changeScene(level.levelName, actor.Player);
    }

}
