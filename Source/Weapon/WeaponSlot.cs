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
        weaponSlot = (Sprite)Owner.GetNode("weaponSlot");
        weaponIcon = (Sprite)GetNode("WeaponIcon");
    }

    public void Equip(PackedScene toEquip, Sprite weaponSprite)
    {
        if (slottedWeapon != toEquip)
        {
            slottedWeapon = toEquip;
        }
        weaponIcon = weaponSprite;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
