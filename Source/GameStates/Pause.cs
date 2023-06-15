using Godot;
using System;

public partial class Pause : State
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "PAUSE";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        // Play screen darken animation. Open UI
        GetTree().Paused = true;
    }
    public override void FixedUpdate(double delta)
    {
        if (Input.IsActionJustPressed("Pause"))
        {
            Stop();
        }
    }

    public override void Stop()
    {
        // Close UI and play animation
        GetTree().Paused = false;
        stateMachine.ResetToOldState();
    }
}
