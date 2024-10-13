using Godot;
using System;

public partial class Coin : Area2D
{
    [Export]
    int value;

    AnimationPlayer anim;

    AudioStreamPlayer2D effect;

    public static Action getCoin;
    public static Action looseCoin;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        anim = (AnimationPlayer)GetNode("AnimationPlayer");
        effect = (AudioStreamPlayer2D)GetNode("AudioStreamPlayer2D");
        BodyEntered += OnCollide;
        anim.Play("spin");
    }

    public void OnCollide(object body)
    {
        if (body is Player)
        {
            effect.Play();
            //anim.Play("Collect");
            getCoin.Invoke();
            QueueFree();
        }
    }

}
