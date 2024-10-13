using Godot;
using System;

public partial class Swim : State
{
    Vector2 _InputVelocity = Vector2.Zero;
    float currentSpeed = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "SWIM";
        Player = (Player)GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        //Player.AnimationPlayer.Play("Swim");
        Player.IsSwimming = true;
    }

    public override Vector2 GetInput()
    {
        if (Input.IsActionPressed("Run"))
        {
            GD.Print("Run");
            currentSpeed = Player.FastSwimSpeed;
        }
        else
        {
            currentSpeed = Player.SwimSpeed;
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

        if (Input.IsActionJustPressed("Jump"))
        {
            _InputVelocity.Y += -1;
        }

        else
        {
            _InputVelocity.Y = 0;
        }
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
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
        Player.IsSwimming = false;
    }
}
