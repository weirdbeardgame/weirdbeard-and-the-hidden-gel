using Godot;
using System;

public class PowerUpEquip : Node
{
    [Export] PackedScene toEquip;

    public void OnTouch(object body)
    {
        if (body is Player)
        {
            PowerUp p = (PowerUp)toEquip.Instance();
            Player play = (Player)body;

            p.Equip((Player)body);
            play.EquipPowerup(p);

            play.StateMachine.AddState(p, p.stateName);

            QueueFree();
        }
    }
}
