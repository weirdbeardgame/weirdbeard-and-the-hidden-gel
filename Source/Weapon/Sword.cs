using Godot;
using System;

public partial class Sword : WeaponCommon
{
    CharacterBody2D toSpawn;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = (Player)SceneManager.CurrentScene.GetNode("Player");
        sprite = (Sprite2D)GetNode("sprite");
    }

    public override void Attack(Vector2 Direction, Node scene)
    {
        Player = (Player)SceneManager.CurrentScene.GetNode("Player");
        spawnPoint = (Node2D)Player.GetNode("WeapSpawn");

        toSpawn = (CharacterBody2D)shootable.Instantiate<CharacterBody2D>();
        sprite = (Sprite2D)toSpawn.GetNode("sprite");
        toSpawn.Position = spawnPoint.GlobalPosition;
        toSpawn.Velocity = new Vector2(Speed * Direction.X, 0);
        scene.AddChild(toSpawn);

        if (Direction.X < 0)
        {
            sprite.FlipH = true;
        }
        else if (Direction.X > 0)
        {
            sprite.FlipH = false;
        }
    }

    public override void _Process(double delta)
    {

    }

}
