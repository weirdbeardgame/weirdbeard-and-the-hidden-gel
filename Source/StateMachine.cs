using Godot;
using System;
using Godot.Collections;

public partial class StateMachine : Node
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
        stateSet = (RichTextLabel)Owner.GetNode("StateSet");
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

    public void InitState(string defaultState)
    {
        state = (State)GetNode(Nodes[defaultState]);
        if (stateSet != null)
        {
            stateSet.Text = defaultState;
        }
        state.Start();
    }

    public void UpdateState(string newState)
    {
        if (state != null)
        {
            if (state.stateName != newState)
            {
                oldState = state;
                state = (State)GetNode(Nodes[newState]);
                if (oldState != null)
                {
                    oldState.Stop();
                }
                if (stateSet != null)
                {
                    stateSet.Text = newState;
                }
                state.Start();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
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
        stateSet.Text = state.stateName;
        state.Start();
    }

    public void ResetToOldState()
    {
        state = null;
        state = oldState;
        if (stateSet != null)
        {
            stateSet.Text = state.stateName;
        }
        state.Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (state != null)
        {
            state.Update(delta);
        }
    }
}
