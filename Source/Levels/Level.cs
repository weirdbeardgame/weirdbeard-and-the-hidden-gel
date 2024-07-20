using Godot;
using System;
using Godot.Collections;

[Tool]
public partial class Level : LevelCommon
{
    [Export]
    Dictionary<string, PackedScene> sublevels;

    [Export]
    public Array<EnemySpawner> ActiveEnemySpanwers;

    [Export]
    public Array<Exit> exits;

    protected Checkpoint CurrentCheckpoint;

    [Export]
    public int maxEnemyAmnt;
    LevelCommon ActiveSubScene;

    // Currently active subScene. Otherwise null
    Level SubScene;
    Camera2D camera;

    Rect2 mapLimits;
    Vector2 mapCellsize;
    Node2D PlayerStartPoint;

    public override void EnterLevel(Player p)
    {
        base.EnterLevel(p);

        switch (_LevelState)
        {
            case LevelState.COMPLETE:
                // Some objectives were completed, powerups that may be perma in the scene may be collected etc.
                // Can also use this in games where there could be optional timed modes.
                break;

            case LevelState.NON_COMPLETE:

                break;
        }

        _LevelState = LevelState.ACTIVE;

        if (ActiveEnemySpanwers != null)
        {
            foreach (var spawner in ActiveEnemySpanwers)
            {
                spawner.Spawn();
            }
        }

        if (!_Player.IsInsideTree())
        {
            AddChild(_Player);
        }

        _Player.ResetPlayer();
        _Player.ResetPlayerPosition(CurrentCheckpoint);

        //CreateAudioStream();
    }

    public override void Update()
    {
        base.Update();

    }

    public void ResetPlayerPosition()
    {
        PlayerStartPoint = GetNode<Node2D>("PlayerStartPoint");

        if (CurrentCheckpoint != null)
        {
            _Player.Position = CurrentCheckpoint.GlobalPosition;
        }
        else
        {
            // Need to grab a "Player Starting place"
            _Player.Position = PlayerStartPoint.GlobalPosition;
        }
    }

    public override void ResetLevel()
    {
        _Player.ResetPlayerPosition(CurrentCheckpoint);
        RemoveChild(_Player);
        EnterLevel(_Player);
    }
    public void Checkpoint(Checkpoint NewCheckpoint)
    {
        if (CurrentCheckpoint != null)
        {
            CurrentCheckpoint.Deactivate();
        }
        NewCheckpoint.isActive = true;
        CurrentCheckpoint = NewCheckpoint;
    }

    // Clear the enemies and other data from the scene.
    // Ensure the scene closes properly before changing.
    public override void ExitLevel()
    {
        RemoveChild(_Player);
        foreach (var spawner in ActiveEnemySpanwers)
        {
            spawner.Destroyed();
        }



        QueueFree();
    }
}

