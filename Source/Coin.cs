using Godot;
using System;

public class Coin : Node
{
    [Export]
    int value;

    AnimationPlayer anim;

    public static Action getCoin;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        getCoin += Collect;
    }

    public void Collect()
    {
        anim.Play("Collect");
        getCoin -= Collect;
        QueueFree();
    }

}
