using Godot;
using System;

public partial class WeaponSlot : Node
{
    public WeaponCommon weapon;

    public static Action<Texture2D> updateWSprite;

    public void Attack(Vector2 dir, Node scene)
    {
        weapon.Attack(dir, scene);
    }
}
