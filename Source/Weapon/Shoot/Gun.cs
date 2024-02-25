using Godot;
using System;

public partial class Gun : WeaponCommon
{
    int firedRounds;

    Ammo spawnedAmmo;

    Vector2 oldPos;

    [Export] float kickback = 0f;

    [Export] float spreadAmt = 0f;

    [Export] float bulletDirection;

    [Export] int maxRoundsInScene = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = (Sprite2D)GetNode("Gun");
        spawnPoint = (Node2D)GetNode("Gun/spawner");
        Player = (Player)SceneManager.s_CurrentScene.GetNode("Player");
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public override void Attack(Vector2 Direction, Node scene)
    {
        if (firedRounds != maxRoundsInScene)
        {
            spawnedAmmo = shootable.Instantiate<Ammo>();
            spawnedAmmo.Position = spawnPoint.GlobalPosition;
            spawnedAmmo.Rotation = spawnPoint.GlobalRotation;

            SceneManager.s_CurrentScene.AddChild(spawnedAmmo);
            //spawnedAmmo.Shoot(bulletDirection);
            firedRounds += 1;
        }
        if (firedRounds == maxRoundsInScene)
        {
            firedRounds -= 1;
        }
    }

    public override void _PhysicsProcess(double delta)
    {

        if (Player.Direction.X >= 0)
        {
            sprite.FlipH = true;
        }
        else
        {
            sprite.FlipH = false;
        }

        bulletDirection = Player.Direction.X;
    }
}
