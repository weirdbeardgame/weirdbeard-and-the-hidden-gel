using Godot;
using System;
using Godot.Collections;

public partial class Level : LevelCommon
{
    [Export]
    Dictionary<string, PackedScene> sublevels;

    [Export]
    public Array<Enemy> activeEnemies;

    [Export]
    public Array<Exit> exits;

    protected Checkpoint currentCheckpoint;

    [Export]
    public int maxEnemyAmnt;

    SceneManager scenes;

    LevelCommon ActiveSubScene;

    // Currently active subScene. Otherwise null
    Level subScene;

    Camera2D camera;

    Rect2 mapLimits;
    Vector2 mapCellsize;

    public override void EnterLevel(Player p)
    {
        base.EnterLevel(p);
        if (currentCheckpoint != null)
        {
            Player.Position = currentCheckpoint.GlobalPosition;
        }
        else
        {
            currentCheckpoint = (Checkpoint)GetNode("0");
            if (currentCheckpoint == null)
            {
                GD.PushError("No Active Checkpoints in Scene!");
            }
            Player.Position = currentCheckpoint.GlobalPosition;
        }
        AddChild(Player);
        Player.ActivateCamera();
        Player.ResetState();
        CreateAudioStream();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ResetLevel()
    {
        GD.Print("LevelReset");
        Player.ResetState();
        ExitLevel();
        EnterLevel(Player);
    }

    public void Checkpoint(Checkpoint newCheckpoint)
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.Deactivate();
        }
        newCheckpoint.isActive = true;
        currentCheckpoint = newCheckpoint;
    }

    // Clear the enemies and other data from the scene.
    // Ensure the scene closes properly before changing.
    public override void ExitLevel()
    {
        //RemoveChild(Player);
        if (activeEnemies != null)
        {
            foreach (var enemy in activeEnemies)
            {
                enemy.Destroy();
            }
            activeEnemies.Clear();
        }
        Player = null;
    }
}
