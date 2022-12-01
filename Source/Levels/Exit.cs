using Godot;
using System;

// The idea would be to take the player back to the other spot they entered or left from like a door
// Or just take a player to the associated level / screen that's attached to this exit.

public class Exit : Node2D
{
    [Export]
    PackedScene toTransportTo;

    [Export]
    Exit connectedExit;

    Level currentLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentLevel = (Level)Owner;
    }

    public void OnExit(object body)
    {
        LevelCommon scene = (LevelCommon)toTransportTo.Instance();
        if (body is Player)
        {
            SceneManager.changeScene(scene.levelName, (Player)currentLevel.GetNode("Player"));
        }
    }
}
