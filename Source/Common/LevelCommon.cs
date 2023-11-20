using Godot;
using System;
using Godot.Collections;

public enum LevelType { GRASS, ISLAND, ICE, WATER, DEFAULT }

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

    AudioStreamPlayer backgroundPlayer;

    [Export] Resource audioFile;


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

    public virtual void EnterLevel(Player p, LevelType t)
    {
        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");
        if (p != null)
        {
            Player = p;
        }

        else if ((Player = (Player)GetNode("Player")) != null)
        {
            GD.Print("Player Found");
        }
    }

    public void CreateAudioStream()
    {
        backgroundPlayer.Stream = GD.Load<AudioStream>(audioFile.ResourcePath);
        backgroundPlayer.Play();
    }


    public void CompleteLevel()
    {
        complete = true;
        Player.ResetState();
        ExitLevel();
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Update();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        FixedUpdate();
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
