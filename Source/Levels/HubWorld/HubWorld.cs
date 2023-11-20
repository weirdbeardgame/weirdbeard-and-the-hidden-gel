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

    public override void EnterLevel(Player p, LevelType t)
    {
        actor = (HubActor)GetNode("Actor");
        GD.Print("HubWorld");
        if (actor == null)
        {
            actor = new HubActor();
            AddChild(actor);
        }
        if (Player != null)
        {
            RemoveChild(Player);
            actor.Activate(Player);
        }

        maxLevelIndexes = levelSpaces.Count;

        CreateAudioStream();
        base.EnterLevel(p, t);
    }

    public override void ResetLevel()
    {
        levelIndex = 0;
        actor.GlobalPosition = levelSpaces[levelIndex].GlobalPosition;
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

        if (Input.IsActionJustPressed("Select"))
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
