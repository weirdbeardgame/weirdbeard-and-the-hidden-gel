using Godot;
using System;
using System.Collections.Generic;

public enum Directions { N, S, E, W };


// Path means linked paths to levels. Directions could be varried by associated paths etc

public class Path : Path2D
{
    [Export] Dictionary<Directions, NodePath> linkedPaths;

    bool unlocked;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void Move()
    {
        if (unlocked)
        {

        }
    }


}
