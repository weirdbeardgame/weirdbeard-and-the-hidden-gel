using Godot;
using System;
using Godot.Collections;

public partial class HubWorld : LevelCommon
{
    [Export] Array<NodePath> containedLevels;

    Paths path;
    PathFollow2D follow2D;
    LevelSpaces currentSpace;
    AudioStreamPlayer backgroundPlayer;

    HubActor actor;

    public override void EnterLevel(Player p)
    {
        activeEnemies = new Array<Enemy>();
        backgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");

        actor = (HubActor)GetNode("Actor");

        base.EnterLevel(p);
        if (player != null)
        {
            RemoveChild(player);
            actor.Activate(player);
            currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
            currentSpace.AddChild(actor);
            currentSpace.actor = actor;
        }
    }

    public override void ResetLevel()
    {
        currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
        actor.Position = currentSpace.Position;
        actor.Rotation = currentSpace.Rotation;
    }

    void GetInput()
    {
        if (Input.IsActionJustPressed("Submit"))
        {
            currentSpace.EnterLevel();
        }

        Direction dir = 0;
        if (Input.IsActionJustPressed("Up"))
        {
            dir = Direction.N;
        }

        if (Input.IsActionJustPressed("Down"))
        {
            dir = Direction.S;
        }

        if (Input.IsActionJustPressed("Left"))
        {
            dir = Direction.W;
        }

        if (Input.IsActionJustPressed("Right"))
        {
            dir = Direction.E;
        }

    }

    public void ChangeSpace(LevelSpaces space)
    {
        if (space != null)
        {
            currentSpace = space;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        if (path != null)
        {
            ChangeSpace(path.space);
        }
    }

    public override void ExitLevel()
    {
        base.ExitLevel();
        RemoveChild(player);
    }
}
