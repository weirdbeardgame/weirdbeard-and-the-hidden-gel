using Godot;
using System;
using Godot.Collections;

[Tool]
public partial class Level : LevelCommon
{
	[Export] Dictionary<string, PackedScene> _Sublevels;

	[Export] private string _hub;
	// If there are sub scenes / levels
	[Export] private Array<Exit> _exits;

	// Could also contain items like piece of map
	private GoalPost _levelGoal;

	[Export] private Array<EnemySpawner> _activeEnemySpanwers;

	protected Checkpoint CurrentCheckpoint;

	[Export] private int _maxEnemyAmnt;
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

		switch (_levelState)
		{
			case LevelState.COMPLETE:
				// Some objectives were completed, powerups that may be perma in the scene may be collected etc.
				// Can also use this in games where there could be optional timed modes.
				break;

			case LevelState.NON_COMPLETE:

				break;
		}

		_levelState = LevelState.ACTIVE;

		if (_activeEnemySpanwers != null)
		{
			foreach (var spawner in _activeEnemySpanwers)
			{
				spawner.Spawn();
			}
		}

		_Player.ResetPlayerPosition(CurrentCheckpoint);
		if (!_Player.IsInsideTree())
		{
			AddChild(_Player);
		}

		_Player.ResetPlayer();

		_levelGoal = GetNode<GoalPost>("Goal");
		_levelGoal.LevelComplete += ExitLevel;

		//CreateAudioStream();
	}

	public override void Update()
	{
		base.Update();

	}

	public void ResetPlayerPosition()
	{
		PlayerStartPoint = GetNode<Node2D>("PlayerStartPoint");

		if (CurrentCheckpoint != null)
		{
			_Player.Position = CurrentCheckpoint.GlobalPosition;
		}
		else
		{
			// Need to grab a "Player Starting place"
			_Player.Position = PlayerStartPoint.GlobalPosition;
		}
	}

	public override void ResetLevel()
	{
		_Player.ResetPlayerPosition(CurrentCheckpoint);
		RemoveChild(_Player);
		EnterLevel(_Player);
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

		RemoveChild(_Player);
		foreach (var spawner in _activeEnemySpanwers)
		{
			spawner.Destroyed();
		}

		SceneManager.ChangeScene(_hub, _Player);

		_levelGoal.LevelComplete -= ExitLevel;

		QueueFree();
	}
}
