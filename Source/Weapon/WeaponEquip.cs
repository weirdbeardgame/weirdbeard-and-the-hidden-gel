using Godot;
using System;

public partial class WeaponEquip : Area2D
{
    [Export]
    private PackedScene _Weapon;

    Player Player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BodyEntered += Equip;
    }

    public void Equip(object body)
    {
        GD.Print("Weapon Equip");
        WeaponCommon weap = _Weapon.Instantiate<WeaponCommon>();
        if (body is Player)
        {
            Player = (Player)body;
            Player.EquipWeapon(_Weapon, weap.Icon);
            if (weap is Thrown)
            {
                var Shootable = (Thrown)weap;
                // Issa gunn!!
                if (Shootable.HasAmmo)
                {
                    Shootable.GlobalPosition = Player.GlobalPosition + new Vector2(5, -30);
                    Player.AddChild(Shootable);
                }

            }
            QueueFree();
        }
    }
}
