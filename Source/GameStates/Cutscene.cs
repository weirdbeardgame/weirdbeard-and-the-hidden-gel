using Godot;
using System;

public partial class Cutscene : State
{
    public override void _Ready()
    {
        StateName = "CUTSCENE";
        Player = (Player)Owner.GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
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
