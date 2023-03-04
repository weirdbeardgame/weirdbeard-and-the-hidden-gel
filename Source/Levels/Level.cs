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

    TileMap tileMap;

    Camera2D camera;

    Rect2 mapLimits;
    Vector2 mapCellsize;

    AudioStreamPlayer backgroundPlayer;

    [Export] Resource audioFile;

    public override void EnterLevel(Player p)
    {
        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");

        base.EnterLevel(p);

        AddChild(player);

        if (currentCheckpoint != null)
        {
            player.Position = currentCheckpoint.GlobalPosition;
        }
        else
        {
            currentCheckpoint = (Checkpoint)GetNode("0");
            if (currentCheckpoint == null)
            {
                GD.PushError("No Active Checkpoints in Scene!");
            }
            player.Position = currentCheckpoint.GlobalPosition;
        }
        player.ResetState();
        tileMap = (TileMap)GetNode("Layer1");
        camera = (Camera2D)player.GetNode("Camera2D");

        camera.Enabled = true;


        mapLimits = tileMap.GetUsedRect();

        SetCameraBounds();
        CreateAudioStream();
    }

    void CreateAudioStream()
    {
        backgroundPlayer.Stream = GD.Load<AudioStream>(audioFile.ResourcePath);
        backgroundPlayer.Play();
    }

    public void SetCameraBounds()
    {
        camera.LimitRight = (int)(mapLimits.End.X * mapCellsize.X);
        camera.LimitLeft = (int)(mapLimits.Position.X * mapCellsize.X);

        camera.LimitBottom = (int)(mapLimits.End.Y * mapCellsize.Y);
        camera.LimitTop = (int)(mapLimits.Position.Y * mapCellsize.Y);
    }

    public override void ResetLevel()
    {
        foreach (var enemy in activeEnemies)
        {
            enemy.Destroy();
            enemy.Free();
        }
        activeEnemies.Clear();
        player.ResetState();
        player.Position = currentCheckpoint.GlobalPosition;
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
        RemoveChild(player);
        if (activeEnemies != null)
        {
            foreach (var enemy in activeEnemies)
            {
                enemy.Destroy();
            }
            activeEnemies.Clear();
        }
        player = null;
    }
}
