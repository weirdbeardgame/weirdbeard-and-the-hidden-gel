using Godot;
using System;

public partial class GoalPost : Node2D
{
    // To Do add gui selector in here for hub to transition to from SceneManager
    [Export] string hub;
    LevelCommon current;

    public void OnTouch(object body)
    {
        current = (LevelCommon)Owner;
        if (body is Player)
        {
            current.CompleteLevel();
            SceneManager.changeScene(hub, (Player)body);
        }
    }
}
