using Godot;
using System;
using System.Collections.Generic;

public class Level : Node
{
    [Export]
    public string name;

    [Export]
    Dictionary<string, PackedScene> sublevels;

    Player player;

    SceneManager scenes;

    // A self refrence for loading sub levels
    Level self;

    // Currently active subScene. Otherwise null
    Level subScene;

    // Set player position to spawnpoint if nessacary.
    // Reset Enemies and Players states. Set NPC states
    public void EnterLevel(Player p)
    {
        player = p;
        AddChild(player);
        scenes = (SceneManager)GetNode("/root/GameManager/SceneManager");
        Node2D spawn = (Node2D)GetNode("SpawnPoint");
        player.Position = spawn.GlobalPosition;
        player.ResetState();
    }

    // Unload root node or rather suspend it, apply nodes in sub scene
    // Without changing the engine "CurrentScene" Keep main scene loaded in background.
    public void EnterSubLevel(string sub)
    {
        self = scenes.CurrentScene;
        var tileMap = (TileMap)GetNode("TileMap");
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

    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }
}
