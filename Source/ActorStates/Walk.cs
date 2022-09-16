using Godot;
using System;

public class Walk : State
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
            inputVelocity.x = 1.0f * currentSpeed;
        }
        else if (Input.IsActionPressed("Left"))
        {
            inputVelocity.x = -1.0f * currentSpeed;
        }
        if (!Input.IsActionPressed("Left") && !Input.IsActionPressed("Right"))
        {
            inputVelocity.x = 0;
        }
        return inputVelocity;
    }

    public override void FixedUpdate(float delta)
    {
        if (player.IsOnFloor())
        {
            GetInput();

            if (inputVelocity.Sign().x < 0)
            {
                player.direction = Vector2.Left;
                player.weirdBeard.FlipH = true;
            }

            else if (inputVelocity.Sign().x > 0)
            {
                player.direction = Vector2.Right;
                player.weirdBeard.FlipH = false;
            }

            if (inputVelocity == Vector2.Zero)
            {
                stateMachine.UpdateState("IDLE");
            }

            GD.Print("InputVelocity: ", inputVelocity);

            player.Velocity = inputVelocity;
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
