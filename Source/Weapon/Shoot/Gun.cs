using Godot;
using System;

public partial class Gun : WeaponCommon
{
    int firedRounds;

    Bullet spawnedAmmo;

    Node2D bulletSpawner;

    Vector2 oldPos;

    [Export] float kickback = 0f;

    [Export] float spreadAmt = 0f;

    [Export] float bulletDirection;

    [Export] int maxRoundsInScene = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = (Sprite2D)GetNode("Gun");
        bulletSpawner = (Node2D)GetNode("Gun/spawner");
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public override void Attack(Vector2 direction)
    {
        if (firedRounds != maxRoundsInScene)
        {
            spawnedAmmo = shootable.Instantiate<Bullet>();
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

    public override void _PhysicsProcess(double delta)
    {

        if (player.direction.X >= 0)
        {
            sprite.FlipH = true;
        }
        else
        {
            sprite.FlipH = false;
        }

        oldPos = GlobalPosition;

        GlobalPosition = oldPos * direction;

        bulletDirection = player.direction.X;
        bulletSpawner.GlobalRotation = player.Rotation;
    }
}
