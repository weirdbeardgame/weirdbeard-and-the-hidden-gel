using Godot;
using System;

public partial class GameOver : State
{

    [Export]
    PackedScene gameOverScene;
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

    public override void FixedUpdate(double delta)
    {
        // Handle UI input processing
    }

    public override void Stop()
    {
        base.Stop();
    }

}
