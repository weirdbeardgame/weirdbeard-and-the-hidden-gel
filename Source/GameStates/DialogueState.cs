using Godot;
using System;

public partial class DialogueState : State
{
    public override void _Ready()
    {
        StateName = "DIALOGUE";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        Player = (Player)SceneManager.CurrentScene.GetNode("Player");
        Player.canMove = false;
    }

    public override void FixedUpdate(double delta)
    {
        // Handle UI input processing
    }

    public override void Stop()
    {
        Player.canMove = true;
    }
}
