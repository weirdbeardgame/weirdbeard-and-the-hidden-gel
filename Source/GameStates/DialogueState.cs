using Godot;
using System;

public partial class DialogueState : State
{
    public override void _Ready()
    {
        StateName = "DIALOGUE";
        StateMachine = (StateMachine)GetParent<StateMachine>();
        StateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        Player = (Player)SceneManager.CurrentScene.GetNode("Player");
        Player.CanMove = false;
    }

    public override void FixedUpdate(double delta)
    {
        // Handle UI input processing
    }

    public override void Stop()
    {
        Player.CanMove = true;
    }
}
