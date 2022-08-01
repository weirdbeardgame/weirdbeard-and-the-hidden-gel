using Godot;
using System;

public class WeaponSlot : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    Weapon SlottedWeapon;

    Sprite weaponBox;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void Equip(Weapon toEquip)
    {
        if (SlottedWeapon != toEquip)
        {
            SlottedWeapon = toEquip;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
