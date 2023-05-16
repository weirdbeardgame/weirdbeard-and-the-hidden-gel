using Godot;
using System;
using Godot.Collections;

public partial class StateMachine : Node
{
    RichTextLabel stateSet;
    Dictionary<string, NodePath> Nodes;

    private State _oldState;
    private State _state;

    public State CurrentState
    {
        get
        {
            return _state;
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

    public void RemoveState(State s)
    {
        if (Nodes.ContainsKey(s.stateName))
        {
            Nodes.Remove(s.stateName);
        }
    }

    public void InitState(string defaultState)
    {
        _state = (State)GetNode(Nodes[defaultState]);
        if (stateSet != null)
        {
            stateSet.Text = defaultState;
        }
        _state.Start();
    }

    public void UpdateState(string newState)
    {
        if (_state != null)
        {
            if (_state.stateName != newState)
            {
                _oldState = _state;
                _state = GetNode<State>(Nodes[newState]);
                if (_oldState != null)
                {
                    _oldState.Stop();
                }
                if (stateSet != null)
                {
                    stateSet.Text = newState;
                }
                _state.Start();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _state?.FixedUpdate(delta);
    }

    // Take the current state and restart it's logic, IE. You're going to double jump!
    public void ResetState()
    {
        var stateTemp = GetNode<State>(Nodes[CurrentState.stateName]);
        _state.Stop();
        _state = null;
        _state = stateTemp;
        stateSet.Text = _state.stateName;
        _state.Start();
    }

    public void ResetToOldState()
    {
        _state = null;
        _state = _oldState;
        if (stateSet != null)
        {
            stateSet.Text = _state.stateName;
        }
        _state.Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _state?.Update(delta);
    }
}
