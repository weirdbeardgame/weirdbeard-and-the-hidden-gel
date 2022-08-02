using Godot;
using System;

public class Idle : State
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "IDLE";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        player.player.Play("Idle");
        player.Velocity = Vector2.Zero;
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void Update(float delta)
    {
        base.Update(delta);
    }

    public override void FixedUpdate(float delta)
    {
        if (Input.IsActionPressed("Right") || Input.IsActionPressed("Left"))
        {
            stateMachine.UpdateState("WALK");
        }

        if (Input.IsActionJustPressed("Jump") && player.CanJump())
        {
            stateMachine.UpdateState("JUMP");
        }

        if (!player.IsOnFloor())
        {
            stateMachine.UpdateState("FALL");
        }
    }

    public override void Exit()
    {
        player.player.Stop(true);
    }
}
