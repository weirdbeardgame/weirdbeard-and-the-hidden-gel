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
        if (body is Player)
        {
            //scenes.SwitchLevel((LevelCommon)teleportTo.Instance(), (Player)body);
        }
    }
}
