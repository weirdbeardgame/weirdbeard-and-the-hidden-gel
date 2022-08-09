using Godot;
using System;

public class SubLevelChanger : Area2D
{
    SceneManager scenes;
    Level currentLevel;

    [Export]
    string subLevel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        scenes = (SceneManager)GetNode("/root/GameManager/SceneManager");
        currentLevel = scenes.CurrentScene;
    }

    public void Teleport(object body)
    {
        if (body is Player)
        {
            currentLevel.EnterSubLevel(subLevel);
        }
    }
}
