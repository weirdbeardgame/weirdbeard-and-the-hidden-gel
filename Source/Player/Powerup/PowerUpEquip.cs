using Godot;
using System;

public partial class PowerUpEquip : Node
{
    [Export] PackedScene toEquip;

    public void OnTouch(object body)
    {
        if (body is Player)
        {
            PowerUp p = toEquip.Instantiate<PowerUp>();
            Player play = (Player)body;

            p.Equip((Player)body);
            play.EquipPowerup(p);

            play.StateMachine.AddState(p, p.stateName);

            QueueFree();
        }
    }
}
