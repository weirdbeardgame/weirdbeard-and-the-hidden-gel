using Godot;
using System;

public class Store : State
{
    public override void _Ready()
    {
        stateName = "STORE";
        player = (Player)Owner.GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
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
