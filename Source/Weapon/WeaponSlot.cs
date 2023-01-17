using Godot;
using System;

public class WeaponSlot : Node
{
    PackedScene slottedWeapon;

    WeaponCommon active;
    WeaponCommon current;

    Sprite weaponBox;
    Sprite weaponIcon;

    Player player;

    public static Action<Texture> updateWSprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weaponBox = (Sprite)Owner.GetNode("Camera2D/HUD/WeaponSlot");
        player = (Player)Owner;
    }

    public void Equip(PackedScene toEquip, Sprite weaponSprite)
    {
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
            updateWSprite.Invoke(weaponSprite.Texture);
        }
    }

    public void Attack()
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
