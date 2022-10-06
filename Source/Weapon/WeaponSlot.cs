using Godot;
using System;

public class WeaponSlot : Node
{
    WeaponCommon slottedWeapon;

    Sprite weaponSlot;

    Sprite weaponIcon;

    Player player;

    public WeaponCommon CurrentWeapon
    {
        get
        {
            return slottedWeapon;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weaponSlot = (Sprite)Owner.GetNode("Camera2D/HUD/WeaponSlot");
        player = (Player)Owner;
    }

    public void Equip(PackedScene toEquip, Sprite weaponSprite)
    {
        if (slottedWeapon != (WeaponCommon)toEquip.Instance())
        {
            slottedWeapon = (WeaponCommon)toEquip.Instance();
        }
    }
}
