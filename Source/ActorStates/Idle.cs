using Godot;
using System;

public partial class Idle : State
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "IDLE";
        Player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        Player.AnimationPlayer.Play("Idle");
        Player.Velocity = Vector2.Zero;
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void Update(double delta)
    {
        base.Update(delta);
    }

    public override void FixedUpdate(double delta)
    {
        if (Player.canMove)
        {
            if (Player.direction == Vector2.Left)
            {
                Player.weirdBeard.FlipH = true;
            }
            else
            {
                Player.weirdBeard.FlipH = false;
            }

            if (Input.IsActionPressed("Right") || Input.IsActionPressed("Left"))
            {
                stateMachine.UpdateState("WALK");
            }

            if (Input.IsActionJustPressed("Jump") && Player.CanJump())
            {
                stateMachine.UpdateState("JUMP");
            }
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
    }
}
