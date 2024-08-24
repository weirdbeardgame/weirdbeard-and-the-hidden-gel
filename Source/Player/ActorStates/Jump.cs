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

    // ~~The mistake is in the State Machine~~ No!
    // The mistake is in the physics. (Future Timotheus)

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
        if (Input.IsActionJustReleased("Jump"))
        {
            _InputVelocity.Y /= 4;
        }

        GD.Print("Jump Gravity: ", Player.JumpGravity);
        GD.Print("Jump Velocity: ", Player.JumpVelocity);

        _InputVelocity.X = Player.Velocity.X;
        Player.Velocity = _InputVelocity;

        Player.NumJumps -= 1;
        Player.wasOnFloor = true;
        GD.Print("Velocity: ", Player.Velocity);
    }

    public override void FixedUpdate(double delta)
    {
        base.FixedUpdate(delta);

        Player.BufferJump();
    }

    public override void Update(double delta)
    {
        base.Update(delta);

        Player.GetAirState();

        if (Input.IsActionJustPressed("Jump") && Player.CanJumpAgain)
        {
            StateMachine.ResetActor();
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
    }
}
