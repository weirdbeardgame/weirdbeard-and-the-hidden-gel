using Godot;
using System;
using System.Collections.Generic;

public enum LevelTypes { LEVEL, SUB, HUB }


public class LevelCommon : Node
{
    [Export]
    public string levelName;

    protected Player player;

    protected Checkpoint currentCheckpoint;

    [Export]
    public int maxEnemyAmnt;

    [Export]
    public List<Enemy> activeEnemies;

    [Export]
    public List<Exit> exits;

    public LevelTypes levelType;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public virtual void EnterLevel(Player p)
    {

    }

    public virtual void ExitLevel()
    {

    }

    public virtual void ResetLevel()
    {

    }

    public virtual void EnterSubLevel(Player player, Level parent)
    {

    }

    public virtual void ExitSubLevel()
    {

    }
}
