using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

public partial class HubWorld : LevelCommon
{
    [Export] Array<NodePath> containedLevels;
    List<Node2D> levelSpaces;
    PathFollow2D follow2D;
    AudioStreamPlayer backgroundPlayer;
    HubActor actor;

    TileCommon map;

    public override void EnterLevel(Player p)
    {
        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");
        actor = (HubActor)GetNode("Actor");
        map = GetNode<TileCommon>("TileMap");
        if (actor == null)
        {
            actor = new HubActor();
            AddChild(actor);
        }
        if (player != null)
        {
            RemoveChild(player);
            actor.Activate(player);
        }

        for (int i = 0; i < containedLevels.Count; i++)
        {
            map.GetScenes()[i].Instantiate();
        }

        base.EnterLevel(p);

    }

    public override void ResetLevel()
    {

    }

    public override void _PhysicsProcess(double delta)
    {

    }

    public override void ExitLevel()
    {
        base.ExitLevel();
        RemoveChild(player);
    }
}
