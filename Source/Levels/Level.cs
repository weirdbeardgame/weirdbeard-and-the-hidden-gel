using Godot;
using System;
using System.Collections.Generic;

public class Level : Node
{
    [Export]
    public string name;

    [Export]
    Dictionary<string, PackedScene> sublevels;

    [Export]
    public int maxEnemyAmnt;

    [Export]
    public List<Enemy> activeEnemies;

    Checkpoint currentCheckpoint;

    Player player;

    SceneManager scenes;

    // A self refrence for loading sub levels
    Level self;

    // Currently active subScene. Otherwise null
    Level subScene;

    public void EnterLevel(Player p)
    {
        player = p;
        scenes = (SceneManager)GetNode("/root/GameManager/SceneManager");

        activeEnemies = new List<Enemy>();

        AddChild(player);

        if (currentCheckpoint != null)
        {
            player.Position = currentCheckpoint.GlobalPosition;
        }
        else
        {
            currentCheckpoint = (Checkpoint)scenes.CurrentScene.GetNode("0");
            if (currentCheckpoint == null)
            {
                GD.PushError("No Active Checkpoints in Scene!");
            }
            player.Position = currentCheckpoint.GlobalPosition;
        }
        player.ResetState();
    }

    public void ResetLevel()
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

    // Unload root node or rather suspend it, apply nodes in sub scene
    // Without changing the engine "CurrentScene" Keep main scene loaded in background.
    public void EnterSubLevel(string sub)
    {
        self = scenes.CurrentScene;
        var tileMap = (TileMap)scenes.CurrentScene.GetNode("TileMap");
        if (sublevels != null)
        {
            Node toLoad = sublevels[sub].Instance();
            tileMap.Hide();
            GetTree().Root.AddChild(toLoad);
        }
    }

    public void ExitSubLevel(string sub)
    {
        subScene.Free();
        var tileMap = (TileMap)GetNode("TileMap");
        tileMap.Show();
    }

    // Clear the enemies and other data from the scene.
    // Ensure the scene closes properly before changing.
    public void ExitLevel()
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
