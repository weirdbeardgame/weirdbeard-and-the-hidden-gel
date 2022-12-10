using Godot;
using System;

public class Weapon : Node
{
    // Packed scene to hold a throwable.
    PackedScene slottedWeapon;

    WeaponCommon gun;

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
            switch (w.weaponType)
            {
                case WeaponType.THROW:
                    slottedWeapon = toEquip;
                    break;

                case WeaponType.SHOOT:
                    slottedWeapon = toEquip;
                    gun = w;
                    break;
            }
        }
    }


    public void Attack()
    {
        if (equipped)
        {
            WeaponCommon current = (WeaponCommon)slottedWeapon.Instance();
            switch (current.weaponType)
            {
                case WeaponType.THROW:
                    current.Attack(player.direction.Sign());
                    break;
            }
        }
    }
}
