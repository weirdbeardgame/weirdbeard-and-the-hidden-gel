using Godot;
using System;

public class Walk : State
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "WALK";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
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
        Vector2 inputVelocity = Vector2.Zero;

        float currentSpeed = 0;

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

            inputVelocity.x = GetInput().x * currentSpeed;
            player.Velocity = inputVelocity;

            if (inputVelocity.x < 0)
            {
                player.weirdBeard.FlipH = true;
            }

            else
            {
                player.weirdBeard.FlipH = false;
            }
        }
        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            stateMachine.UpdateState("JUMP");
        }

        if (inputVelocity == Vector2.Zero)
        {
            stateMachine.UpdateState("IDLE");
        }
    }

    public override void Exit()
    {
        player.player.Stop(true);
    }
}
