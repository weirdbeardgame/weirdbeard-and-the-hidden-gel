using Godot;
using System;

public partial class Falling : State
{
    Vector2 inputVelocity;

    public override void _Ready()
    {
        StateName = "FALL";
        Player = (Player)GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
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
            Player.Gravity = Player.FallGravity;
            GD.Print("Fall Gravity: ", Player.Gravity);
        }
    }

    public override void FixedUpdate(double delta)
    {
        if (Input.IsActionPressed("Jump") && Player.CanJump())
        {
            GD.Print("Jump Again");
            StateMachine.ResetToOldState();
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