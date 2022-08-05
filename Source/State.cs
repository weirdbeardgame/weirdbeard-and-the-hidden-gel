using Godot;
using System;

public class State : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    protected Player player;

    protected StateMachine stateMachine;

    public string StateName;

    protected bool isPaused;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public virtual void Start()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual Vector2 GetInput()
    {
        Vector2 inputVelocity = Vector2.Zero;
        inputVelocity.x = (Input.GetActionRawStrength("Right") - Input.GetActionRawStrength("Left"));
        if (Input.IsActionPressed("Jump"))
        {
            inputVelocity.y = -player.jumpForce;
        }
        return inputVelocity;
    }

    public virtual void Update(float delta)
    {

    }

    public virtual void FixedUpdate(float delta)
    {

    }
}
