using Godot;
using System;

public class Death : State
{

    bool isDead = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "DEATH";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        if (player.playerLives > 0 && !isDead)
        {
            player.Die();
        }
        else
        {
            // GAME OVER
        }
    }

    public override void Stop()
    {
        isDead = true;
    }

}
