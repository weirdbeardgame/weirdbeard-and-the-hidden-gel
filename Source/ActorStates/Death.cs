using Godot;
using System;

public partial class Death : State
{

    bool isDead = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "DEATH";
        Player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        if (!isDead)
        {
            isDead = true;
            Player.Die();
        }
        else
        {
            // GAME OVER
        }
    }

    public override void Stop()
    {
        isDead = false;
    }

}
