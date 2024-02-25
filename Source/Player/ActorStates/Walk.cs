using Godot;
using System;

public partial class Walk : State
{
    Vector2 _InputVelocity = Vector2.Zero;
    float currentSpeed = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "WALK";
        Player = (Player)GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        Player.AnimationPlayer.Play("Walk");
    }

    public override Vector2 GetInput()
    {
        if (Input.IsActionPressed("Run"))
        {
            GD.Print("Run");
            currentSpeed = Player.RunSpeed;
        }
        else
        {
            GD.Print("Walk");
            currentSpeed = Player.Speed;
        }
        if (Input.IsActionPressed("Right"))
        {
            _InputVelocity = Vector2.Right * currentSpeed;
        }
        else if (Input.IsActionPressed("Left"))
        {
            _InputVelocity = Vector2.Left * currentSpeed;
        }
        if (!Input.IsActionPressed("Left") && !Input.IsActionPressed("Right"))
        {
            _InputVelocity.X = 0;
        }

        _InputVelocity.Y = 0;

        return _InputVelocity;
    }

    public override void FixedUpdate(double delta)
    {
        if (Player.IsOnFloor())
        {
            Player.Velocity = GetInput();

            if (_InputVelocity.Sign().X < 0)
            {
                Player.Direction = Vector2.Left;
                Player.WeirdBeard.FlipH = true;
            }

            else if (_InputVelocity.Sign().X > 0)
            {
                Player.Direction = Vector2.Right;
                Player.WeirdBeard.FlipH = false;
            }

            if (_InputVelocity == Vector2.Zero)
            {
                StateMachine.UpdateState("IDLE");
            }
        }
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        // Oops, fell off platform.
        if (Player.Velocity.Y > 0)
        {
            Player.wasOnFloor = true;
            StateMachine.UpdateState("FALL");
        }

        if (Input.IsActionJustPressed("Jump") && Player.CanJump())
        {
            StateMachine.UpdateState("JUMP");
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
    }
}
