using Godot;
using System;

public partial class Air : State
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

    public override void FixedUpdate(double delta)
    {
        if (Input.IsActionJustReleased("Jump") || !Input.IsActionPressed("Jump"))
        {
            inputVelocity.X = player.Velocity.X;
            inputVelocity.Y = player.Velocity.Y * 0.5f;
            player.Velocity = inputVelocity;
        }
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            player.canJumpAgain = false;
            stateMachine.UpdateState("JUMP");
        }

        if (player.Velocity.Y >= 0)
        {
            stateMachine.UpdateState("FALL");
        }
    }

    public override void Stop()
    {
        player.player.Stop(false);
    }
}
