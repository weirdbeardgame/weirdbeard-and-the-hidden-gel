using Godot;
using System;

public partial class Jump : State
{
    Vector2 inputVelocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "JUMP";
        Player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    // The mistake is in the State Machine

    public override void Start()
    {
        Player.AnimationPlayer.Play("Jump");
        if (!Player.projectileMotionJump)
        {
            inputVelocity.Y = -Player.maxJumpImpulse;
        }
        if (Player.projectileMotionJump)
        {
            Player.gravity = Player.JumpGravity;
            inputVelocity.Y = Player.JumpVelocity;
        }

        GD.Print("Jump Gravity: ", Player.JumpGravity);
        GD.Print("Jump Velocity: ", Player.JumpVelocity);

        inputVelocity.X = Player.Velocity.X;
        Player.Velocity = inputVelocity;

        Player.NumJumps -= 1;
        GD.Print("Velocity: ", Player.Velocity);

        Player.BufferJump();
    }

    public override void FixedUpdate(double delta)
    {
        base.FixedUpdate(delta);
        Player.GetAirState();
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        if (Input.IsActionJustPressed("Jump") && Player.CanJump())
        {
            stateMachine.ResetState();
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
    }
}
