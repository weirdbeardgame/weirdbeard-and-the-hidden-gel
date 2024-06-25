using Godot;
using System;

public partial class Thrown : WeaponCommon
{

    [Export]
    PackedScene _Equippable;

    [Export]
    protected int _MaxAmmoAmnt;

    // Yes Captian!
    private VisibleOnScreenNotifier2D _OnScreen;

    private Area2D _SpawnedAmmo;

    private KinematicCollision2D _Col;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _OnScreen = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
    }

    public bool HasAnEquip => _Equippable != null;

    public Area2D EquipObj => _Equippable.Instantiate<Area2D>();

    public override Vector2 Move() => _Speed * _Direction;

    public override void Shoot(Vector2 Dir, Vector2 Pos)
    {
        _Direction = Dir;
        GlobalPosition = Pos;
        GetNode<Sprite2D>("sprite").FlipH = (bool)(_Direction.X < 0);
        SceneManager.s_CurrentScene.AddChild(this);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _Velocity = Move();

        Position += _Velocity * (float)delta;

        if (!_OnScreen.IsOnScreen())
        {
            //GD.Print("Off Screen");
            //QueueFree();
        }
    }
}
