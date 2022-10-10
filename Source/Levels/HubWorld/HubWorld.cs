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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void EnterLevel(Player p)
    {
        player = p;
        currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
        player.Position = currentSpace.GlobalPosition;
        AddChild(player);
    }

    public override void ResetLevel()
    {
        currentSpace = (LevelSpaces)GetNode(containedLevels[0]);
        player.Position = currentSpace.GlobalPosition;
    }

    public override void _PhysicsProcess(float delta)
    {
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

            path = (Path2D)GetNode(currentSpace.AttachedPaths[dir]);
            interpolate = (Tween)path.GetNode("Tween");
            follow2D = (PathFollow2D)path.GetNode("follow");
            follow2D.AddChild(player);
            interpolate.InterpolateProperty(follow2D, "UnitOffset", 0.0f, 1.0f, 3.0f, Tween.TransitionType.Back, Tween.EaseType.InOut);

            // Add player child of next space
        }


    }

    public override void ExitLevel()
    {
        base.ExitLevel();
    }
}
