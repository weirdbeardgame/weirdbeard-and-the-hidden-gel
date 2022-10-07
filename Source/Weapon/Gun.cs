using Godot;
using System;

public class Gun : WeaponCommon
{
    [Export] PackedScene bulletSpawn;
    Node2D bulletSpawner;

    KinematicBody2D spawnedAmmo;

    [Export] float spreadAmt = 0f;

    [Export] float kickback = 0f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = (Sprite)GetNode("sprite");
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public override void Attack(Vector2 direction)
    {
        spawnedAmmo = (KinematicBody2D)bulletSpawn.Instance();
        bulletSpawner = (Node2D)GetNode("spawner");
        spawnedAmmo.Position = bulletSpawner.GlobalPosition;
        spawnedAmmo.Rotation = bulletSpawner.GlobalRotation;

        SceneManager.CurrentScene.AddChild(spawnedAmmo);
        velocity.x = speed * direction.x;
    }

    public override void _PhysicsProcess(float delta)
    {

        if (player.direction.x >= 0)
        {
            sprite.FlipH = true;
        }
        else
        {
            sprite.FlipH = false;
        }

        GlobalRotation = player.Rotation;

        if (spawnedAmmo != null)
        {
            KinematicCollision2D col = spawnedAmmo.MoveAndCollide(velocity * delta);
            if (col != null)
            {
                GD.Print("Collision");
                if (col.Collider is Enemy)
                {
                    GD.Print("Enemy Detected");
                    Enemy e = col.Collider as Enemy;
                    e.QueueFree();
                    spawnedAmmo.QueueFree();
                }
            }
        }
    }
}
