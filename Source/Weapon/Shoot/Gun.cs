using Godot;
using System;

public class Gun : WeaponCommon
{
    int firedRounds;

    Bullet spawnedAmmo;

    Node2D bulletSpawner;

    [Export] float kickback = 0f;

    [Export] float spreadAmt = 0f;

    [Export] float bulletDirection;

    [Export] int maxRoundsInScene = 3;

    [Export] protected PackedScene bulletSpawn;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = (Sprite)GetNode("sprite");
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public override void Attack(Vector2 direction)
    {
        if (firedRounds != maxRoundsInScene)
        {
            spawnedAmmo = (Bullet)bulletSpawn.Instance();
            bulletSpawner = (Node2D)GetNode("spawner");

            spawnedAmmo.Position = bulletSpawner.GlobalPosition;
            spawnedAmmo.Rotation = bulletSpawner.GlobalRotation;

            SceneManager.CurrentScene.AddChild(spawnedAmmo);
            spawnedAmmo.Shoot(bulletDirection);
            firedRounds += 1;
        }
        if (firedRounds == maxRoundsInScene)
        {
            firedRounds -= 1;
        }
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

        bulletDirection = player.direction.x;
        GlobalRotation = player.Rotation;
    }
}
