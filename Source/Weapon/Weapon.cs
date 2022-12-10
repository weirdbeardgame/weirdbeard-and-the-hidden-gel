using Godot;
using System;

public class Weapon : Node
{
    // Packed scene to hold a throwable.
    PackedScene slottedWeapon;

    WeaponCommon active;

    Sprite weaponBox;
    Sprite weaponIcon;

    Player player;

    bool equipped;

    public PackedScene CurrentWeapon
    {
        get
        {
            return slottedWeapon;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weaponBox = (Sprite)Owner.GetNode("Camera2D/HUD/WeaponSlot");
        player = (Player)Owner;
    }

    public void Equip(PackedScene toEquip, Sprite weaponSprite)
    {
        WeaponCommon current = new WeaponCommon();
        WeaponCommon w = (WeaponCommon)toEquip.Instance();
        if (slottedWeapon != null)
        {
            current = (WeaponCommon)slottedWeapon.Instance();
        }
        // ToDo - Added equipped weapon into active box
        if (w != current)
        {
            slottedWeapon = toEquip;
            active = w;
            equipped = true;
        }
    }

    public void Attack()
    {
        if (equipped)
        {
            switch (active.weaponType)
            {
                case WeaponType.THROW:
                    WeaponCommon current = (WeaponCommon)slottedWeapon.Instance();
                    current.Attack(player.direction.Sign());
                    break;

                case WeaponType.SHOOT:
                    active.Attack(player.direction.Sign());
                    break;
            }
        }
    }
}
