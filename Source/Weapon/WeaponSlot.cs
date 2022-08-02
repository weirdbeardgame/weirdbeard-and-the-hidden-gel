using Godot;
using System;

public class WeaponSlot : Node
{
    PackedScene slottedWeapon;

    Sprite weaponBox;

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

    }

    public void Equip(PackedScene toEquip)
    {
        if (slottedWeapon != toEquip)
        {
            slottedWeapon = toEquip;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
