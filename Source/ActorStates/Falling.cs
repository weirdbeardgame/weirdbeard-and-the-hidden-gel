using Godot;
using System;

public class Falling : State
{
    bool doubleJump = true;

    public override void _Ready()
    {
        stateName = "FALL";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        player.player.Play("Fall");
        player.gravity = 3500f;
    }

    public override void FixedUpdate(float delta)
    {
        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            player.canJumpAgain = false;
            stateMachine.UpdateState("JUMP");
        }

        if (player.IsOnFloor())
        {
            player.ResetState();
        }
    }

    public override void Stop()
    {
        player.player.Stop(false);
    }
}
