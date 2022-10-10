using Godot;
using System;
using System.Collections.Generic;

public enum Direction { N, S, E, W };

public class LevelSpaces : Node2D
{
    [Export] LevelCommon attachedLevel;

    [Export] private Dictionary<Direction, NodePath> attachedPaths;

    public Dictionary<Direction, NodePath> AttachedPaths
    {
        get
        {
            return attachedPaths;
        }
    }

    Player player;

    public void Enter(object body)
    {
        if (body is Player)
        {
            player = (Player)body;
            if (Input.IsActionJustPressed("Submit"))
            {
                attachedLevel.EnterLevel(player);
            }
        }
    }

    public bool CanMove(Direction dir)
    {
        return attachedPaths.ContainsKey(dir);
    }

}
