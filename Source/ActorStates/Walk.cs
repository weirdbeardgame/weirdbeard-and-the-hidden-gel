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
                PlayerData.direction = Vector2.Left;
                player.weirdBeard.FlipH = true;
            }

            else
            {
                PlayerData.direction = Vector2.Right;
                player.weirdBeard.FlipH = false;
            }
        }
        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            stateMachine.UpdateState("JUMP");
        }

        if (player.IsOnFloor() && inputVelocity.x < 0.1 && inputVelocity.x > -player.speed)
        {
            stateMachine.UpdateState("IDLE");
        }

        GD.Print("InputVelocity:", inputVelocity.x);

    }

    public override void Exit()
    {
        player.player.Stop(true);
    }
}
