using Godot;
using System;

public partial class Game : State
{
    GameManager manager;

    public override void _Ready()
    {
        StateName = "GAME";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
    }

    public void NewGame()
    {
        GD.Print("New Game");
    }

    public override void FixedUpdate(double delta)
    {
        if (Input.IsActionJustPressed("Pause"))
        {
            stateMachine.UpdateState("PAUSE");
        }
    }

    public override void Stop()
    {
        base.Stop();
    }

    public void QuitGame()
    {

    }
}
