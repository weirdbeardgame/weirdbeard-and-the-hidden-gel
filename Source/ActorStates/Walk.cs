using Godot;
using System;

public partial class Walk : State
{
    Vector2 inputVelocity = Vector2.Zero;
    float currentSpeed = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "WALK";
        Player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        Player.AnimationPlayer.Play("Walk");
    }

    public override Vector2 GetInput()
    {
        if (Input.IsActionPressed("Run"))
        {
            currentSpeed = Player.runSpeed;
        }
        else
        {
            currentSpeed = Player.speed;
        }
        if (Input.IsActionPressed("Right"))
        {
            inputVelocity.X = 1.0f * currentSpeed;
        }
        else if (Input.IsActionPressed("Left"))
        {
            inputVelocity.X = -1.0f * currentSpeed;
        }
        if (!Input.IsActionPressed("Left") && !Input.IsActionPressed("Right"))
        {
            inputVelocity.X = 0;
        }
        return inputVelocity;
    }

    public override void FixedUpdate(double delta)
    {
        if (Player.IsOnFloor())
        {
            GetInput();

            if (inputVelocity.Sign().X < 0)
            {
                Player.direction = Vector2.Left;
                Player.weirdBeard.FlipH = true;
            }

            else if (inputVelocity.Sign().X > 0)
            {
                Player.direction = Vector2.Right;
                Player.weirdBeard.FlipH = false;
            }

            if (inputVelocity == Vector2.Zero)
            {
                stateMachine.UpdateState("IDLE");
            }
            Player.Velocity = inputVelocity;
        }
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        // Oops, fell off platform.
        if (Player.Velocity.Y > 0)
        {
            Player.wasOnFloor = true;
            stateMachine.UpdateState("FALL");
        }

        if (Input.IsActionJustPressed("Jump") && Player.CanJump())
        {
            stateMachine.UpdateState("JUMP");
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
    }
}
