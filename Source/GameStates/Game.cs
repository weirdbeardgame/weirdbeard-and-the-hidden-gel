using Godot;
using System;

public class Game : State
{
    GameManager manager;

    SceneManager scenes;

    public override void _Ready()
    {
        StateName = "GAME";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, StateName);
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

    public override void Exit()
    {
        base.Exit();
    }

    public void QuitGame()
    {

    }
}
