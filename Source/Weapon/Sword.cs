using Godot;
using System;

public partial class Sword : WeaponCommon
{
    CharacterBody2D toSpawn;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override void Swing()
    {
        _AnimationPlayer.Play("Swing");
    }

    public override void _Process(double delta)
    {

    }

}
