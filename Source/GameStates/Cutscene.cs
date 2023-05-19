using Godot;
using System;

public partial class Cutscene : State
{
    public override void _Ready()
    {
        stateName = "CUTSCENE";
        Player = (Player)Owner.GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate(double delta)
    {
        // Handle UI input processing
    }

    public override void Stop()
    {
        base.Stop();
    }
}
