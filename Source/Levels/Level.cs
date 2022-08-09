using Godot;
using System;
using System.Collections.Generic;

public class Level : Node
{

    [Export]
    Dictionary<Node, string> sublevels;

    // Set player position to spawnpoint if nessacary. 
    // Reset Enemies and Players states. Set NPC states
    public void EnterLevel()
    {
    }

    // Unload root node or rather suspend it, apply nodes in sub scene
    // Without changing the engine "CurrentScene" Keep main scene loaded in background.
    public void EnterSubLevel(string sub)
    {
    }

    public void ExitSubLevel(string sub)
    {
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
