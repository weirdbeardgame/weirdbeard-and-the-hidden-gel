using Godot;
using System;

public class LevelChanger : Area2D
{

    [Export]
    PackedScene teleportTo;

    public override void _Ready()
    {
    }

    public void Teleport(object body)
    {
        LevelCommon scene = (LevelCommon)teleportTo.Instance();
        if (body is Player)
        {
            SceneManager.changeScene(scene.levelName, (Player)body);
        }
    }
}
