using Godot;
using System;

public class DialogueState : State
{
    public override void _Ready()
    {
        stateName = "DIALOGUE";
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

    public override void Exit()
    {
        base.Exit();
    }
}
