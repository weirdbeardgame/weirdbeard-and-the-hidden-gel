using Godot;
using System;

public class LevelChanger : Area2D
{
    SceneManager scenes;

    [Export]
    string teleportTo;

    public override void _Ready()
    {
        scenes = (SceneManager)GetNode("/root/GameManager/SceneManager");
    }

    public void Teleport(object body)
    {
        if (body is Player)
        {
            scenes.SwitchLevel(teleportTo);
        }
    }
}
