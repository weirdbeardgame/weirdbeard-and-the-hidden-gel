using Godot;
using System;

public partial class Falling : State
{
    Vector2 _InputVelocity;

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
            GD.Print("Fall Gravity: ", Player.FallGravity);
        }
    }

    public override void FixedUpdate(double delta)
    {
        if (Input.IsActionPressed("Jump") && Player.CanJumpAgain)
        {
            GD.Print("Jump Again");

            if (Player.Velocity.X == 0)
            {
                if (Input.IsActionPressed("Right"))
                {
                    _InputVelocity.X = 1 * Player.Speed;
                }

                else if (Input.IsActionPressed("Left"))
                {
                    _InputVelocity.X = -1 * Player.Speed;
                }

                else
                {
                    _InputVelocity.X = 0;
                }

                Player.Velocity = _InputVelocity;
            }

            GD.Print("Velocity on Double Jump", Player.Velocity);

            StateMachine.ResetToOldState();
        }
        if (Player.IsOnFloor())
        {
            Player.ResetPlayer();
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(false);
    }
}
