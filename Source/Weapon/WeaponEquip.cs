using Godot;
using System;

public class WeaponEquip : Area2D
{

    [Export]
    PackedScene w;

    WeaponSlot slot;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        slot = (WeaponSlot)Owner.GetNode("HUD/WeaponSlot");
    }

    public void Equip(object body)
    {
        if (body is Player)
        {
            slot.Equip(w);
            QueueFree();
        }
    }


}
