using Godot;
using System;
using System.Collections.Generic;

public partial class ProjectileManager : Node
{

    private List<WeaponCommon> _ProjectileInstances;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _ProjectileInstances = new List<WeaponCommon>();
    }

    public void AddProjectile(WeaponCommon Projectile)
    {
        //Projectile.BodyEntered += DestroyProjectile;
        _ProjectileInstances.Add(Projectile);
    }

    private void DestroyProjectile(Node2D ActorBody, Area2D Projectile)
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        foreach (var Projectile in _ProjectileInstances)
        {
            Projectile.Move();
        }
    }
}
