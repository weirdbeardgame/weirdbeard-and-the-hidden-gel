using Godot;
using System;
using System.Collections.Generic;

public class Paths : Node
{
    [Export] Dictionary<Direction, NodePath> levelAttached;
    Tween interpolate;
    PathFollow2D follow2D;

    Player player;

    public LevelSpaces space;

    Direction direction;

    public void Start(Player p, Direction dir)
    {
        interpolate = (Tween)GetNode("Tween");
        follow2D = (PathFollow2D)GetNode("PathFollow2D");
        GD.Print("Move");

        player = p;
        direction = dir;

        follow2D.AddChild(player);
        interpolate.InterpolateProperty(follow2D, "unit_offset", 0.0f, 1.0f, 3.0f, Tween.TransitionType.Back, Tween.EaseType.InOut);
        interpolate.Start();
    }

    public void TweenComplete()
    {
        follow2D.RemoveChild(player);
        space = GetNode<LevelSpaces>(levelAttached[direction]);
        space.AddChild(player);
        space.player = player;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
