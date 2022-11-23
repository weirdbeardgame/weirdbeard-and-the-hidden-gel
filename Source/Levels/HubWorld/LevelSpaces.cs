using Godot;
using System;
using System.Collections.Generic;

public enum Direction { N, S, E, W };

public class LevelSpaces : Node2D
{
    [Export] PackedScene attachedLevel;

    [Export] private Dictionary<Direction, NodePath> attachedPaths;

    public Player player;

    public Dictionary<Direction, NodePath> AttachedPaths
    {
        get
        {
            return attachedPaths;
        }
    }

    public void EnterLevel()
    {
        RemoveChild(player);
        SceneManager.changeScene(attachedLevel, player);
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
