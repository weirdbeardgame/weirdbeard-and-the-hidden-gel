using Godot;
using System;

public partial class Coin : Area2D
{
    [Export]
    int value;

    AnimationPlayer anim;

    public static Action getCoin;
    public static Action looseCoin;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        anim = (AnimationPlayer)GetNode("AnimationPlayer");
        BodyEntered += OnCollide;
        anim.Play("spin");
    }

    public void OnCollide(object body)
    {
        if (body is Player)
        {
            //anim.Play("Collect");
            getCoin.Invoke();
            QueueFree();
        }
    }

}
