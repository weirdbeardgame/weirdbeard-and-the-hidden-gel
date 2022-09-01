using Godot;
using System;

public enum EnemyDirection { LEFT, RIGHT };

public class Enemy : Actor
{
    public AnimationPlayer player;

    RayCast2D Right;
    RayCast2D Left;

    Sprite sprite;

    EnemyDirection dirToWalk;

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


    public void Spawn(EnemyDirection dir)
    {
        sprite = (Sprite)GetNode("Rat");

        switch (dir)
        {
            case EnemyDirection.LEFT:
                velocity.x = -speed;
                sprite.FlipH = true;
                break;

            case EnemyDirection.RIGHT:
                velocity.x = speed;
                sprite.FlipH = false;
                break;
        }
    }

    public void Destroy()
    {
        velocity = Vector2.Zero;
        sprite = null;
    }

    public override void _PhysicsProcess(float delta)
    {

        if (Right.IsColliding())
        {
            sprite.FlipH = true;
            velocity.x = -speed;
        }

        if (Left.IsColliding())
        {
            sprite.FlipH = false;
            velocity.x = speed;
        }

        velocity.y += gravity * delta;

        velocity.y = MoveAndSlide(velocity, Vector2.Up).y;
    }

    public void OnArea2DAreaEntered(object area)
    {
        if (area is Player)
        {
            Player dead = area as Player;
            dead.SetState("DEATH");
        }
    }
}
