using Godot;
using System;
using Godot.Collections;

public enum Direction { N, S, E, W };

public partial class LevelSpaces : Node2D
{
    [Export] PackedScene attachedLevel;

    [Export] private Dictionary<Direction, NodePath> attachedPaths;

    public HubActor actor;

    public Dictionary<Direction, NodePath> AttachedPaths
    {
        get
        {
            return attachedPaths;
        }
    }

    public void EnterLevel()
    {
        LevelCommon scene = attachedLevel.Instantiate<LevelCommon>();
        actor.Deactivate();
        SceneManager.changeScene(scene.levelName, actor.Player);
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
