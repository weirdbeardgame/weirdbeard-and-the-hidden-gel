using Godot;
using System;

public partial class PowerUpEquip : Area2D
{
    [Export] PackedScene toEquip;

    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnTouch;
    }

    public void OnTouch(object body)
    {
        if (body is Player)
        {
            GD.Print("Equip");
            PowerUp p = toEquip.Instantiate<PowerUp>();
            Player play = (Player)body;

            p.Equip((Player)body);
            play.EquipPowerup(p);

            play.StateMachine.AddState(p, p.stateName);

            QueueFree();
        }
    }
}
