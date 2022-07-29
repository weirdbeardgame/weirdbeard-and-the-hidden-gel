using Godot;
using System;

public class Enemy : Actor
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public AnimationPlayer player;

    RayCast2D Right;
    RayCast2D Left;

    Sprite sprite;

    float dir;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        Right = (RayCast2D)GetNode("Right");
        Left = (RayCast2D)GetNode("Left");
        sprite = (Sprite)GetNode("Rat");
        player.Play("Walk");
        velocity.x = -speed;
        sprite.FlipH = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        velocity.y += gravity * delta;

        if (Right.IsColliding())
        {
            sprite.FlipH = false;
            velocity.x = speed;
        }

        if (Left.IsColliding())
        {
            sprite.FlipH = true;
            velocity.x = -speed;
        }

        velocity.y = MoveAndSlide(velocity, Vector2.Up).y;
    }

    public void OnArea2DAreaEntered(object area)
    {
        if (area is Player)
        {
            Player dead = area as Player;
            GD.Print("PLAYER TOUCH");
            dead.QueueFree();
        }
    }
}
