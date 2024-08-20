using Godot;
using System;

public partial class Idle : State
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "IDLE";
        Player = (Player)GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
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
        if (Player.CanMove)
        {
            if (Player.Direction == Vector2.Left)
            {
                Player.WeirdBeard.FlipH = true;
            }
            else
            {
                Player.WeirdBeard.FlipH = false;
            }

            if (Input.IsActionPressed("Right") || Input.IsActionPressed("Left"))
            {
                StateMachine.UpdateState("WALK");
            }

            if (Input.IsActionJustPressed("Jump") && Player.CanJump)
            {
                StateMachine.UpdateState("JUMP");
            }
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(true);
    }
}
