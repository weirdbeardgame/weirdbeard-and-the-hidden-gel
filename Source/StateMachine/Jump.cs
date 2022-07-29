using Godot;
using System;

public class Jump : State
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    bool isFalling;

    public override void Start()
    {
        player.player.Play("Jump");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        StateName = "JUMP";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void FixedUpdate(float delta)
    {
        Vector2 InputVelocity = Vector2.Zero;
        player.Velocity += GetInput();
        if (player.IsOnFloor() == false && !Input.IsActionJustPressed("Jump"))
        {
            stateMachine.UpdateState("FALL");
        }
    }

    public override void Exit()
    {
        player.player.Stop(true);
    }
}
