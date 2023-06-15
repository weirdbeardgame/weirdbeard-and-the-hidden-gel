using Godot;
using System;

public partial class PowerUpEquip : Area2D
{
    [Export] PackedScene toEquip;

    public override void _Ready()
    {
        BodyEntered += OnTouch;
        base._Ready();
    }

    PowerUp Power
    {
        get
        {
            return (PowerUp)toEquip.Instantiate();
        }
    }

    public void OnTouch(object body)
    {
        GD.Print("Touched");
        Node2D collided = (Node2D)body;
        GD.Print(collided.Name);
        if (collided.Name == "Player")
        {
            Player.OnEquip(Power);
            QueueFree();
        }
    }
}
