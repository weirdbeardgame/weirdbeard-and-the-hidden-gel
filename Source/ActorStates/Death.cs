using Godot;
using System;

public class Death : State
{
    public override void Start()
    {
        base.Start();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "DEATH";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Update(float delta)
    {
        if (player.lives > 0)
        {
            GetTree().ReloadCurrentScene();
            player.lives -= 1;
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
