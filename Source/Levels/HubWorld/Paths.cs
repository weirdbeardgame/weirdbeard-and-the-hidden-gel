using Godot;
using System;
using System.Collections.Generic;

public class Paths : Node
{
    [Export] Dictionary<Direction, NodePath> levelAttached;
    Tween interpolate;
    PathFollow2D follow2D;

    HubActor actor;

    public LevelSpaces space;

    Direction direction;

    public void Start(HubActor p, Direction dir)
    {
        interpolate = (Tween)GetNode("Tween");
        follow2D = (PathFollow2D)GetNode("PathFollow2D");
        GD.Print("Move");

        actor = p;
        direction = dir;

        follow2D.AddChild(p);
        interpolate.InterpolateProperty(follow2D, "unit_offset", 0.0f, 1.0f, 3.0f, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        interpolate.Start();
    }

    public void TweenComplete()
    {
        follow2D.RemoveChild(actor);
        space = GetNode<LevelSpaces>(levelAttached[direction]);
        space.AddChild(actor);
        space.actor = actor;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
