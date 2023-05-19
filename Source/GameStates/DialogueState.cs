using Godot;
using System;

public partial class DialogueState : State
{
    public override void _Ready()
    {
        stateName = "DIALOGUE";
        stateMachine = (StateMachine)GetParent<StateMachine>();
        stateMachine.AddState(this, stateName);
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
