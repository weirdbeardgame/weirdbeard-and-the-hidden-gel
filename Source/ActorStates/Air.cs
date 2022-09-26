using Godot;
using System;

public class Air : State
{
    Vector2 inputVelocity;
    public override void _Ready()
    {
        stateName = "AIR";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {

    }

    public override void FixedUpdate(float delta)
    {
        if (Input.IsActionJustReleased("Jump") || !Input.IsActionPressed("Jump"))
        {
            inputVelocity.x = player.Velocity.x;
            inputVelocity.y = player.Velocity.y * 0.5f;
            player.Velocity = inputVelocity;
        }
    }

    public override void Update(float delta)
    {
        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            player.canJumpAgain = false;
            stateMachine.UpdateState("JUMP");
        }

        if (player.Velocity.y >= 0)
        {
            stateMachine.UpdateState("FALL");
        }
    }

    public override void Stop()
    {
        player.player.Stop(false);
    }
}
