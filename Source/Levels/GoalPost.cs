using Godot;
using System;

public partial class GoalPost : Area2D
{
    // To Do add gui selector in here for hub to transition to from SceneManager
    [Export] string hub;
    LevelCommon current;

    public override void _Ready()
    {
        BodyEntered += OnTouch;
    }

    public void OnTouch(object body)
    {
        GD.Print("GOOOAAAALLLL");
        current = (LevelCommon)Owner;
        if (body is Player)
        {
            current.CompleteLevel();
            SceneManager.changeScene(hub, (Player)body);
        }
    }
}
