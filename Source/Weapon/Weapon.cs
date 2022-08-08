using Godot;
using System;

public enum WeaponType { THROW, SWING, SHOOT }


public class Weapon : KinematicBody2D
{
    [Export]
    public WeaponType type;

    [Export]
    public string weaponName;

    [Export]
    public float fireRate;

    [Export]
    public float speed;

    public bool canThrowWeapon = true;

    Vector2 velocity = Vector2.Right;

    Sprite sprite;

    AnimationPlayer play;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sprite = (Sprite)GetNode("weaponSprite");
        play = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public void Attack(float dir, float delta)
    {
        switch (type)
        {
            case WeaponType.THROW:
                Throw(dir, delta);
                break;
        }
    }

    async void Throw(float dir, float delta)
    {
        if (dir < 0)
        {
            sprite.FlipH = true;
        }
        else if (dir > 0)
        {
            sprite.FlipH = false;
        }

        GD.Print("Dir: ", dir);

        canThrowWeapon = false;
        await ToSignal(GetTree().CreateTimer(fireRate), "timeout");
        canThrowWeapon = true;

        velocity.x = delta * speed * dir;
    }

    public override void _PhysicsProcess(float delta)
    {
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

    public void _on_VisibilityNotifier2D_viewport_exited()
    {
        QueueFree();
    }
}