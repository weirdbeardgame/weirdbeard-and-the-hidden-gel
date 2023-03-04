using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
    [Export] float speed;
    Vector2 velocity;
    public bool isActive;

    public void Shoot(float vel)
    {
        isActive = true;
        velocity.X = vel * speed;
    }

    public override void _PhysicsProcess(double delta)
    {
        var col = MoveAndCollide(velocity * (float)(delta));
        if (col != null)
        {
            GD.Print("Collision");
            if (col.GetCollider() is Enemy)
            {
                GD.Print("Enemy Detected");
                Enemy e = col.GetCollider() as Enemy;
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
