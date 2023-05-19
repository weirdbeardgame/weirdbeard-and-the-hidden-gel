using Godot;
using System;
using Godot.Collections;

public enum LevelTypes { LEVEL, SUB, HUB }
public enum LevelElements { GRASS, ISLAND, ICE, WATER }

public partial class LevelCommon : Node2D
{
    [Export]
    public string levelName;

    protected Player Player;

    protected Checkpoint currentCheckpoint;

    [Export]
    public int maxEnemyAmnt;

    [Export]
    public Array<Enemy> activeEnemies;

    [Export]
    public Array<Exit> exits;

    public LevelTypes levelType;

    bool unlocked;
    bool complete;

    public bool isComplete
    {
        get
        {
            return complete;
        }
    }

    public bool isUnlocked
    {
        get
        {
            return unlocked;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public virtual void EnterLevel(Player p)
    {
        if (p != null)
        {
            Player = p;
        }

        else if ((Player = (Player)GetNode("Player")) != null)
        {
            GD.Print("Player Found");
        }
    }

    public void CompleteLevel()
    {
        complete = true;
        Player.ResetState();
        ExitLevel();
    }

    public virtual void ExitLevel()
    {

    }

    public virtual void ResetLevel()
    {

    }

    public virtual void EnterSubLevel(Player Player, Level parent)
    {

    }

    public virtual void ExitSubLevel()
    {

    }
}
