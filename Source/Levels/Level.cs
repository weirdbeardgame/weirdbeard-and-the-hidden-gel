using Godot;
using System;
using Godot.Collections;

[Tool]
public partial class Level : LevelCommon
{
	[Export]
	Dictionary<string, PackedScene> sublevels;

	[Export]
	public Array<EnemySpawner> ActiveEnemySpanwers;

	[Export]
	public Array<Exit> exits;

	protected Checkpoint CurrentCheckpoint;

	[Export]
	public int maxEnemyAmnt;
	LevelCommon ActiveSubScene;

	// Currently active subScene. Otherwise null
	Level SubScene;
	Camera2D camera;

	Rect2 mapLimits;
	Vector2 mapCellsize;
	Node2D PlayerStartPoint;

	public override void EnterLevel(Player p)
	{
		base.EnterLevel(p);

		PlayerStartPoint = GetNode<Node2D>("PlayerStartPoint");

		if (CurrentCheckpoint != null)
		{
			Player.Position = CurrentCheckpoint.GlobalPosition;
		}
		else
		{
			// Need to grab a "Player Starting place"
			Player.Position = PlayerStartPoint.GlobalPosition;
		}

		if (ActiveEnemySpanwers != null)
		{
			foreach (var spawner in ActiveEnemySpanwers)
			{
				spawner.Spawn();
			}
		}

		/*if (ActiveEnemies != null)
		{
			foreach (var Enemy in ActiveEnemies)
			{
				if (!Enemy.IsInsideTree())
				{
					AddChild(Enemy);
				}
			}
		}*/

		if (!Player.IsInsideTree())
		{
			AddChild(Player);
		}
		Player.ResetPlayer();
		CreateAudioStream();
	}

	public override void Update()
	{
		base.Update();
	}

	public override void ResetLevel()
	{
		ExitLevel();
		EnterLevel(Player);
	}

	public void Checkpoint(Checkpoint NewCheckpoint)
	{
		if (CurrentCheckpoint != null)
		{
			CurrentCheckpoint.Deactivate();
		}
		NewCheckpoint.isActive = true;
		CurrentCheckpoint = NewCheckpoint;
	}

	// Clear the enemies and other data from the scene.
	// Ensure the scene closes properly before changing.
	public override void ExitLevel()
	{
		CallDeferred(nameof(CalledDefferedExitLevel));
	}

	public override void CalledDefferedExitLevel()
	{
		if (Player.IsInsideTree())
		{
			RemoveChild(Player);
		}
		/*if (ActiveEnemies != null)
		{
			foreach (var enemy in ActiveEnemies)
			{
				if (enemy.IsInsideTree())
				{
					RemoveChild(enemy);
				}
			}
		}*/
	}
}

