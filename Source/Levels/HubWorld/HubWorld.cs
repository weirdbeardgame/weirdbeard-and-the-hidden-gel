using Godot;
using System;
using System.Collections.Generic;

public class HubWorld : LevelCommon
{
    [Export] List<NodePath> containedLevels;

    LevelSpaces currentSpace;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void EnterLevel(Player p)
    {
        player = p;
        currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
        player.Position = currentSpace.GlobalPosition;
        AddChild(player);
    }

    public override void ResetLevel()
    {
        currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
        player.Position = currentSpace.GlobalPosition;
    }

    public override void ExitLevel()
    {
        base.ExitLevel();
    }
}
