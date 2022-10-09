using Godot;
using System;

public class LevelSpaces : Node2D
{
    [Export] LevelCommon attachedLevel;

    public void Enter(object body)
    {
        if (body is Player)
        {
            if (Input.IsActionJustPressed("Submit"))
            {
                attachedLevel.EnterLevel((Player)body);
            }
        }
    }

}
