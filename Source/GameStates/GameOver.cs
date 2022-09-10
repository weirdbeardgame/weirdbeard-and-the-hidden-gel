using Godot;
using System;

public class GameOver : State
{

    [Export]
    PackedScene gameOverScene;
    public override void _Ready()
    {
        stateName = "GAMEOVER";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate(float delta)
    {
        // Handle UI input processing
    }

    public override void Stop()
    {
        base.Stop();
    }

}
