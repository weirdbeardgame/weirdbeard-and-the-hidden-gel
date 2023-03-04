using Godot;
using System;

public partial class LevelChanger : Area2D
{

    [Export]
    PackedScene teleportTo;

    public override void _Ready()
    {
    }

    public void Teleport(object body)
    {
        LevelCommon scene = teleportTo.Instantiate<LevelCommon>();
        if (body is Player)
        {
            SceneManager.changeScene(scene.levelName, (Player)body);
        }
    }
}
