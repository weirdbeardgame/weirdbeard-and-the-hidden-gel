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

	// Default to true because we want Spawner to run at start.
	bool _IsDestroyed = true;

	Enemy _Enemy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_EnePos = new Vector2();
		_ActiveScene = (LevelCommon)Owner;
	}

	// We'll just respawn it anyway, might as well eradicate active instance entirely from memory and reallocate.
	public void Destroyed()
	{
		if (_Enemy != null && !_IsDestroyed)
		{
			_IsDestroyed = true;
			RemoveChild(_Enemy);
			_Enemy.QueueFree();
		}
	}

	public void Spawn()
	{
		// Check if Enemy is Destroyed, extra check incase _IsDestroyed accidentially set to true but Enemy actually exists
		if (_IsDestroyed && !HasNode("Enemy"))
		{
			_Enemy = (Enemy)_Spawn.Instantiate<Actor>();

			CallDeferred("add_child", _Enemy);
			_Enemy.Position = new Vector2(0, -5);
			_Enemy.Destroyed += Destroyed;
			_IsDestroyed = false; // Enemy is active :D
		}
		// Fix the oopises just incase
		if (_IsDestroyed && HasNode("Enemy"))
		{
			_IsDestroyed = false;
		}

	}
}
