using Godot;
using System;

public partial class Jump : State
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

    // The mistake is in the State Machine

    public override void Start()
    {
        player.player.Play("Jump");
        if (!player.projectileMotionJump)
        {
            inputVelocity.Y = -player.maxJumpImpulse;
        }
        if (player.projectileMotionJump)
        {
            player.gravity = player.JumpGravity;
            inputVelocity.Y = player.JumpVelocity;
        }

        inputVelocity.X = player.Velocity.X;
        player.Velocity = inputVelocity;

        player.BufferJump();
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            player.canJumpAgain = false;
            stateMachine.ResetState();
        }

        player.GetAirState();
    }

    public override void Stop()
    {
        player.player.Stop(true);
    }
}
