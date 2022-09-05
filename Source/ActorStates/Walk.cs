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
        return base.GetInput();
    }

    public override void FixedUpdate(float delta)
    {


        if (player.IsOnFloor())
        {
            if (Input.IsActionPressed("Run"))
            {
                currentSpeed = player.speed * 2;
            }

            else
            {
                currentSpeed = player.speed;
            }

            inputVelocity.x = currentSpeed * GetInput().x;
            player.Velocity = inputVelocity;

            if (inputVelocity.x < 0)
            {
                player.direction = Vector2.Left;
                player.weirdBeard.FlipH = true;
            }

            else
            {
                player.direction = Vector2.Right;
                player.weirdBeard.FlipH = false;
            }
        }
        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            stateMachine.UpdateState("JUMP");
        }

        if (player.IsOnFloor() && inputVelocity == Vector2.Zero)
        {
            stateMachine.UpdateState("IDLE");
        }
    }

    public override void Exit()
    {
        player.player.Stop(true);
    }
}
