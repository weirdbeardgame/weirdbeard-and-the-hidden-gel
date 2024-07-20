using Godot;
using System;

public partial class GoalPost : Area2D
{
    // To Do add gui selector in here for hub to transition to from SceneManager
    [Export] string hub;

    private LevelCommon _Current;

    public override void _Ready()
    {
        _Current = (LevelCommon)GetParent();
    }

    public void OnTouch(Node2D body)
    {
        if (_Current.IsLevelComplete)
        {
            return;
        }

        if (_Current.LevelState == LevelCompleteState.ACTIVE)
        {
            if (body is Player)
            {
                _Current.CompleteLevel();
                SceneManager.ChangeScene(hub, (Player)body);
            }
        }
    }
}
