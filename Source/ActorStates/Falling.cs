using Godot;
using System;

public class Falling : State
{
    bool doubleJump = true;

    public override void _Ready()
    {
        StateName = "FALL";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        player.player.Play("Fall");
    }


    public override void FixedUpdate(float delta)
    {
        if (Input.IsActionJustPressed("Jump"))
        {
            stateMachine.UpdateState("HELICOPTER");
        }

        if (player.IsOnFloor())
        {
            stateMachine.UpdateState("IDLE");
        }
    }

    public override void Exit()
    {
        player.player.Stop(false);
    }
}
