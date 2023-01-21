using Godot;
using System;

public enum WeaponType { THROW, SWING, SHOOT }

public class WeaponCommon : Node2D
{
    [Export] public int dmgAmnt;
    [Export] public float speed;
    [Export] public float fireRate;
    [Export] public string weaponName;
    [Export] public WeaponType weaponType;
    [Export] protected PackedScene shootable;

    public bool canThrowWeapon;

    protected Sprite sprite;
    protected Player player;
    protected Vector2 velocity;
    protected Vector2 direction;
    protected AnimationPlayer animationPlayer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Player)SceneManager.CurrentScene.GetNode("Player");
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public virtual void Equip()
    {

    }

    public virtual void Attack(Vector2 direction)
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
