using Godot;
using System;

public partial class WeaponSlot : Node
{
    Sprite2D _Icon;
    PackedScene _Weapon;

    public PackedScene Weapon => _Weapon;

    public override void _Ready()
    {
        _Icon = GetNode<Sprite2D>("WeaponBox/WeaponIcon");
    }

    public void SlotWeapon(PackedScene W, Sprite2D I)
    {
        if (W != _Weapon)
        {
            _Weapon = W;
            _Icon = I;
        }
    }
}
