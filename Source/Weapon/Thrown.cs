using Godot;
using System;

public partial class Thrown : WeaponCommon
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override Vector2 Shoot(Vector2 Dir) => new Vector2(_Speed * Dir.X, 0);


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
}