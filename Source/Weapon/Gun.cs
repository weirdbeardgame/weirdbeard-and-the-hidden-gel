using Godot;
using System;

public class Gun : WeaponCommon
{
    PackedScene bulletSpawn;
    Node2D bulletSpawner;

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
        var ammoSpawn = (KinematicBody2D)bulletSpawn.Instance();
        bulletSpawner = (Node2D)GetNode("spawner");
        ammoSpawn.Position = bulletSpawner.GlobalPosition;
        ammoSpawn.Rotation = bulletSpawner.GlobalRotation;

        SceneManager.CurrentScene.AddChild(ammoSpawn);
        velocity.x = speed * direction.x;
        ammoSpawn.MoveAndSlide(velocity);
    }
}
