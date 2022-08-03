using Godot;
using System;

public class Pause : State
{

    bool isPaused;

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

        PauseGame();
    }

    void PauseGame()
    {
        if (isPaused)
        {
            return;
        }
        else
        {
            isPaused = true;
            GetTree().Paused = true;
        }
    }

    void UnpauseGame()
    {
        if (!isPaused)
        {
            return;
        }
        else
        {
            isPaused = false;
            GetTree().Paused = false;
        }
    }

    public override void FixedUpdate(float delta)
    {
        // Handle UI input processing

    }

    public override void Exit()
    {
        base.Exit();
    }
}
