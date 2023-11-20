using Godot;
using System;

public partial class LevelSpace : Node2D
{
    [Export]
    PackedScene teleportTo;

    public override void _Ready()
    {
    }

    public void ActivateLevel(object body)
    {
        LevelCommon scene = teleportTo.Instantiate<LevelCommon>();
        if (body is Player)
        {
            SceneManager.changeScene(scene.levelName, (Player)body);
        }
    }
}
