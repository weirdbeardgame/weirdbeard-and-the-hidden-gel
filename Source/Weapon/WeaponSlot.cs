using Godot;
using System;

public class WeaponSlot : Node
{
    PackedScene slottedWeapon;

    Sprite weaponSlot;

    Sprite weaponIcon;

    Player player;

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
        weaponSlot = (Sprite)Owner.GetNode("Camera2D/HUD/WeaponSlot");
        player = (Player)Owner;
    }

    public void Equip(PackedScene toEquip, Sprite weaponSprite)
    {
        if (slottedWeapon != toEquip)
        {
            slottedWeapon = toEquip;
        }
    }

    // Weapons like Blunder bluss or Flintlock he'll actually be holding
    public void AttachToPlayer(PackedScene toEquip, Sprite weaponSprite)
    {
        if (slottedWeapon != toEquip)
        {
            slottedWeapon = toEquip;
            //player.GetNode("WeapSpawn").AddChild(slottedWeapon.Instance());
        }
    }
}
