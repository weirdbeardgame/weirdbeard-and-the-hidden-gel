using Godot;
using System;
using System.Collections.Generic;

public class Level : LevelCommon
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
        activeEnemies = new List<Enemy>();

        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");

        if (p != null)
        {
            player = p;
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
        }
        else
        {
            player = (Player)GetNode("Player");
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
        }

        tileMap = (TileMap)GetNode("TileMap");
        camera = (Camera2D)player.GetNode("Camera2D");

        mapCellsize = tileMap.CellSize;
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
        camera.LimitRight = (int)(mapLimits.End.x * mapCellsize.x);
        camera.LimitLeft = (int)(mapLimits.Position.x * mapCellsize.x);

        camera.LimitBottom = (int)(mapLimits.End.y * mapCellsize.y);
        camera.LimitTop = (int)(mapLimits.Position.y * mapCellsize.y);
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
