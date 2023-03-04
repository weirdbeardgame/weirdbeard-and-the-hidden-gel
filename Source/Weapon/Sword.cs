using Godot;
using System;

public partial class Sword : WeaponCommon
{
    Node2D spawnPoint;

    CharacterBody2D toSpawn;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        sprite = (Sprite2D)GetNode("sprite");
    }

    public override void Attack(Vector2 direction)
    {
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        sprite = (Sprite2D)GetNode("sprite");

        toSpawn = (CharacterBody2D)shootable.Instantiate<CharacterBody2D>();

        spawnPoint = (Node2D)player.GetNode("WeapSpawn");
        toSpawn.Position = spawnPoint.GlobalPosition;
        toSpawn.Rotation = spawnPoint.GlobalRotation;

        SceneManager.CurrentScene.AddChild(this);

        velocity.X = speed * direction.X;

        if (direction.X < 0)
        {
            sprite.FlipH = true;
        }
        else if (direction.X > 0)
        {
            sprite.FlipH = false;
        }
    }

    public override void _Process(double delta)
    {
        KinematicCollision2D col = toSpawn.MoveAndCollide(velocity);
        if (col != null)
        {
            GD.Print("Collision");
            if (col.GetCollider() is Enemy)
            {
                GD.Print("Enemy Detected");
                Enemy e = col.GetCollider() as Enemy;
                e.QueueFree();
                QueueFree();
            }
        }
    }

}
