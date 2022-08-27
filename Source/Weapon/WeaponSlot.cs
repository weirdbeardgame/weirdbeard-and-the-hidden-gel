using Godot;
using System;

public class WeaponSlot : Node
{
    PackedScene slottedWeapon;

    Sprite weaponSlot;

    Sprite weaponIcon;

    public PackedScene Weapon
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
    }

    public void Equip(PackedScene toEquip, Sprite weaponSprite)
    {
        if (slottedWeapon != toEquip)
        {
            slottedWeapon = toEquip;
        }
        weaponSlot = weaponSprite;
        Vector2 S = new Vector2(100, 100);
        weaponSlot.Scale = S;
    }
}
