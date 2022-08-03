using Godot;
using System;

public class GameOver : State
{
    public override void _Ready()
    {
        StateName = "GAMEOVER";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        base.Start();
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
