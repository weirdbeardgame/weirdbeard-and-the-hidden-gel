using Godot;
using System;


public class Player : Actor
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public AnimationPlayer player;

    public int lives = 0;

    public Sprite weirdBeard;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        stateMachine = (StateMachine)GetNode("StateMachine");
        stateMachine.UpdateState("IDLE");
        weirdBeard = (Sprite)GetNode("WeirdBeard");
        lives = 3;
    }

    public Vector2 Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
        }
    }

    public void SetState(string state)
    {
        stateMachine.UpdateState(state);
    }

    public override void _PhysicsProcess(float delta)
    {
        velocity.y += gravity * delta;
        velocity = MoveAndSlide(velocity, Vector2.Up);
    }
}
