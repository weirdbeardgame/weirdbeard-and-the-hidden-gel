using Godot;
using System;

public class State : Node
{

    protected Player player;

    protected StateMachine stateMachine;

    public string stateName;

    protected bool isPaused;

    protected AnimationPlayer animator;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public virtual void Start()
    {

    }

    public virtual void Stop()
    {
    }

    public virtual Vector2 GetInput()
    {
        Vector2 inputVelocity = Vector2.Zero;
        //inputVelocity.x = (Input.GetActionRawStrength("Right") - Input.GetActionRawStrength("Left"));
        if (Input.IsActionPressed("Right"))
        {
            inputVelocity.x = 1.0f;
        }
        else if (Input.IsActionPressed("Left"))
        {
            inputVelocity.x = -1.0f;
        }

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
