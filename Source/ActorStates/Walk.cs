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
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        player.player.Play("Walk");
    }

    public override Vector2 GetInput()
    {
        if (Input.IsActionPressed("Run"))
        {
            currentSpeed = player.runSpeed;
        }
        else
        {
            currentSpeed = player.speed;
        }
        if (Input.IsActionPressed("Right"))
        {
            GD.Print("Right");
            inputVelocity.X = 1.0f * currentSpeed;
        }
        else if (Input.IsActionPressed("Left"))
        {
            GD.Print("Left");
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
        if (player.IsOnFloor())
        {
            GetInput();

            if (inputVelocity.Sign().X < 0)
            {
                player.direction = Vector2.Left;
                player.weirdBeard.FlipH = true;
            }

            else if (inputVelocity.Sign().X > 0)
            {
                player.direction = Vector2.Right;
                player.weirdBeard.FlipH = false;
            }

            if (inputVelocity == Vector2.Zero)
            {
                stateMachine.UpdateState("IDLE");
            }
            player.Velocity = inputVelocity;
        }
    }

    public override void Update(double delta)
    {
        // Oops, fell off platform.
        if (player.Velocity.Y > 0)
        {
            player.wasOnFloor = true;
            stateMachine.UpdateState("FALL");
        }

        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            stateMachine.UpdateState("JUMP");
        }
    }

    public override void Stop()
    {
        player.player.Stop(true);
    }
}
