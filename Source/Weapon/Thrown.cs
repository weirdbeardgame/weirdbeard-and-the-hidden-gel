using Godot;
using System;

public partial class Thrown : WeaponCommon
{
    [Export]
    Vector2 _SpawnOffset;

    [Export]
    protected int _MaxAmmoAmnt;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public override Vector2 Shoot(Vector2 Dir) => new Vector2(_Speed * Dir.X, 0);


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Velocity = Shoot(_Direction);

        var col = MoveAndCollide(Velocity);

        switch (_User)
        {
            case WeaponUser.PLAYER:
                GD.Print("Player Threw Weapon");
                if (col.GetCollider() is Enemy)
                {
                    var ene = (Enemy)col.GetCollider();
                    ene.QueueFree();

                    // Destroy the weapon after collision.
                    QueueFree();
                }
                break;

            case WeaponUser.ENEMY:
                if (col.GetCollider() is Player)
                {
                    var plyr = (Player)col.GetCollider();
                    plyr.QueueFree();

                    // Destroy the weapon after collision.
                    QueueFree();
                }
                break;
        }
    }
}
