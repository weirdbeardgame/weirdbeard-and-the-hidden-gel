using Godot;
using System;

public partial class WeaponEquip : Area2D
{
    [Export]
    PackedScene w;

    Player player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public void Equip(object body)
    {
        WeaponCommon weap = w.Instantiate<WeaponCommon>();
        if (body is Player)
        {
            player = (Player)body;
            player.EquipWeapon(w, weap.icon);
            QueueFree();
        }
    }
}
