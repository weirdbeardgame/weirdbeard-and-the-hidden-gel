using Godot;
using System;
using System.Collections.Generic;

public class StateMachine : Node
{
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
        oldState = state;
        if (oldState != null)
        {
            oldState.Stop();
        }

        var owner = Owner;
        var own = Owner.Owner;
        state = (State)GetNode(Nodes[newState]);
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

    // Take the current state and restart it's logic, IE. You're going to double jump!
    public void ResetState()
    {
        var stateTemp = GetNode<State>(Nodes[CurrentState.stateName]);

        state.Stop();
        state = null;

        state = stateTemp;

        state.Start();
    }

    public void ResetToOldState()
    {
        state.Stop();
        state = null;

        state = oldState;
        state.Start();
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
