using Godot;
using System;

public class Death : State
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "DEATH";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        if (PlayerData.playerLives > 0)
        {
            player.isDie();
        }
        else
        {
            // GAME OVER
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
