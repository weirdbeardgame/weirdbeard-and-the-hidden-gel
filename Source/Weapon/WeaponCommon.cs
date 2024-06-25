using Godot;
using System;

public enum WeaponType { THROW, SWING }

// To tell the weapon system who to target and who used or threw it
// A Weapon thrown by an enemy wouldn't hurt an enemy
public enum WeaponUser { ENEMY, PLAYER }

public partial class WeaponCommon : Area2D
{
    [Export]
    protected string _Name;

    [Export]
    protected float _Speed;

    [Export]
    protected WeaponType _WeaponType;

    protected WeaponUser _User;

    // For BlunderBuss or, weapons with weight.
    [Export]
    protected Vector2 _PushbackForce;

    protected Vector2 _Direction;
    protected Vector2 _Velocity;

    protected AnimationPlayer _AnimationPlayer;

    [Export]
    public Sprite2D Icon;
    private Vector2 _SpawnPoint;
    private int _CurrentAmmoAmnt;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Player = (Player)SceneManager.s_CurrentScene.GetNode("Player");
        _AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        BodyEntered += DetectHit;

    }

    public virtual void Shoot(Vector2 Dir, Vector2 Pos)
    {

    }

    public virtual void Swing()
    {

    }

    public virtual void Equip()
    {

    }

    public void ApplyPushBack(Actor P) => P.Velocity += _PushbackForce;


    public virtual Vector2 Move()
    {
        return Vector2.Zero;
    }

    public void DetectHit(Node2D body)
    {
        switch (_User)
        {
            case WeaponUser.PLAYER:
                if (body is not Player && body is Enemy)
                {
                    Enemy ene = (Enemy)body;
                    GD.Print("Detected Enemy");
                    ene.Die();
                    QueueFree();
                }
                break;

            case WeaponUser.ENEMY:
                if (body is Player && body is not Enemy)
                {
                    Player plyr = (Player)body;
                    GD.Print("Detected Player");
                    plyr.Die();
                    QueueFree();
                }
                break;
        }
    }


    public void Attack(Vector2 Direction, Node scene, WeaponUser U, Actor P)
    {
        _User = U;
        _Direction = Direction;
        _SpawnPoint = GetNodeOrNull<Node2D>("Gun/SpawnPoint")?.GlobalPosition ?? P.GlobalPosition;

        switch (_WeaponType)
        {
            case WeaponType.SWING:
                Swing();
                break;

            case WeaponType.THROW:
                //_AnimationPlayer.Play("Shoot");
                //if (_CurrentAmmoAmnt > 0)
                //{
                Shoot(_Direction, _SpawnPoint);
                _CurrentAmmoAmnt -= 1;
                ApplyPushBack(P);
                //}
                break;
        }
    }
}
