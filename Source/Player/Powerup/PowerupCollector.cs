using Godot;
using System;

public partial class PowerupCollector : Node2D
{
    Player player;

    public void EquipPowerup(PowerUp power)
    {
        player = (Player)Owner;

        if (player.CurrentPowerup != power)
        {
            GD.Print("Powerup Equip");
            RemoveChild(player.CurrentPowerup);
            AddChild(power);
            player.CurrentPowerup = power;
        }
        else
        {
            return;
        }
    }
}
