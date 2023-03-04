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
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        player.canMove = false;
    }

    public override void FixedUpdate(double delta)
    {
        // Handle UI input processing
    }

    public override void Stop()
    {
        player.canMove = true;
    }
}
