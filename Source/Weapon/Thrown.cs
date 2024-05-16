using Godot;
using System;

public partial class Thrown : WeaponCommon
{
    private Vector2 _InputVelocity

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override Vector2 Shoot()
    {
        _InputVelocity = new Vector2(Speed * Direction.X, 0);

        return _InputVelocity;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
}
