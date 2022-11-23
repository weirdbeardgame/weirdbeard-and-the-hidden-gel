using Godot;
using System;
using System.Collections.Generic;

public class HubWorld : LevelCommon
{
    [Export] List<NodePath> containedLevels;

    Paths path;
    PathFollow2D follow2D;

    LevelSpaces currentSpace;

    public override void EnterLevel(Player p)
    {
        base.EnterLevel(p);
        if (player != null)
        {
            RemoveChild(player);
            player.gravity = 0;
            currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
            player.Position = currentSpace.GlobalPosition;
            currentSpace.AddChild(player);
            currentSpace.player = player;
        }
    }

    public override void ResetLevel()
    {
        currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
        player.Position = currentSpace.Position;
        player.Rotation = currentSpace.Rotation;
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

        if (currentSpace.CanMove(dir))
        {
            path = (Paths)currentSpace.GetNode(currentSpace.AttachedPaths[dir]);
            currentSpace.RemoveChild(player);
            path.Start(player, dir);
        }
    }

    public void ChangeSpace(LevelSpaces space)
    {
        if (space != null)
        {
            currentSpace = space;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        if (path != null)
        ChangeSpace(path.space);
    }

    public override void ExitLevel()
    {
        base.ExitLevel();
        RemoveChild(player);
        player = null;
    }
}
