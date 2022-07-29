using Godot;
using System;

public class Falling : State
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "FALL";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override void Start()
    {
        player.player.Play("Fall");
    }


    public override void FixedUpdate(float delta)
    {
        Vector2 inputVelocity = Vector2.Zero;
        inputVelocity.y += player.gravity * delta;

        if (player.IsOnFloor())
        {
            stateMachine.UpdateState("IDLE");
        }
    }

    public override void Exit()
    {
        player.player.Stop(false);
    }
}
