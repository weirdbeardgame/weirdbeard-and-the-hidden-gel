using Godot;
using System;

public class Sword : WeaponCommon
{
    Node2D spawnPoint;

    KinematicBody2D toSpawn;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        sprite = (Sprite)GetNode("sprite");
    }

    public override void Attack(Vector2 direction)
    {
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        sprite = (Sprite)GetNode("sprite");

        toSpawn = (KinematicBody2D)shootable.Instance();

        spawnPoint = (Node2D)player.GetNode("WeapSpawn");
        toSpawn.Position = spawnPoint.GlobalPosition;
        toSpawn.Rotation = spawnPoint.GlobalRotation;

        SceneManager.CurrentScene.AddChild(this);

        velocity.x = speed * direction.x;

        if (direction.x < 0)
        {
            sprite.FlipH = true;
        }
        else if (direction.x > 0)
        {
            sprite.FlipH = false;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        KinematicCollision2D col = toSpawn.MoveAndCollide(velocity * delta);
        if (col != null)
        {
            GD.Print("Collision");
            if (col.Collider is Enemy)
            {
                GD.Print("Enemy Detected");
                Enemy e = col.Collider as Enemy;
                e.QueueFree();
                QueueFree();
            }
        }
    }

}
