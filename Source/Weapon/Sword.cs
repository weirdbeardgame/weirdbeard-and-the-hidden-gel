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

    public override void Attack(Vector2 direction, Node scene)
    {
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        spawnPoint = (Node2D)player.GetNode("WeapSpawn");

        toSpawn = (CharacterBody2D)shootable.Instantiate<CharacterBody2D>();
        sprite = (Sprite2D)toSpawn.GetNode("sprite");
        toSpawn.Position = spawnPoint.GlobalPosition;
        toSpawn.Velocity = new Vector2(speed * direction.X, 0);
        scene.AddChild(toSpawn);

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

    }

}
