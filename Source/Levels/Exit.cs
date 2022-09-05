using Godot;
using System;

// The idea would be to take the player back to the other spot they entered or left from like a door
// Or just take a player to the associated level / screen that's attached to this exit.

public class Exit : Node
{
    SceneManager scenes;

    [Export]
    LevelCommon toTransportTo;

    Checkpoint checkpoint;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        scenes = (SceneManager)GetNode("/root/GameManager/SceneManager");
    }

    public void OnExit(object body)
    {
        if (body is Player)
        {
            checkpoint.Activate(body);
            scenes.SwitchLevel(toTransportTo, (Player)body);
        }
    }
}
