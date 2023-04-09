using Godot;
using System;

public enum WeaponType { THROW, SWING, SHOOT }

public partial class WeaponCommon : Node
{
    [Export] public Texture2D icon;
    [Export] public int dmgAmnt;
    [Export] public float speed;
    [Export] public float fireRate;
    [Export] public string weaponName;
    [Export] public WeaponType weaponType;
    [Export] protected PackedScene shootable;

    protected Sprite2D sprite;
    protected Player player;
    public bool canThrowWeapon;
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

    public virtual void Attack(Vector2 direction, Node scene)
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
