using Godot;
using System;

public class Weapon : KinematicBody2D
{
    [Export]
    string WeaponName;

    [Export]
    public float fireRate = 0.2f;

    [Export]
    public float speed = 350.0f;

    Vector2 velocity = Vector2.Right;

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

    public void Throw(float dir, float delta)
    {
        GD.Print("Dir: ", dir);
        velocity.x = dir * speed * delta;
    }

    public override void _PhysicsProcess(float delta)
    {

        if (velocity.x < 0)
        {
            sprite.FlipH = true;
        }
        else
        {
            sprite.FlipH = false;
        }

        //GD.Print("Velocity: ", velocity);
        KinematicCollision2D col = MoveAndCollide(velocity);
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
