using Godot;
using System;

public partial class Falling : State
{
    Vector2 inputVelocity;

    public override void _Ready()
    {
        StateName = "FALL";
        Player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        Player.AnimationPlayer.Play("Fall");
        if (Player.wasOnFloor)
        {
            GD.Print("Was On Floor");
            Player.StartCoyoteTimer();
        }
        if (Player.projectileMotionJump)
        {
            Player.gravity = Player.FallGravity;
            GD.Print("Fall Gravity: ", Player.gravity);
        }
    }

    public override void FixedUpdate(double delta)
    {
        if (Input.IsActionPressed("Jump") && Player.CanJump())
        {
            GD.Print("Jump Again");
            stateMachine.ResetToOldState();
        }
        if (Player.IsOnFloor())
        {
            Player.ResetState();
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(false);
    }
}
