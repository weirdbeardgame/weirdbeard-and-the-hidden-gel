using Godot;
using System;

public enum WeaponType { THROW, SWING, SHOOT }

public class Weapon : KinematicBody2D
{
    [Export] public int dmgAmnt;
    [Export] public float speed;
    [Export] public float fireRate;
    [Export] public WeaponType type;
    [Export] public PackedScene ammo;
    [Export] public string weaponName;

    Sprite sprite;

    Node2D spawnPoint;
    Node2D bulletSpawner;
    AnimationPlayer play;
    public Vector2 direction;
    Vector2 velocity = Vector2.Right;
    public bool canThrowWeapon = true;


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
                Shoot(direction);
                break;
        }
    }

    async void Throw(Vector2 direction)
    {
        var scene = SceneManager.CurrentScene;
        spawnPoint = (Node2D)scene.GetNode("Player/WeapSpawn");
        Position = spawnPoint.GlobalPosition;
        Rotation = spawnPoint.GlobalRotation;
        SceneManager.CurrentScene.AddChild(this);

        velocity.x = speed * direction.x;

        if (direction.x < 0)
        {
            sprite.FlipH = true;
        }
        else if (direction.x > 0)
        {
            sprite.FlipH = false;
        }

        canThrowWeapon = false;
        await ToSignal(GetTree().CreateTimer(fireRate), "timeout");
        canThrowWeapon = true;
    }

    void Shoot(Vector2 direction)
    {
        if (ammo != null)
        {
           var ammoSpawn = (KinematicBody2D)ammo.Instance();
           bulletSpawner = (Node2D)GetNode("spawner");
           ammoSpawn.Position = bulletSpawner.GlobalPosition;
           ammoSpawn.Rotation = bulletSpawner.GlobalRotation;

           SceneManager.CurrentScene.AddChild(ammoSpawn);
           velocity.x = speed * direction.x;
           ammoSpawn.MoveAndSlide(velocity);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
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