using Godot;
using System;

public partial class Thrown : WeaponCommon
{

    [Export]
    PackedScene _Ammo;

    [Export]
    protected int _MaxAmmoAmnt;

    // Yes Captian!
    private VisibleOnScreenNotifier2D _OnScreen;

    private CharacterBody2D _SpawnedAmmo;

    private KinematicCollision2D _Col;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _OnScreen = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
    }

    public bool HasAmmo => _Ammo != null;


    public override void Shoot(Vector2 Dir, Vector2 Pos)
    {
        _Direction = Dir;
        if (_Ammo != null)
        {
            GD.Print("Guuunnnn");
            _SpawnedAmmo = _Ammo.Instantiate<CharacterBody2D>();
            _SpawnedAmmo.GlobalPosition = Pos;
            SceneManager.s_CurrentScene.AddChild(_SpawnedAmmo);
        }

        else
        {
            GlobalPosition = Pos;
            GetNode<Sprite2D>("sprite").FlipH = (bool)(_Direction.X > 0);
            AddChild(this);
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!_OnScreen.IsOnScreen())
        {
            QueueFree();
        }
    }
}
