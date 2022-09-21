using Godot;
using System;

public class Jump : State
{
    Vector2 inputVelocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "JUMP";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        player.player.Play("Jump");
        if (!player.projectileMotionJump)
        {
            inputVelocity.y = -player.maxJumpImpulse;
        }
        if (player.projectileMotionJump)
        {
            player.gravity = player.jumpGravity;
            inputVelocity.y = player.jumpVelocity;
            GD.Print("Jump Gravity: ", player.jumpGravity);
        }
        inputVelocity.x = player.Velocity.x;
        player.Velocity = inputVelocity;

        player.BufferJump();

        stateMachine.UpdateState("AIR");
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void Stop()
    {
        player.player.Stop(true);
    }
}
