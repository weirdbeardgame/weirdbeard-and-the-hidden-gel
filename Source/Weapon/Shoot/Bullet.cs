using Godot;
using System;

public class Bullet : KinematicBody2D
{
    [Export] float speed;
    Vector2 velocity;
    public bool isActive;

    public void Shoot(float vel)
    {
        isActive = true;
        velocity.x = vel * speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        var col = MoveAndCollide(velocity * delta);
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
            else
            {
                QueueFree();
            }
        }
    }
}
