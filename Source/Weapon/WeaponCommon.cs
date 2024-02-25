using Godot;
using System;

public enum WeaponType { THROW, SWING, SHOOT }

public partial class WeaponCommon : Node
{
    [Export] public Texture2D icon;
    [Export] public int dmgAmnt;
    [Export] public float Speed;
    [Export] public float fireRate;
    [Export] public string weaponName;
    [Export] public WeaponType weaponType;
    [Export] protected PackedScene shootable;

    protected Sprite2D sprite;
    protected Player Player;
    public bool canThrowWeapon;
    protected Vector2 velocity;
    protected Vector2 Direction;
    protected Node2D spawnPoint;

    protected AnimationPlayer animationPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = (Player)SceneManager.s_CurrentScene.GetNode("Player");
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public virtual void Equip()
    {

    }

    public virtual void Attack(Vector2 Direction, Node scene)
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
