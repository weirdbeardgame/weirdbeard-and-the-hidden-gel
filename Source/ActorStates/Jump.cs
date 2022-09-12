using Godot;
using System;

public class Jump : State
{
    Vector2 inputVelocity = Vector2.Zero;

    public override void Start()
    {
        player.player.Play("Jump");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "JUMP";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override Vector2 GetInput()
    {
        inputVelocity.x = player.Velocity.x;
        inputVelocity.y = -player.maxJumpImpulse;

        if (Input.IsActionJustReleased("Jump") && player.Velocity.y < 0)
        {
            inputVelocity.y = player.minJumpImpulse;
        }

        return inputVelocity;
    }

    public override void FixedUpdate(float delta)
    {
        player.Velocity = GetInput();
        if (player.IsOnFloor() == false && player.Velocity.y > 0)
        {
            stateMachine.UpdateState("FALL");
        }
    }

    public override void Stop()
    {
        player.player.Stop(true);
    }
}
