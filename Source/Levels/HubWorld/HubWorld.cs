using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

[Tool]
public partial class HubWorld : LevelCommon
{
	[Export] Array<LevelSpace> levelSpaces;
	AudioStreamPlayer backgroundPlayer;
	HubActor Actor;

	int MaxLevelIndexes;
	int CurrentLevelIndex;

	int DesiredIndex;

	public override void EnterLevel(Player p)
	{
		Actor = (HubActor)GetNode("HubWorldActor");
		GD.Print("HubWorld");
		Scenes = SceneManager.Manager;

		if (Actor == null)
		{
			Actor = new HubActor();
		}
		if (_Player != null)
		{
			RemoveChild(_Player);
			Actor.Activate(_Player);
		}
		else
		{
			_Player = SceneManager._ActivePlayerRef;
			Actor.Activate(_Player);
		}

		if (levelSpaces != null)
		{
			MaxLevelIndexes = levelSpaces.Count;
			CurrentLevelIndex = 0;
			DesiredIndex = 0;
			Actor.GlobalPosition = levelSpaces[0].GlobalPosition;
		}
		else
		{
			GD.PrintErr("Level Spaces null");
		}
		//CreateAudioStream();
	}

	public override void ResetLevel()
	{
		CurrentLevelIndex = 0;
		Actor.GlobalPosition = levelSpaces[CurrentLevelIndex].GlobalPosition;
	}

	public override void Update()
	{
		base.Update();
		Move();
	}

	void Move()
	{
		DesiredIndex = CurrentLevelIndex;
		if (Input.IsActionJustPressed("Up"))
		{

		}
		if (Input.IsActionJustPressed("Down"))
		{

		}
		if (Input.IsActionJustPressed("Left"))
		{
			if (DesiredIndex > 0)
			{
				DesiredIndex -= 1;
			}
		}
		if (Input.IsActionJustPressed("Right"))
		{
			if (DesiredIndex < MaxLevelIndexes)
			{
				DesiredIndex += 1;
			}
		}

		// ToDo, play Tween animation in here
		if (DesiredIndex != CurrentLevelIndex)
		{
			CurrentLevelIndex = DesiredIndex;
			Actor.GlobalPosition = levelSpaces[DesiredIndex].GlobalPosition;
		}

		if (Input.IsActionJustPressed("Submit"))
		{
			Actor.Deactivate();
			levelSpaces[CurrentLevelIndex].ActivateLevel(_Player);
		}
	}

	public override void ExitLevel()
	{
		base.ExitLevel();
		//RemoveChild(_Player);
	}
}
