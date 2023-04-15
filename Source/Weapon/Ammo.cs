using Godot;
using System;

public partial class Ammo : CharacterBody2D
{
    // For BlunderBuss or spread types
    [Export] float ammoAmount;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public void OnExit()
    {
        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        KinematicCollision2D col = MoveAndCollide(Velocity * (float)delta);
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
        }
    }
}
