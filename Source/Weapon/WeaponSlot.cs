using Godot;
using System;

public class WeaponSlot : Node
{
    public WeaponCommon slottedWeapon;

    Player player;

    public static Action<Texture> updateWSprite;

    public void Attack()
    {
        switch (slottedWeapon.weaponType)
        {
            case WeaponType.THROW:
                // Needs to make a new instance each time! This spawns in scene when thrown
                // Does the player need to do this? Or, should this really be in here like it was previously
                // Even though it was me "Equipping a packed secne" Which won't work for Guns that are attached to player

                // WeaponCommon throwable = (WeaponCommon)slottedWeapon.Instance();
                slottedWeapon.Attack(player.direction.Sign());
                break;

            case WeaponType.SHOOT:
                slottedWeapon.Attack(player.direction.Sign());
                break;
        }
    }
}
