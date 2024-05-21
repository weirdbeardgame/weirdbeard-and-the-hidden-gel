using Godot;
using System;

public enum WeaponType { THROW, SWING }

// To tell the weapon system who to target and who used or threw it
// A Weapon thrown by an enemy wouldn't hurt an enemy
public enum WeaponUser { ENEMY, PLAYER }

public partial class WeaponCommon : CharacterBody2D
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
    protected AnimationPlayer _AnimationPlayer;

    [Export]
    public Sprite2D Icon;
    private Node2D _SpawnPoint;
    private int _CurrentAmmoAmnt;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Player = (Player)SceneManager.s_CurrentScene.GetNode("Player");
        _AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public virtual Vector2 Shoot(Vector2 Dir)
    {
        return Vector2.Zero;
    }

    public virtual void Swing()
    {

    }

    public void ApplyPushBack(Player P) => P.Velocity += _PushbackForce;


    public void Attack(Vector2 Direction, Node scene, WeaponUser U, Player P = null)
    {
        _Direction = Direction;
        _User = U;

        switch (_WeaponType)
        {
            case WeaponType.SWING:
                Swing();
                break;

            case WeaponType.THROW:
                //_AnimationPlayer.Play("Shoot");
                if (_CurrentAmmoAmnt > 0)
                {
                    _CurrentAmmoAmnt -= 1;
                    ApplyPushBack(P);
                }
                break;
        }
    }
}
