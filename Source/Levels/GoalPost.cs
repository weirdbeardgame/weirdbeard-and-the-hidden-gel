using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class GoalPost : Area2D
{

    public Action LevelComplete;

    // To Do add gui selector in here for hub to transition to from SceneManager
    [Export] string hub;

    private LevelCommon _current;

    bool _isTouched;

    public void OnTouch(Node2D body)
    {
        LevelComplete.Invoke();
    }
}
