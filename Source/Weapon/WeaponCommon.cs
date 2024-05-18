using Godot;
using System;

public enum WeaponType { THROW, SWING }

public partial class WeaponCommon : CharacterBody2D
{
    [Export]
    protected string _Name;

    [Export]
    protected float _Speed;

    [Export]
    protected int _MaxAmmoAmnt;

    [Export]
    protected WeaponType _WeaponType;

    // For BlunderBuss or, weapons with weight.
    [Export]
    protected Vector2 _PushbackForce;

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

    public virtual void Equip()
    {

    }

    public virtual Vector2 Shoot(Vector2 Dir)
    {
        return Vector2.Zero;
    }

    public virtual void Swing()
    {

    }

    public void ApplyPushBack(Player P) => P.Velocity += _PushbackForce;


    public void Attack(Vector2 Direction, Node scene, Player P = null)
    {
        switch (_WeaponType)
        {
            case WeaponType.SWING:
                Swing();
                break;

            case WeaponType.THROW:
                if (_CurrentAmmoAmnt > 0)
                {
                    Velocity += Shoot(Direction);
                    _CurrentAmmoAmnt -= 1;
                    ApplyPushBack(P);
                }
                break;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
