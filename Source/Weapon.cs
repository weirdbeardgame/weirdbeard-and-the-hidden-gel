using Godot;
using System;

public class Weapon : KinematicBody2D
{
    [Export]
    string WeaponName;

    [Export]
    public float fireRate = 0.2f;

    [Export]
    public float speed = 180.0f;

    Vector2 velocity = Vector2.Zero;

    Sprite sprite;


    AnimationPlayer play;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = (Sprite)GetNode("weaponSprite");
        play = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public bool Equip()
    {
        return false;
    }

    public void Throw(float dir)
    {
        GD.Print("Dir: ", dir);

        if (dir < 0)
        {
            sprite.FlipH = true;
        }
        else
        {
            sprite.FlipH = false;
        }

        Vector2 inputVelocity = new Vector2(dir * speed, 0);
        velocity = inputVelocity;
    }

    public void Attack(object body)
    {
        if (body is Enemy)
        {
            Enemy e = body as Enemy;
            e.QueueFree();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        GD.Print("Velocity: ", velocity);
        MoveAndSlide(velocity);
    }
}
