using Godot;
using System;
using System.Collections.Generic;

public class HubWorld : LevelCommon
{
    [Export] List<NodePath> containedLevels;

    Path2D path;
    PathFollow2D follow2D;
    Tween interpolate;

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
            AddChild(player);
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
            path = (Path2D)currentSpace.GetNode(currentSpace.AttachedPaths[dir]);
            interpolate = (Tween)path.GetNode("Tween");
            follow2D = (PathFollow2D)path.GetNode("PathFollow2D");
            GD.Print("Move");

            RemoveChild(player);
            follow2D.AddChild(player);
            interpolate.InterpolateProperty(follow2D, "unit_offset", 0.0f, 1.0f, 3.0f, Tween.TransitionType.Back, Tween.EaseType.InOut);
            interpolate.Start();

            

            //follow2D.RemoveChild(player);
            AddChild(player);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
    }

    public override void ExitLevel()
    {
        base.ExitLevel();
        RemoveChild(player);
        player = null;
    }
}
