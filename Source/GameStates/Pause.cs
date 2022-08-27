using Godot;
using System;

public class Pause : State
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "PAUSE";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        // Play screen darken animation. Open UI
        GetTree().Paused = true;
    }
    public override void FixedUpdate(float delta)
    {
    }

    public override void Exit()
    {
        // Close UI and play animation
        GetTree().Paused = false;
    }
}
