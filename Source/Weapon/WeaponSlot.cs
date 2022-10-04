using Godot;
using System;

public class WeaponSlot : Node
{
    Weapon slottedWeapon;

    Sprite weaponSlot;

    Sprite weaponIcon;

    Player player;

    public Weapon CurrentWeapon
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
        if (slottedWeapon != (Weapon)toEquip.Instance())
        {
            slottedWeapon = (Weapon)toEquip.Instance();
            
            if (slottedWeapon.type == WeaponType.SHOOT)
            {
                player.EquipWeapon(slottedWeapon);
            }
        }
        weaponSlot = weaponSprite;
        Vector2 S = new Vector2(100, 100);
        weaponSlot.Scale = S;
    }
}
