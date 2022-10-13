using Godot;
using System;
using System.Collections.Generic;

public enum Direction { N, S, E, W };

public class LevelSpaces : Node2D
{
    [Export] LevelCommon attachedLevel;

    [Export] private Dictionary<Direction, NodePath> attachedPaths;

    Action playerTouched;

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
        //playerTouched.Invoke();
        if (body is Player)
        {
            player = (Player)body;
        }
    }

    public void EnterLevel()
    {
        attachedLevel.EnterLevel(player);
    }

    public bool CanMove(Direction dir)
    {
        if (attachedPaths != null)
        {
            return attachedPaths.ContainsKey(dir);
        }
        return false;
    }

}
