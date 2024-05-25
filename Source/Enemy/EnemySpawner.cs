using Godot;
using System;

// Basically the Abstract Factory Pattern
public partial class EnemySpawner : Node2D
{
    [Export]
    PackedScene _Spawn;

    [Export]
    Direction _EnemyDirection = Direction.LEFT;

    Vector2 _EnePos, _ScreenSize;

    LevelCommon _ActiveScene;

    bool _IsDestroyed = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _EnePos = new Vector2();
        _ActiveScene = (LevelCommon)Owner;
    }

    public void Destroyed() => _IsDestroyed = true;

    public void Spawn()
    {
        if (_IsDestroyed)
        {
            var act = _Spawn.Instantiate<Actor>();

            act.GlobalPosition = GlobalPosition;
            CallDeferred("add_child", act);
            act.Destroyed += Destroyed;

            _IsDestroyed = false; // Enemy is active :D
        }

    }
}
