using Godot;
using System;

public class Jump : State
{
    Vector2 inputVelocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "JUMP";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        player.player.Play("Jump");
        player.gravity = player.jumpGravity;

        GD.Print("Jump Gravity: ", player.jumpGravity);
    }

    public override Vector2 GetInput()
    {
        inputVelocity.x = player.Velocity.x;
        if (Input.IsActionPressed("Jump"))
        {
            inputVelocity.y = player.jumpVelocity;
        }
        return inputVelocity;
    }

    public override void FixedUpdate(float delta)
    {
        player.Velocity = GetInput();
        if (player.Position.y <= -player.jumpHeight)
        {
            stateMachine.UpdateState("FALL");
        }
    }

    public override void Stop()
    {
        player.player.Stop(true);
    }
}
