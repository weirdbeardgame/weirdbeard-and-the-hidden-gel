using Godot;
using System;

public enum WeaponType { THROW, SWING, SHOOT }

public class Weapon : KinematicBody2D
{
    [Export] public WeaponType type;

    [Export] public string weaponName;

    [Export] public float fireRate;

    [Export] public float speed;

    [Export] public int dmgAmnt;

    public Vector2 direction;

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

    public void Attack(Vector2 direction)
    {
        switch (type)
        {
            case WeaponType.THROW:
                Throw(direction);
                break;

            case WeaponType.SHOOT:
                Shoot();
                break;
        }
    }

    async void Throw(Vector2 direction)
    {
        float dir = direction.Sign().x;
        if (dir < 0)
        {
            sprite.FlipH = true;
        }
        else if (dir > 0)
        {
            sprite.FlipH = false;
        }

        velocity.x = speed * dir;

        canThrowWeapon = false;
        await ToSignal(GetTree().CreateTimer(fireRate), "timeout");
        canThrowWeapon = true;
    }

    async void Shoot()
    {
        // Spawn bullet types in here. Could be blunderbuss shotgun or flintlock
        canThrowWeapon = false;
        await ToSignal(GetTree().CreateTimer(fireRate), "timeout");
        canThrowWeapon = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        //GD.Print("Velocity: ", velocity);
        KinematicCollision2D col = MoveAndCollide(velocity * delta);
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

    public void _on_VisibilityNotifier2D_viewport_exited(Viewport view)
    {
        QueueFree();
    }
}