using Godot;
using System;

public class Game : State
{
    GameManager manager;

    public override void _Ready()
    {
        stateName = "GAME";
        //manager = (GameManager)GetParent<GameManager>();
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        // Start basic game logic. Clear player state newgame whateva's
        NewGame();
    }

    public void NewGame()
    {
        GD.Print("New Game");
    }

    public override void FixedUpdate(float delta)
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
