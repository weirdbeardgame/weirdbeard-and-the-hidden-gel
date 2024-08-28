using Godot;
using System;

public partial class LadderDetector : Area2D
{

    [Export] public LadderStates State;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        BodyEntered += OnBody;
    }

    public void OnBody(Node2D body)
    {
        if (body is Player)
        {
            Player.s_UpdateLadderState(State);
        }
    }
}
