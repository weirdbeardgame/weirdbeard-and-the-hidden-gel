using Godot;
using System;

public enum WeaponType { THROW, SWING }

public partial class WeaponCommon : Node
{
    protected string _Name;
    protected int _AmmoAmnt;
    protected int _MaxAmmoAmnt;

    protected float _Speed;

    // For BlunderBuss or, weapons with weight.
    protected Vector2 _PushbackForce;

    protected WeaponType _WeaponType;
    protected AnimationPlayer _AnimationPlayer;

    [Export]
    public Texture2D Icon;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Player = (Player)SceneManager.s_CurrentScene.GetNode("Player");
        _AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public virtual void Equip()
    {

    }

    public virtual Vector2 Shoot()
    {

    }

    public virtual void Swing()
    {

    }

    public void Attack(Vector2 Direction, Node scene)
    {
        switch (_WeaponType)
        {
            case WeaponType.SWING:
                Velocity += Shoot();
                break;

            case WeaponType.THROW:
                Swing();
                break;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
