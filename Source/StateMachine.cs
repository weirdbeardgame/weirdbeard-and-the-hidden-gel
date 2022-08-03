using Godot;
using System;
using System.Collections.Generic;

public class StateMachine : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    RichTextLabel stateSet;

    Dictionary<string, NodePath> Nodes;

    State oldState;
    State state;

    public State CurrentState
    {
        get
        {
            return state;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateSet = (RichTextLabel)Owner.GetChild(7);
    }

    public void AddState(State s, string name)
    {
        if (Nodes == null)
        {
            Nodes = new Dictionary<string, NodePath>();
        }

        if (!Nodes.ContainsKey(name) && Nodes != null)
        {
            Nodes.Add(name, s.GetPath());
        }

        else
        {
            return;
        }
    }

    public void UpdateState(string newState)
    {
        if (oldState != null)
        {
            oldState = state;
            oldState.Exit();
        }
        state = GetNode<State>(Nodes[newState]);
        if (stateSet != null)
        {
            stateSet.Text = newState;
        }
        state.Start();
    }

    public override void _PhysicsProcess(float delta)
    {
        if (state != null)
        {
            state.FixedUpdate(delta);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (state != null)
        {
            state.Update(delta);
        }
    }
}
