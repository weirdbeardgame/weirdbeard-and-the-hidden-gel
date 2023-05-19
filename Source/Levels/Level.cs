using Godot;
using System;
using Godot.Collections;

public partial class Level : LevelCommon
{
    [Export]
    Dictionary<string, PackedScene> sublevels;

    SceneManager scenes;

    // Currently active subScene. Otherwise null
    Level subScene;

    Camera2D camera;

    Rect2 mapLimits;
    Vector2 mapCellsize;

    AudioStreamPlayer backgroundPlayer;

    [Export] Resource audioFile;

    public override void EnterLevel(Player p)
    {
        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");

        base.EnterLevel(p);

        if (!HasNode(Player.GetPath()))
        {
            AddChild(Player);
        }
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
        Player.ResetState();
        CreateAudioStream();
    }

    void CreateAudioStream()
    {
        backgroundPlayer.Stream = GD.Load<AudioStream>(audioFile.ResourcePath);
        backgroundPlayer.Play();
    }

    public override void ResetLevel()
    {
        GD.Print("LevelReset");
        activeEnemies.Clear();
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
        RemoveChild(Player);
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
