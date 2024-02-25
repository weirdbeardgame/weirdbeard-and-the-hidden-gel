using Godot;
using System;

public partial class Death : State
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "DEATH";
        Player = (Player)GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        SceneManager.ResetLevel();
    }

    public override void Stop()
    {
    }

}
