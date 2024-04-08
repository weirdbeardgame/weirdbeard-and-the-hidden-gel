using Godot;
using System;

public partial class Jump : State
{
    Vector2 _InputVelocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "JUMP";
        Player = (Player)GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
    }

    // The mistake is in the State Machine

    public override void Start()
    {
        GD.Print("Jump");
        Player.AnimationPlayer.Play("Jump");
        if (!Player.projectileMotionJump)
        {
            _InputVelocity.Y = -Player.maxJumpImpulse;
        }
        if (Player.projectileMotionJump)
        {
            _InputVelocity.Y = Player.JumpVelocity;
        }

        GD.Print("Jump Gravity: ", Player.JumpGravity);
        GD.Print("Jump Velocity: ", Player.JumpVelocity);

        _InputVelocity.X = Player.Velocity.X;
        Player.Velocity = _InputVelocity;

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
            StateMachine.ResetActor();
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
    }
}
