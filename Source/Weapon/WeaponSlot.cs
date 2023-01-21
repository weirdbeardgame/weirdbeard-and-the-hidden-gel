using Godot;
using System;

public class WeaponSlot : Node
{
    public WeaponCommon weapon;

    public static Action<Texture> updateWSprite;

    public void Attack(Vector2 dir)
    {
        weapon.Attack(dir);
    }
}
