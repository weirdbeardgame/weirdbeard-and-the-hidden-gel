using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

public partial class HubWorld : LevelCommon
{
    [Export] Array<LevelSpace> levelSpaces;
    AudioStreamPlayer backgroundPlayer;
    HubActor actor;

    int maxLevelIndexes;
    int levelIndex;

    public override void EnterLevel(Player p)
    {
        actor = (HubActor)GetNode("Actor");
        GD.Print("HubWorld");
        if (actor == null)
        {
            actor = new HubActor();
        }
        if (Player != null)
        {
            RemoveChild(Player);
            actor.Activate(Player);
            AddChild(actor);
        }

        if (levelSpaces != null)
        {
            maxLevelIndexes = levelSpaces.Count;
        }
        else
        {
            GD.PrintErr("Level Spaces null");
        }
        //CreateAudioStream();
        base.EnterLevel(p);
    }

    public override void ResetLevel()
    {
        levelIndex = 0;
        actor.GlobalPosition = levelSpaces[levelIndex].GlobalPosition;
    }

    public override void Update()
    {
        base.Update();
        Move();
    }

    void Move()
    {
        if (Input.IsActionJustPressed("Up"))
        {

        }
        if (Input.IsActionJustPressed("Down"))
        {

        }
        if (Input.IsActionJustPressed("Left"))
        {
            if (levelIndex > 0)
            {
                levelIndex -= 1;
                actor.GlobalPosition = levelSpaces[levelIndex].GlobalPosition;
            }
        }
        if (Input.IsActionJustPressed("Right"))
        {
            if (levelIndex < maxLevelIndexes)
            {
                levelIndex += 1;
                actor.GlobalPosition = levelSpaces[levelIndex].GlobalPosition;
            }
        }

        if (Input.IsActionJustPressed("Submit"))
        {
            levelSpaces[levelIndex].ActivateLevel(Player);
        }
    }

    public override void ExitLevel()
    {
        base.ExitLevel();
        RemoveChild(Player);
    }
}
